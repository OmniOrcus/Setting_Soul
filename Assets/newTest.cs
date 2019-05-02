using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newTest : MonoBehaviour {

    public TextFadeTransitionFX test;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.T)) {
            test.Begin();
        }
	}
}
