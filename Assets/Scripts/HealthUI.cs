using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HealthUI : MonoBehaviour, IObserver
{

    Image indicator;
    PlayerControl player;

    public float maxIntensity = 1;

    public void Observe()
    {
        Debug.Log("#TEST# ~ Damage percentage: " + (1.0f - (float)((float)player.Health / (float)player.maxHealth)));
            indicator.color = new Color(indicator.color.r, indicator.color.g, indicator.color.b, (1.0f - (float)((float)player.Health / (float)player.maxHealth)) * maxIntensity);
    }

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        player.RegisterObserver(this);
        indicator = GetComponent<Image>();
    }
}
