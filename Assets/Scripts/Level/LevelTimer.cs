using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] private float levelReward = 10000f;
    [SerializeField] private float timeInterval = 1;
    [SerializeField] private float timeIntervalCost = 1;

    private float completedTimeIntervals= 0;

    IEnumerator timerRoutine;

    void Start()
    {
        //Subscribe to Game manager

        timerRoutine = StartLevelTimer();

        StartCoroutine(timerRoutine);
    }

    private void OnGameFinished()
    {
        StopCoroutine(timerRoutine);
    }

    public float GetRemainingReward()
    {
        return Mathf.Clamp(levelReward - completedTimeIntervals * timeIntervalCost, 0, levelReward);
    }


    private IEnumerator StartLevelTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeInterval);
            completedTimeIntervals++;
        }
    }
}
