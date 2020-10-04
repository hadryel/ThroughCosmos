using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    public GameObject LevelManager;
    public Text ReplayMessage;
    public Text ReplayButtonText;

    void Start()
    {

    }

    void Update()
    {

    }

    public void StartGame()
    {
        //gameObject.SetActive(false);
        SceneManager.LoadScene("Game");
    }

    public void SetupWin()
    {
        GameObject dmgo = GameObject.Find("DataManager");
        dmgo.GetComponent<DataManager>().CurrentLevel++;

        ReplayMessage.text = "You win!";
        ReplayButtonText.text = "Continue";

        DontDestroyOnLoad(dmgo);
        gameObject.SetActive(true);
    }

    public void SetupDefeat()
    {
        ReplayMessage.text = "You lost. Reached level " + LevelManager.GetComponent<LevelManager>().GetCurrentLevel();
        ReplayButtonText.text = "Replay";

        GameObject dmgo = GameObject.Find("DataManager");
        if (dmgo != null)
            Destroy(dmgo);

        gameObject.SetActive(true);
    }
}
