using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    public void LoadRestArea()
    {
        //SceneManager.LoadScene("");
    }

    public void LoadNextLevel() //Loads Next Level
    {
        List <int> completedLevels = PlayerPreferencesManager.GetCompletedLevels();
        string nextLevelName = FindNextLevel(completedLevels).ToString();
        SceneManager.LoadScene(nextLevelName);
    }

    private int FindNextLevel(List <int> completedLevels) //Finds The Next Level
    {
        int maximumCompletedLevels = (completedLevels.Count > 0) ? completedLevels.Max() : 0;
        int nextLevel = maximumCompletedLevels + 1;

        return nextLevel;
    }

    private string GetSceneName() //Retrieves Name Of Scene
    {
        string sceneName = SceneManager.GetActiveScene().name;
        return sceneName;
    }
}
