using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ProximitySFX : MonoBehaviour {

    PlayerControl player;
    AudioSource source;
    public float distanceRequirement = 1;
    public float soundCooldown = 1;
    public AudioClip[] sounds;
    float playTrack = 0;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        source = GetComponent<AudioSource>();
    }

	// Update is called once per frame
	void Update () {
        if ((player.transform.position - transform.position).magnitude < distanceRequirement && playTrack <= 0) {
            Debug.Log(name + "is making prox sound.");
            source.PlayOneShot(sounds[Random.Range(0, sounds.Length)]);
            playTrack = soundCooldown;
        }

        if (soundCooldown > 0) {
            playTrack -= Time.deltaTime;
        }

	}
}
