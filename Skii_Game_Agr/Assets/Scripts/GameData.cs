using UnityEngine;
using System.Collections.Generic;

public class GameData : MonoBehaviour
{
  public List<float> bestTimes = new List<float>();
  private static GameData instance;
  [SerializeField] private string leaderboardKey = "LeaderboardLVL1-";
  private void Awake()
  {
    if (instance != null && instance != this)
    {
      Destroy(gameObject);
    }
    else
    {
      instance = this;
      DontDestroyOnLoad(gameObject);
    }
    LoadLeaderboard();
  }

  public void AddLevelTime(float time)
  {
    bestTimes.Add(time);
    bestTimes.Sort();
    SaveLeaderboard();
  }

  public static GameData Instance
  {
    get {return instance;}
  }

  private void SaveLeaderboard()
  {
    for (int i = 0; i < 5; i++)
    {
      if(i <= bestTimes.Count)
        PlayerPrefs.SetFloat(leaderboardKey + i, bestTimes[i]);
    }
    PlayerPrefs.Save();
  }

  private void LoadLeaderboard()
  {
    for (int i = 0; i < 5; i++)
    {
      float time = PlayerPrefs.GetFloat(leaderboardKey + i, 999.99f);
      bestTimes.Add(time);
    }
    bestTimes.Sort();
  }
}
