using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject Menu;
    public GameObject Instructions;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void OpenInstructions()
    {
        Menu.SetActive(false);
        Instructions.SetActive(true);
    }

    public void BackToMenu()
    {
        Instructions.SetActive(false);
        Menu.SetActive(true);
    }
}
