using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    public QuestType Type;
    string Description;
    public bool Completed = false;
    public int[] Requirements; // 1 - stone, 2 - wood, 3 - alien 
    public float BonusTime;
    void Start()
    {
        SetupQuest(Type);
    }

    public void SetupQuest(QuestType type)
    {
        Requirements = new int[3];

        switch (type)
        {
            case QuestType.Refuel:
                Requirements[0] = 0;
                Requirements[1] = 3;
                Requirements[2] = 0;

                GetComponent<Text>().text = "Refuel the engines";
                BonusTime = 10f;
                break;
            case QuestType.NavigationSensor:
                Requirements[0] = 3;
                Requirements[1] = 3;
                Requirements[2] = 0;

                GetComponent<Text>().text = "Repair the navigation sensor";
                BonusTime = 20f;
                break;
            case QuestType.OxygenTanks:
                Requirements[0] = 0;
                Requirements[1] = 0;
                Requirements[2] = 4;

                GetComponent<Text>().text = "Refuel the Oxygen tanks";
                BonusTime = 25f;
                break;
            case QuestType.FoodSupplies:
                Requirements[0] = 0;
                Requirements[1] = 4;
                Requirements[2] = 4;

                GetComponent<Text>().text = "Repair the food supplier";
                BonusTime = 30f;
                break;
            case QuestType.DataAnalysis:
                Requirements[0] = 4;
                Requirements[1] = 1;
                Requirements[2] = 2;

                GetComponent<Text>().text = "Fix the data analysis device";
                BonusTime = 30f;
                break;
        }
    }
}

public enum QuestType
{
    Refuel, NavigationSensor, OxygenTanks, FoodSupplies, DataAnalysis
}