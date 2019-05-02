using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScript : MonoBehaviour {

    public TextFadeTransitionFX fadeFX;
    public string Message;
    public float timeOut;
    float timeElapsed = 0;

    void Start() {
        Message = Message.Replace("\\n", "\n");
    }

    // Update is called once per frame
    void Update()
    {

        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) && timeElapsed <= 0)
        {
            timeElapsed = timeOut;
            fadeFX.SetText(Message);
            fadeFX.Begin();
        }

        if (timeElapsed > 0)
        {
            timeElapsed -= Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Y))
            {
                Application.Quit();
            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                timeElapsed = 0;

            }
        }
    }
}
