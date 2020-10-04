using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int CurrentLevel = 0;
    public List<int> SafeChunkIndexes;

    public static List<int> EnemyPool;

    public QuestManager QuestManager;
    public GameObject QuestPrefab;

    public TimeManager TimeManager;
    public DataManager DataManager;

    public GameObject DataManagerPrefab;
    void Awake()
    {
        SetupDataManager();
        LoadData();

        GetSafeChunks();
        InitEnemyPool();
        InitQuestPool();

        Time.timeScale = 1;
    }

    void LoadData()
    {
        CurrentLevel = DataManager.CurrentLevel;
        TimeManager.RemainingTime = 90f;
    }

    void SetupDataManager()
    {
        GameObject dmgo = GameObject.Find("DataManager");

        if (dmgo == null)
        {
            GameObject dmGO = GameObject.Instantiate(DataManagerPrefab);
            dmGO.name = "DataManager";
            DataManager = dmGO.GetComponent<DataManager>();
        }
        else
        {
            DataManager = dmgo.GetComponent<DataManager>();
        }
    }

    void InitQuestPool()
    {
        for (int i = 0; i <= CurrentLevel; i++)
        {
            GameObject questGO = GameObject.Instantiate(QuestPrefab, QuestManager.transform);
            Quest quest = questGO.GetComponent<Quest>();
            quest.Type = (QuestType)Random.Range(0, 4);
        }

        if (Random.Range(0f, 1f) >= 0.5f)
        {
            GameObject questGO = GameObject.Instantiate(QuestPrefab, QuestManager.transform);
            Quest quest = questGO.GetComponent<Quest>();
            TimeManager.RemainingTime += quest.BonusTime;
        }
    }

    void InitEnemyPool()
    {
        EnemyPool = new List<int>();

        switch (CurrentLevel)
        {
            case 0:
                for (int i = 0; i < 10; i++)
                    EnemyPool.Add(0);
                EnemyPool.Add(1);
                break;
            case 1:
                for (int i = 0; i < 10; i++)
                    EnemyPool.Add(0);
                for (int i = 0; i < 5; i++)
                    EnemyPool.Add(1);
                break;
            default:
                for (int i = 0; i < 10 + CurrentLevel * 3; i++)
                    EnemyPool.Add(0);
                for (int i = 0; i < 5 + CurrentLevel * 3; i++)
                    EnemyPool.Add(1);
                break;
        }
    }

    public static int GetRandomEnemy()
    {
        if (EnemyPool.Count <= 0)
            return -1;

        int index = Random.Range(0, EnemyPool.Count);
        int result = EnemyPool[index];
        EnemyPool.RemoveAt(index);

        return result;
    }

    public static void AddRandomEnemy(int type)
    {
        EnemyPool.Add(type);
    }

    void GetSafeChunks()
    {
        SafeChunkIndexes = new List<int>();
        List<int> PossibleChunkIndexes = new List<int>();

        for (int i = 0; i < 24; i++)
        {
            PossibleChunkIndexes.Add(i);
        }

        for (int i = 0; i < 5; i++)
        {
            int index = Random.Range(0, PossibleChunkIndexes.Count);
            SafeChunkIndexes.Add(PossibleChunkIndexes[index]);
            PossibleChunkIndexes.RemoveAt(index);
        }
    }

    public int GetCurrentLevel()
    {
        return CurrentLevel + 1;
    }
}
