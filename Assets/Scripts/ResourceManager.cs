using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    public Text StoneText;
    public Text WoodText;
    public Text AlienText;

    public int MaximumStone = 0;
    public int MaximumWood = 0;
    public int MaximumAlien = 0;
    void Start()
    {

    }

    public void UpdateResource(ResourceType type, int amount)
    {
        switch (type)
        {
            case ResourceType.Stone:
                StoneText.text = "x " + amount + "/" + MaximumStone;
                break;
            case ResourceType.Wood:
                WoodText.text = "x " + amount + "/" + MaximumWood;
                break;
            case ResourceType.Alien:
                AlienText.text = "x " + amount + "/" + MaximumAlien;
                break;
        }

    }
}
