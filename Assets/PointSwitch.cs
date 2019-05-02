using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSwitch : MonoBehaviour, IObserver {

    public CheckPoint old;
    public CheckPoint neo;
    public CheckPoint trigger;

    public void Observe()
    {
        if (old)
        {
            old.gameObject.SetActive(false);
        }
        neo.gameObject.SetActive(true);
    }

    // Use this for initialization
    void Start () {
        neo.gameObject.SetActive(false);
        trigger.RegisterObserver(this);
	}

}
