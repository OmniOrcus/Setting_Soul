using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFadeTransitionFX : MonoBehaviour {

    public float transitionTime = 1;
    public float stayTime = 1;
    float transitionDelta = 0;
    Text text;
    Color store;
    enum State { Start, Stay, End, Wait }
    State current = State.Wait;

    // Use this for initialization
    void Start () {
        text = GetComponent<Text>();
        store = text.color;
        text.color = new Color(store.r, store.g, store.b, 0);
        enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        switch (current)
        {
            case State.Start:
                FXStart();
                break;
            case State.Stay:
                FXStay();
                break;
            case State.End:
                FXEnd();
                break;
            default:
                Debug.LogError("TextFade Switch Failed!");
                enabled = false;
                break;
        }
        transitionDelta += Time.deltaTime;
    }

    public void SetText(string _text)
    {
        text.text = _text;
    }

    public void Begin()
    {
        current = State.Start;
        transitionDelta = 0;
        enabled = true;
    }

    void FXStart()
    {
        if (transitionDelta >= transitionTime)
        {
            transitionDelta = 0;
            current = State.Stay;
        }
        else
        {
            text.color = new Color(store.r, store.g, store.b, (transitionDelta / transitionTime) * store.a);
        }
    }

    void FXStay()
    {
        if (transitionDelta >= stayTime)
        {
            transitionDelta = 0;
            current = State.End;
        }
    }

    void FXEnd()
    {
        if (transitionDelta >= transitionTime)
        {
            transitionDelta = 0;
            current = State.Wait;
            enabled = false;
        }
        else
        {
            text.color = new Color(store.r, store.g, store.b, (1-(transitionDelta / transitionTime)) * store.a);
        }
    }

}
