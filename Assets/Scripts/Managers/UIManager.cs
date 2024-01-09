using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameObject pauseMenu; //Holds Our Main Menu Object
    private GameObject gameOverMenu; //Holds Our Game Over Screen

    void Start()
    {
        GameManager.OnGamePlay += TogglePauseMenuOff;
        GameManager.OnGamePaused += TogglePauseMenuOn;
    }

    void Update()
    {
        
    }

    public void TogglePauseMenuOff() //Responsible For Toggling The Pause Menu
    {
        pauseMenu.SetActive(false);
    }

    public void TogglePauseMenuOn()
    {
        pauseMenu.SetActive(true);
    }

    public void GameOver()
    {

    }
}
