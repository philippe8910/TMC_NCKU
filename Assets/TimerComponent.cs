using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimerComponent : MonoBehaviour
{
    public float timeMax = 99f;
    public float timeCurrent = 0f;
    
    public bool isTimerActive = false;
    
    public UnityEvent OnTimerStart , OnTimerStop, OnTimerReset , OnTimerEnd;

    private void Update()
    {
        if (isTimerActive)
        {
            if (timeCurrent > 0)
            {
                timeCurrent -= Time.deltaTime;
            }
            else
            {
                timeCurrent = 0;
                OnTimerEnd.Invoke();
                isTimerActive = false;
            }
        }
    }

    public void SetTimeMax(float _timeMax)
    {
        timeMax = _timeMax;
        timeCurrent = timeMax;
    }
    
    public void StartTimer()
    {
        isTimerActive = true;
        OnTimerStart.Invoke();
    }
    
    public void StopTimer()
    {
        isTimerActive = false;
        OnTimerStop.Invoke();
    }
    
    public void ResetTimer()
    {
        timeCurrent = timeMax;
        OnTimerReset.Invoke();
    }
    
    
}
