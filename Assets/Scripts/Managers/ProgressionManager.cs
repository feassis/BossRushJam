using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //USING THIS TEMPORARILY

public class ProgressionManager : MonoBehaviour
{
    private void Awake()
    {
        GameManager.OnLevelCompleted += LevelComplete; //Subscribes The Level Complete Function To This Event
    }


    private void LevelComplete() //Responsible For Handling Level Completion Code
    {
        string levelName = SceneManager.GetActiveScene().name;
        int levelNumber = int.Parse(levelName);
        PlayerPreferencesManager.MarkLevelCompleted(levelNumber); //Adds This Level Number To The List Of Completed Levels
    }
}
