using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowMob : MonoBehaviour, IObserver {

    public float agroRequirement;
    public int damage = 1;
    public float hitCooldown= 0.5f;
    float hitTrack;
    public float hitRange = 0.5f;
    public Area[] areas;
    PlayerControl player;
    public float WalkSpeed;
    public float TurnSpeed;
    public float WalkAccuracy = 5;
    public Vector3 capsuleOffset;
    CapsuleCollider col;
    EyeLights eyes;

    public void Observe()
    {
        Debug.Log(name + " is observing.");
        if (AreaCheck()) {

            if (player.Agro >= agroRequirement)
            {
                if (eyes.Open == false) {
                    eyes.Set(true);
                }
                enabled = true;
            }
            else
            {
                if (eyes.Open == true)
                {
                    eyes.Set(false);
                }
                enabled = false;
            }
        }
    }

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        col = GetComponent<CapsuleCollider>();
        eyes = GetComponent<EyeLights>();
        player.RegisterObserver(this);
        enabled = false;
    }

    void Update() {
        Debug.Log(name + " is Updating.");
        //Line from mob to player
        Vector3 line = player.transform.position - transform.position;
        //Turn
        Debug.Log(name + " turn value: " + VectorComponent(line.normalized, transform.rotation * Vector3.right));
        transform.Rotate(transform.rotation * Vector3.up, Clamp((VectorComponent(line.normalized, transform.rotation * Vector3.right) * 90), TurnSpeed) * Time.deltaTime);

        //Move
        Debug.Log(name + " walk angle: " + Vector3.Angle(line.normalized, transform.rotation * Vector3.forward));
        if (Vector3.Angle(line.normalized, transform.rotation * Vector3.forward) < WalkAccuracy && MoveCheck()) {
            transform.position += Vector3.ClampMagnitude(line, WalkSpeed) * Time.deltaTime;
        }
        //Attack
        Debug.Log(name + " Attack Check: " + (player.transform.position - transform.position).magnitude);
        if ((player.transform.position - transform.position).magnitude < hitRange && hitTrack <= 0) {
            Debug.Log(name + " hit player.");
            player.TakeDamage(damage);
            hitTrack = hitCooldown;
        }
        if (hitTrack > 0) {
            hitTrack -= Time.deltaTime;
        }
    }

    bool AreaCheck() {
        foreach (Area area in areas) {
            if (area.Contains(player.transform.position)) {
                return true;
            }
        }
        return false;
    }

    bool MoveCheck() {
        return !Physics.CapsuleCast(transform.position + capsuleOffset + (Vector3.up * (col.height / 2)), transform.position + capsuleOffset - (Vector3.up * (col.height / 2)), col.radius, transform.rotation * Vector3.forward, 0.55f);
    }

    //Calculates the component of a on b.
    static float VectorComponent(Vector3 a, Vector3 b)
    {
        return (Vector3.Dot(b, a) / b.magnitude);
    }

    static float Clamp(float value, float clamp) {
        if (value > clamp) { return clamp; }
        if (value < -clamp) { return -clamp; }
        return value;
    }


}
