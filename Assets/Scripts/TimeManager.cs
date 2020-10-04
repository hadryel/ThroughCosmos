using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public ResultManager ResultManager;

    public static float RemainingTime = 0f;
    float StartTime;

    public Color WarningColor;
    public Color DangerColor;

    public Text TimeText;

    void Start()
    {
        StartTime = RemainingTime;
    }

    void Update()
    {
        RemainingTime -= Time.deltaTime;

        if (RemainingTime <= 0)
            LoseGame();

        UpdateTimeColor();
        UpdateTimeText();
    }

    public void LoseGame()
    {
        Time.timeScale = 0;
        ResultManager.SetupDefeat();
    }

    public void UpdateTimeText()
    {
        float minutes = Mathf.FloorToInt(RemainingTime / 60);
        float seconds = Mathf.FloorToInt(RemainingTime % 60);

        if (RemainingTime <= 0)
            TimeText.text = "00 00";
        else
            TimeText.text = minutes.ToString("00") + " " + seconds.ToString("00");
    }

    public void UpdateTimeColor()
    {
        if (RemainingTime <= StartTime / 4)
            TimeText.color = DangerColor;
        else if (RemainingTime <= StartTime / 2)
            TimeText.color = WarningColor;
    }

    public static void AddTime(float amount)
    {
        RemainingTime += amount;
    }
}
