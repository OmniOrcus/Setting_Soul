using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class DeathLight : MonoBehaviour {

    PlayerControl player;
    public float minDistance;
    public float maxDistance;
    Light source;
    float distance;

    // Use this for initialization
    void Start()
    {
        source = GetComponent<Light>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
    }
	
	// Update is called once per frame
	void Update () {
        distance = (transform.position - player.transform.position).magnitude;
        if (distance < minDistance) {
            Debug.Log(name + " is lighting player.");
            if (distance < maxDistance)
            {
                player.AddAgro(1);
            }
            else {
                player.AddAgro(1 - ((distance - maxDistance) / (minDistance - maxDistance)));
            }
        }
	}
}
