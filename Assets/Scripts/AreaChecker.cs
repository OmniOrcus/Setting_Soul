using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaChecker : MonoBehaviour {

    public Area area;

	// Use this for initialization
	void Start () {
        Debug.Log("#TEST# ~ Location Check: " + area.Contains(transform.position));
	}
	
}
