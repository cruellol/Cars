using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ui : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _playerName;
    [SerializeField]
    private TMP_Text _leaderBoard;

    public static string CurrentPlayer;

    public static string FilePath = "Leaders.txt";
    public static List<Score> AllScores = new List<Score>();
    public void OnStart()
    {
        CurrentPlayer = _playerName.text;
        SceneManager.LoadScene(sceneName: "Cars");
        return;
    }
    public void OnQuit()
    {
        if (Application.isEditor)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }
    }
    private void Start()
    {
        UpdateScores();
    }

    private void UpdateScores()
    {
        if (File.Exists(FilePath))
        {
            AllScores.Clear();
            string allRecordsString = File.ReadAllText(FilePath);
            AllScores = JsonConvert.DeserializeObject<List<Score>>(allRecordsString);
        }

        StringBuilder sb = new StringBuilder();
        if (AllScores != null)
        {
            foreach (var score in AllScores.OrderBy(s=>s.RaceTime).Take(10))
            {
                sb.Append(score.RaceTime.ToString(@"mm\:ss\:f")).Append(" ").Append(score.Name).AppendLine();
            }
        }
        else
        {
            AllScores = new List<Score>();
        }
        _leaderBoard.text = sb.ToString();
    }
}

[System.Serializable]
public class Score
{
    public string Name;
    public TimeSpan RaceTime;

    public Score()
    {

    }

    public Score(string name, TimeSpan raceTime)
    {
        Name = name;
        RaceTime = raceTime;
    }
}
