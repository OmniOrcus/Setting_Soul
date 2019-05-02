using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSlideTransitionFX : MonoBehaviour {

    public Vector2 StartPoint;
    public Vector2 StayPoint;
    public Vector2 EndPoint;
    public float transitionTime = 1;
    public float stayTime = 1;
    public Text text;
    Vector2 textLocation;
    float transitionDelta;
    enum State {Start, Stay, End, Wait}
    State current = State.Wait;
    RectTransform textTransform;
    RectTransform fxTransform;

    // Use this for initialization
    void Start () {
        //Component Reference Retrieval
        //GetComponentInChildren<Text>();
        textTransform = text.GetComponent<RectTransform>();
        fxTransform = GetComponent<RectTransform>();
        //Setup
        textLocation = textTransform.position;
        fxTransform.localPosition = StartPoint;
        textTransform.position = textLocation;
        enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        switch (current) {
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
        textTransform.position = textLocation;
        transitionDelta += Time.deltaTime;
	}


    public void SetText(string _text) {
        text.text = _text;
    }

    public void Begin() {
        current = State.Start;
        transitionDelta = 0;
        transform.localPosition = StartPoint;
        enabled = true;
    }

    void FXStart() {
        if (transitionDelta >= transitionTime)
        {
            fxTransform.localPosition = StayPoint;
            transitionDelta = 0;
            current = State.Stay;
        }
        else
        {
            fxTransform.localPosition = Vector2.Lerp(StartPoint, StayPoint, transitionDelta / transitionTime);
        }
    }

    void FXStay() {
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
            fxTransform.localPosition = StayPoint;
            transitionDelta = 0;
            current = State.Wait;
            enabled = false;
        }
        else
        {
            fxTransform.localPosition = Vector2.Lerp(StayPoint, EndPoint, transitionDelta / transitionTime);
        }
    }

}
