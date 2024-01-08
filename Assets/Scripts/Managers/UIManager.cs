using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameObject pauseMenu; //Holds Our Main Menu Object
    private GameObject gameOverMenu;

    void Start()
    {
        GameManager.OnUIToggled += TogglePauseMenu;
    }

    void Update()
    {
        
    }

    public void TogglePauseMenu(GameState newState) //Responsible For Toggling The Pause Menu
    {
        if(newState == GameState.Paused)
        {
            //Toggle On
        } else if (newState == GameState.Playing)
        {
            //Toggle Off
        }
    }

    public void GameOver()
    {

    }
}
