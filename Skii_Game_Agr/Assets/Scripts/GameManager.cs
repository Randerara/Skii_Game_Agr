using System;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using TMPro;

public class GameManager : MonoBehaviour
{
    public delegate void TimerEvent();

    private DateTime raceStart;
    private TimeSpan raceTime;
    private TimeSpan penaltyTime;
    private TimeSpan bestTime;
    private bool raceFinish = false;
    private bool racing = false;
    [SerializeField] private TMP_Text timer, bestTimeText;
    [SerializeField] private string bestTimeKey = "BestTimeLVL1";

    private void Start()
    {
        int bestTimeInt = PlayerPrefs.GetInt(bestTimeKey, int.MaxValue);
        bestTime = new TimeSpan(bestTimeInt);
        bestTimeText.text = "BEST: " + bestTime.ToString("mm\\:ss");
    }

    private void OnEnable()
    {
        FinishGate.FinishRace += FinishRace;
        StartGate.StartRace += StartRace;
        SlalomFlag.RacePenalty += RacePenalty;
    }

    void FinishRace()
    {
        racing = false;
        Debug.Log("Finish Race");
        if (raceTime < bestTime)
        {
            bestTimeText.text = "BEST: " + bestTime.ToString("mm\\:ss");
            PlayerPrefs.SetInt(bestTimeKey, (int)raceTime.Ticks);
            PlayerPrefs.Save();
        }
    }

    void StartRace()
    {
        racing = true;
        raceStart = DateTime.Now;
        Debug.Log("Start Race");
    }

    void RacePenalty()
    {
        penaltyTime += new TimeSpan(0, 0, 3);
    }

    void Update()
    {
        if(racing) 
            raceTime = DateTime.Now - raceStart + penaltyTime;
        Debug.Log("Race time: " + raceTime);
        timer.text = "TIME: " + raceTime.ToString("mm\\:ss");
    }
}
