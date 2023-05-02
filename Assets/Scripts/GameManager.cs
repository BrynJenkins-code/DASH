using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
public class GameManager : MonoBehaviour
{
    public static event Action<float> OnGameEnd; 
    string path; 
    public UIManager uIManager; 

    float startTime; 


  private void Awake() {
    path = Application.dataPath + Path.AltDirectorySeparatorChar + "Leaderboard.json";
    startTime = Time.time; 

  }

    public void AddHighscoreEntry( string name, int score) {
        // Create HighscoreEntry

        Debug.Log(name + score); 
        ScoreData highscoreEntry = new ScoreData{ name = name , score = score };
        
        // Load saved Highscores
        using StreamReader reader = new StreamReader(path);
        string jsonRead = reader.ReadToEnd();

        reader.Close();
        
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonRead);

        if (highscores == null) {
            // There's no stored table, initialize
            highscores = new Highscores() {
                leaderboard = new List<ScoreData>()
            };
        }

        // Add new entry to Highscores
        highscores.leaderboard.Add(highscoreEntry);

        // Save updated Highscores
        string json = JsonUtility.ToJson(highscores);
        using StreamWriter writer = new StreamWriter(path);
        writer.Write(json);
        Debug.Log(json);
        writer.Close();
    }
    public void EndGame(){
        uIManager.EnableEndUI();
        OnGameEnd(Time.time - startTime);



    }
    [System.Serializable]    
    private class Highscores {
        public List<ScoreData> leaderboard;
    }

  
}
