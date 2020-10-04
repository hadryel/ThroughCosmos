using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletUIManager : MonoBehaviour
{
    public PlayerController Player;
    public Image[] Bullets;
    int CurrentBulletCount;

    void Start()
    {
        CurrentBulletCount = Player.BulletCounter;
    }

    void Update()
    {
        UpdateBulletUI();

        //if (PlayerController.BulletCounter != CurrentBulletCount)
        //{
        //    UpdateBulletUI();
        //}
    }

    void UpdateBulletUI()
    {
        if (Time.time - Player.RechargeStart < Player.RechargeTime)
        {
            for (int i = 0; i < Bullets.Length; i++)
            {
                Bullets[i].enabled = false;
            }
            return;
        }

        CurrentBulletCount = Player.BulletCounter;

        for (int i = 0; i < Bullets.Length; i++)
        {
            if (i < CurrentBulletCount)
                Bullets[i].enabled = true;
            else
                Bullets[i].enabled = false;
        }
    }
}
