using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour {

    public float width; //x
    public float height; //y
    public float depth; //z

    public bool Contains(Vector3 point) {
        if (point.x > transform.position.x + width || point.x < transform.position.x - width) { return false; }
        if (point.y > transform.position.y + height || point.y < transform.position.y - height) { return false; }
        if (point.z > transform.position.z + depth || point.z < transform.position.z - depth) { return false; }
        return true;
    }

}
