using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : ObservableBehaviour
{ 

    //Movement
    public float walkSpeed;
    public float StrafeSpeed;
    public float TurnSpeed;
    public float LookSpeed;
    public float LookUpper;
    public float LookLower;
    public float collisionBuffer = 0.5f;
    float lookTrack = 0;
    public float JumpStrength;
    public Vector3 capsuleOffset;
    Rigidbody jumper;
    public Camera cam;
    CapsuleCollider col;

    //Agro System
    public float Agro { get; private set; }
    public float agroGain = 1;
    public float agroLoss = 1;
    bool lit;

    //Death System
    uint deathCount = 0;
    public int maxHealth;
    public int Health { get; private set; }
    public Vector3 checkPoint;
    public GameObject deathLight;
    public HitFlashUI hitFlash;
    public SFXRandom hitSound;

    //Suiside Cooldown... This feels weird to code :/.
    public float suisideCooldown;
    float suisideTrack = 0;
    public float suicideDelay = 0.5f;

    // Use this for initialization
    void Start () {
        jumper = GetComponent<Rigidbody>();
        Health = maxHealth;
        col = GetComponent<CapsuleCollider>();
	}
	
	// Update is called once per frame
	void Update () {

        //Player Controls
        //Rotation First
        if (Input.GetAxis("Turn") != 0)
        {
            transform.Rotate(transform.rotation * Vector3.up, Input.GetAxis("Turn") * TurnSpeed * Time.deltaTime);
        }
        if (Input.GetAxis("Look") != 0)
        {
            float lookDelta = -Input.GetAxis("Look") * LookSpeed * Time.deltaTime;
            if (lookTrack - lookDelta < LookUpper && lookTrack - lookDelta > LookLower)
            {
                lookTrack -= lookDelta;
                cam.transform.Rotate(cam.transform.localRotation * Vector3.right, lookDelta);
            }
        }
        //Translation Second
        if (Input.GetAxis("Walk") != 0) {
            if (!Physics.CapsuleCast(transform.position + capsuleOffset + (Vector3.up * (col.height/2)), transform.position + capsuleOffset - (Vector3.up * (col.height / 2)), col.radius, transform.rotation * (Input.GetAxis("Walk") * Vector3.forward), (walkSpeed * Time.deltaTime) + collisionBuffer))
            {
                transform.position += transform.rotation * (Input.GetAxis("Walk") * Vector3.forward * walkSpeed * Time.deltaTime);
            }
        }
        if (Input.GetAxis("Strafe") != 0)
        {
            if (!Physics.CapsuleCast(transform.position + capsuleOffset + (Vector3.up * (col.height/2)), transform.position + capsuleOffset - (Vector3.up * (col.height / 2)), col.radius, transform.rotation * (Input.GetAxis("Strafe") * Vector3.right), (StrafeSpeed * Time.deltaTime) + collisionBuffer))
            {
                transform.position += transform.rotation * (Input.GetAxis("Strafe") * Vector3.right * StrafeSpeed * Time.deltaTime);
            }
        }
        //Jump
        if (Input.GetAxis("Jump") > 0 && Grounded()) {
            Debug.Log("Player Jumped");
            jumper.velocity = Vector3.up * JumpStrength;
            InformObservers();
        }
        //Actions
        if (Input.GetAxis("Suicide") > 0 && suisideTrack <= 0) {
            suisideTrack = suisideCooldown;
            hitSound.KnifeSound();
            Invoke("Suicide", suicideDelay);
        }
            

        //Agro Disapate
        if (!lit && Agro > 0) {
            Agro -= agroLoss * Time.deltaTime;
            InformObservers();
        }
        lit = false;

        if (Health <= 0) {
            Dead();
        }

        Cooldowns();

    }

    void Cooldowns() {
        if (suisideTrack > 0) {
            suisideTrack -= Time.deltaTime;
        }
    }

    void Dead() {
        deathCount++;
        Instantiate(deathLight, transform.position, Quaternion.identity).name = ("Death Light " + deathCount.ToString());
        transform.position = checkPoint;
        Health = maxHealth;
        Agro = 0;
        InformObservers();
    }

    public void AddAgro(float intensity) {
        lit = true;
        Agro += agroGain * intensity * Time.deltaTime;
        Debug.Log("Player agro is: " + Agro);
        InformObservers();
    }

    public void TakeDamage(int damage) {
        Health -= damage;
        hitSound.Play();
        hitFlash.GotHit();
        InformObservers();
    }

    public bool Grounded() {
        //return Physics.Raycast(transform.position, -Vector3.up, 1.1f);
        bool output = Physics.CapsuleCast(transform.position + capsuleOffset + ((transform.rotation * Vector3.up) * (col.height / 2)), transform.position + capsuleOffset - ((transform.rotation * Vector3.up) * (col.height / 2)), col.radius, transform.rotation * -Vector3.up, 0.75f);
        Debug.Log("Grounded: " + output);
        return output;
    }

    //Encapsulated Suicide to allow for a delay
    private void Suicide() {
        Health = 0;
    }

}
