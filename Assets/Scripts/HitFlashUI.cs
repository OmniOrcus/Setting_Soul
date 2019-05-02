using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HitFlashUI : MonoBehaviour {

    Image flash;
    public float fadeTime = 1;
    float fadeTrack = 0;
    public float maxVisability = 1;
    // Use this for initialization
    void Start () {
        flash = GetComponent<Image>();
        enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        flash.color = new Color(flash.color.r, flash.color.g, flash.color.b, (fadeTrack/fadeTime) * maxVisability);
        fadeTrack -= Time.deltaTime;
        if (fadeTrack <= 0) {
            enabled = false;
        }
    }

    public void GotHit() {
        fadeTrack = fadeTime;
        enabled = true;
    }

}
