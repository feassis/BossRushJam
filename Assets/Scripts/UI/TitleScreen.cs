using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private string menuSceneName;

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            GoToMainMenu();
        }
    }

    private void GoToMainMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }
}
