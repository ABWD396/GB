using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Singleton]
public class TimeManager : MonoBehaviour
{
    private float mainTimer = 0;

    private bool stopTimers = false;

    private List<CallbackTimer> callbackTimers;
    private List<CallbackTimer> callbackTimersToRemove;

    private bool isGamePaused = true;
    private bool isGameFinished = false;

    TimeManager()
    {
        InitManager();
    }
    void Awake()
    {
        PauseGame();
    }

    public void InitManager()
    {
        mainTimer = 0;
        stopTimers = false;
        callbackTimersToRemove = new List<CallbackTimer>();
        callbackTimers = new List<CallbackTimer>();
    }

    public void Handle(float deltaTime)
    {
        mainTimer += Time.deltaTime;

        TimersHandle(deltaTime);
    }

    public float GetMainTimer()
    {
        return mainTimer;
    }

    void TimersHandle(float deltaTime)
    {
        if (stopTimers)
        {
            return;
        }

        callbackTimersToRemove.Clear();

        foreach (CallbackTimer callbackTimer in callbackTimers)
        {
            callbackTimer.GetCallback().Invoke(callbackTimer.timer -= deltaTime);
            if (callbackTimer.timer < 0)
            {
                callbackTimersToRemove.Add(callbackTimer);
            }
        }

        foreach (CallbackTimer callbackTimer in callbackTimersToRemove)
        {
            callbackTimers.Remove(callbackTimer);
        }
    }

    public void AddCallbackTimers(float timer, Action<float> callback)
    {
        callbackTimers.Add(new CallbackTimer(timer, callback));
    }

    public string FormatTime(float cycle, float time)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        return String.Format("{0}D {1}H", cycle, timeSpan.ToString(@"ss"));
    }

    public bool GetIsGamePaused()
    {
        return isGamePaused;
    }

    public bool GetIsTimerStopped()
    {
        return stopTimers;
    }

    public void PauseTimers()
    {
        stopTimers = true;
    }

    public void ResumeTimers()
    {
        stopTimers = false;
    }

    public void PauseGame(bool finished = false)
    {
        Time.timeScale = 0;
        isGamePaused = true;
        isGameFinished = finished;
    }

    public void ResumeGame(bool force = false)
    {
        if(isGameFinished && !force){
            return;
        }
        Time.timeScale = 1;
        isGamePaused = false;
    }

    public void ToggleGame()
    {
        if (isGamePaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

}