using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLifeMonitor : MonoBehaviour
{
    public Color Fine;
    public Color Caution;
    public Color Warning;
    public Color Danger;

    public Image LifeBar;

    PlayerController Player;
    void Start()
    {
        Player = GetComponent<PlayerController>();
    }

    public void Update()
    {
        // Remove this from loop
        UpdateLifeMonitor();
    }

    public void UpdateLifeMonitor()
    {
        float currentLife = Player.Life / Player.MaximumLife;

        if (currentLife >= 0.76)
        {
            LifeBar.color = Fine;
        }
        else if (currentLife >= 0.51)
        {
            LifeBar.color = Caution;
        }
        else if (currentLife >= 0.26)
        {
            LifeBar.color = Warning;
        }
        else
        {
            LifeBar.color = Danger;
        }
    }
}
