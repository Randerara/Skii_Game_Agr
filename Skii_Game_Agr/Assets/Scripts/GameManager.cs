using System;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class GameManager : MonoBehaviour
{
    public delegate void TimerEvent();

    private DateTime raceStart;
    private TimeSpan raceTime;
    private bool raceFinish = false;
    private bool racing = false;
    
    private void OnEnable()
    {
        FinishGate.FinishRace += FinishRace;
        StartGate.StartRace += StartRace;
    }

    void FinishRace()
    {
        raceFinish = false;
        Debug.Log("Finish Race");
    }

    void StartRace()
    {
        racing = true;
        raceStart = DateTime.Now;
        Debug.Log("Start Race");
    }

    void Update()
    {
     if(racing) 
         raceTime = DateTime.Now - raceStart;
     Debug.Log("Race time: " + raceTime);
    }
}
