using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour {

    public PlayerControl player;
    public float range = 2;
    public Vector3 destination;
	
	// Update is called once per frame
	void Update () {
        if ((player.transform.position - transform.position).magnitude <= range) {
            player.transform.position = destination;
            enabled = false;
        }
	}
}
