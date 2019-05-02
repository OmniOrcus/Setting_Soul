using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class AgroUI : MonoBehaviour, IObserver {

    Image indicator;
    PlayerControl player;
    public float maxAgro;

    public void Observe()
    {
        if (player.Agro >= maxAgro)
        {
            indicator.color = new Color(indicator.color.r, indicator.color.g, indicator.color.b, 1);
        }
        else
        {

            indicator.color = new Color(indicator.color.r, indicator.color.g, indicator.color.b, player.Agro/maxAgro);
        }
    }

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        player.RegisterObserver(this);
        indicator = GetComponent<Image>();
	}
}
