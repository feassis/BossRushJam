using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /* Game State Change Events */
    public delegate void GameStateHandler(); //Game State Event
    public static event GameStateHandler OnGamePaused; //Event For Pause
    public static event GameStateHandler OnGamePlay; //Event For Play
    public static event GameStateHandler OnGameOver; //Event For Game Over
    public static event GameStateHandler OnLevelCompleted; //Event For Completed Level

    private GameState currentGameState; //Enum References

    private static GameManager instance; //Singleton Instance
    
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

    public void ChangeGameState(GameState newState) //Responsible For Changing GameState
    {
        if(currentGameState != newState)
        {
            currentGameState = newState; //Changes Current Game State

            GameStateEvents(currentGameState); //Calls GameStateEvents function to run events based on current state
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
            AdjustTimeScale(currentGameState); //Adjust Game Time Flow
            //Resume Game Time
            OnGamePlay?.Invoke();
            break;

            case GameState.Paused:
            AdjustTimeScale(currentGameState); //Adjust Game Time Flow  
            //Pause Game Time          
            OnGamePaused?.Invoke();
            break;

            case GameState.GameOver:
            AdjustTimeScale(currentGameState); //Adjust Game Time Flow
            OnGameOver?.Invoke();
            break;

            default:
            Debug.LogWarning("There Has Been An Issue With Triggering Events For The Game State");
            break;
        }
    }
}

