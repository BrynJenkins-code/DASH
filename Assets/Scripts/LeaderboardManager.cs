using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class LeaderboardManager : MonoBehaviour
{
    // Start is called before the first frame update
    
    string path; 

    public Transform entryContainer; 
    public Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;

    void Awake(){
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "Leaderboard.json";
        //entryTemplate = entryContainer.Find("highscoreEntryTemplate");
        entryTemplate.gameObject.SetActive(false);

        using StreamReader reader = new StreamReader(path);
        string json = reader.ReadToEnd();
        Highscores highscores = JsonUtility.FromJson<Highscores>(json);

        // Sort entry list by Score
        for (int i = 0; i < highscores.leaderboard.Count; i++) {
            for (int j = i + 1; j < highscores.leaderboard.Count; j++) {
                if (highscores.leaderboard[j].score > highscores.leaderboard[i].score) {
                    // Swap
                    ScoreData tmp = highscores.leaderboard[i];
                    highscores.leaderboard[i] = highscores.leaderboard[j];
                    highscores.leaderboard[j] = tmp;
                }
            }
        }
        highscoreEntryTransformList = new List<Transform>();
        foreach (ScoreData highscoreEntry in highscores.leaderboard) {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
            
        }
    }

     private void CreateHighscoreEntryTransform(ScoreData highscoreEntry, Transform container, List<Transform> transformList) {
        float templateHeight = 31f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank) {
        default:
            rankString = rank + "TH"; break;

        case 1: rankString = "1ST"; break;
        case 2: rankString = "2ND"; break;
        case 3: rankString = "3RD"; break;
        }

        entryTransform.Find("posText").GetComponent<TMP_Text>().text = rankString;

        int score = highscoreEntry.score;

        entryTransform.Find("scoreText").GetComponent<TMP_Text>().text = score.ToString();

        string name = highscoreEntry.name;
        entryTransform.Find("nameText").GetComponent<TMP_Text>().text = name;

        // Set background visible odds and evens, easier to read
        //entryTransform.Find("background").gameObject.SetActive(rank % 2 == 1);
        
        // Highlight First
        if (rank == 1) {
            entryTransform.Find("posText").GetComponent<TMP_Text>().color = Color.green;
            entryTransform.Find("scoreText").GetComponent<TMP_Text>().color = Color.green;
            entryTransform.Find("nameText").GetComponent<TMP_Text>().color = Color.green;
        }
        transformList.Add(entryTransform);
    }

    public void MainMenu(){
        Application.LoadLevel("MenuScene");
    }
    
    private class Highscores {
        public List<ScoreData> leaderboard;
    }
    

}
