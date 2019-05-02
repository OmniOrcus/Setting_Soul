using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CheckPoint : ObservableBehaviour {

    PlayerControl player;
    public GameObject Pointer;
    public Vector2 nextPoint;
    public float requiredDistance;
    public TextFadeTransitionFX fadeFX;
    public string Message;
    Light signal;
    bool triggered = false;
    public Color offColor;
    public Color onColor;
    AudioSource source;

    // Use this for initialization
    void Start () {
        Message = Message.Replace("\\n", "\n");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        signal = GetComponentInChildren<Light>();
        signal.color = offColor;
        Vector3 nextVector = new Vector3(nextPoint.x, transform.position.y, nextPoint.y);
        Pointer.transform.rotation = Quaternion.LookRotation(nextVector - transform.position, Vector3.up);
        Pointer.gameObject.SetActive(false);
        source = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if ((transform.position - player.transform.position).magnitude < requiredDistance && !triggered) {
            Debug.Log(name + "has been triggered!");
            triggered = true;
            player.checkPoint = transform.position;
            signal.color = onColor;
            Pointer.gameObject.SetActive(true);
            fadeFX.SetText(Message);
            fadeFX.Begin();
            source.Play();
            InformObservers();
        }
	}
}
