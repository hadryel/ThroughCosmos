using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public PlayerController Player;
    public Color CompleteColor;
    Quest[] Quests;
    int RemainingQuests;

    public ResultManager ResultManager;
    public ResourceManager ResourceManager;

    void Start()
    {
        Quests = GetComponentsInChildren<Quest>();
        RemainingQuests = Quests.Length;

        SetupTotals();
    }

    void SetupTotals()
    {
        foreach (Quest q in Quests)
        {
            ResourceManager.MaximumStone += q.Requirements[0];
            ResourceManager.MaximumWood += q.Requirements[1];
            ResourceManager.MaximumAlien += q.Requirements[2];
        }

        for (int i = 0; i < 3; i++)
        {
            ResourceManager.UpdateResource((ResourceType)i, 0);
        }
    }

    public void TryToComplete()
    {
        foreach (Quest q in Quests)
        {
            if (q.Completed)
                continue;

            bool finished = true;
            for (int i = 0; i < 3; i++)
            {
                if (Player.Resources[i] - q.Requirements[i] < 0)
                {
                    finished = false;
                    break;
                }
            }

            if (finished)
            {
                for (int i = 0; i < 3; i++)
                    Player.Resources[i] -= q.Requirements[i];

                RefreshResources();
                Complete(q);
            }
        }
    }

    public void Complete(Quest q)
    {
        q.Completed = true;
        q.GetComponent<Text>().color = CompleteColor;
        RemainingQuests--;

        if (RemainingQuests <= 0)
        {
            GameObject.Find("Player").SetActive(false);
            Time.timeScale = 0;
            ResultManager.SetupWin();
        }
    }

    public void RefreshResources()
    {
        for (int i = 0; i < 3; i++)
            Player.ResourceManager.UpdateResource((ResourceType)i, Player.Resources[i]);
    }
}
