using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggeredDestroy : MonoBehaviour, IObserver
{

    public ObservableBehaviour trigger;
    public float destroyDelay = 0.05f;

    void Start() {
        trigger.RegisterObserver(this);
    }

    public void Observe()
    {
        Invoke("RemoveSelf", destroyDelay);
    }

    private void RemoveSelf() {
        trigger.UnregisterObserver(this);
        Destroy(gameObject);
    }

}
