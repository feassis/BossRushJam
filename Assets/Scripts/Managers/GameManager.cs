using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /* Game State Change Events */
    public delegate void UIToggleHandler(GameState newState); // UI Toggle Delegate
    public static event UIToggleHandler OnUIToggled; // UI Toggle Event

    private GameState currentGameState;

    private static GameManager instance;
    
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    private void OnDestroy()
    {
        if(instance == this)
        {
            instance = null;
        }
    }

    private void Start()
    {

    }

    public void ChangeGameState(GameState newState) //Responsible For Changing GameState
    {
        if(currentGameState != newState)
        {
            currentGameState = newState; //Changes Current Game State

            GameStateEvents(currentGameState);
        }
    }

    private void AdjustTimeScale(GameState newState) //Responsible For Adjusting The Time Scale Of The Game
    {
        Time.timeScale = (newState == GameState.Paused || newState == GameState.GameOver) ? 0f : 1f;
    }

    private void GameStateEvents(GameState currentGameState) //Handles Events Based On Current GameState
    {
        switch(currentGameState)
        {
            case GameState.Playing:
            AdjustTimeScale(currentGameState);
            OnUIToggled?.Invoke(currentGameState);
            break;

            case GameState.Paused:
            AdjustTimeScale(currentGameState);
            OnUIToggled?.Invoke(currentGameState);
            break;

            case GameState.GameOver:
            AdjustTimeScale(currentGameState);
            break;

            default:
            Debug.LogWarning("There Has Been An Issue With Triggering Events For The Game State");
            break;
        }
    }
}

