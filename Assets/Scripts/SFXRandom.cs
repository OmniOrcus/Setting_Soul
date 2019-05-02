using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXRandom : MonoBehaviour {

    AudioSource source;
    public AudioClip[] clips;
    public AudioClip knifeFX;

	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
	}

    public void Play() {
        if (!source.isPlaying)
        {
            source.PlayOneShot(clips[Random.Range(0, clips.Length)]);
        }
    }

    //Added Later
    public void KnifeSound() {
        source.PlayOneShot(knifeFX);
    }
}
