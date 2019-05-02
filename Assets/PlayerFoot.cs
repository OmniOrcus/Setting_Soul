using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFoot : MonoBehaviour, IObserver {

    BoxCollider footCollider;
    public PlayerControl player;
    public float checkDelay = 0.5f;
    

    // Use this for initialization
    void Start () {
        footCollider = GetComponent<BoxCollider>();
        player.RegisterObserver(this);
	}

    void Update() {
        if (player.Grounded() && footCollider.enabled == false) {
            footCollider.enabled = true;
        }
    }

    void JumpCheck() {
        if (!player.Grounded())
        {
            footCollider.enabled = false;
        }
    }

    public void Observe()
    {
        Invoke("JumpCheck", checkDelay);
    }
}
