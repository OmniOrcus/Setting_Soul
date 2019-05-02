using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeLights : MonoBehaviour {

    public Light[] eyes;
    float[] eyeIntensity;
    public float transitionTime = 1;
    float transitionDelta = 0;
    public bool Open { get; private set; }
    void Start() {
        Open = false;
        //Record Eye Intensities
        eyeIntensity = new float[eyes.Length];
        for (uint i = 0; i < eyes.Length; i++) {
            eyeIntensity[i] = eyes[i].intensity;
            eyes[i].intensity = 0;
        }
    }

    void Update()
    {
        if (Open)
        {
            Opening();
        }
        else {
            Closing();
        }

        transitionDelta += Time.deltaTime;
    }

    void Opening()
    {
        for (uint i = 0; i < eyes.Length; i++)
        {
            eyes[i].intensity = (transitionDelta / transitionTime) * eyeIntensity[i];
        }
        if (transitionDelta >= transitionTime) {
            enabled = false;
        }
    }

    void Closing()
    {
        for (uint i = 0; i < eyes.Length; i++)
        {
            eyes[i].intensity = (1-(transitionDelta / transitionTime)) * eyeIntensity[i];
        }
        if (transitionDelta >= transitionTime)
        {
            enabled = false;
        }
    }

    public void Set(bool _open) {
        transitionDelta = 0;
        Open = _open;
        enabled = true;
    }

}
