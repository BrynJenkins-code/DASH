using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject panel; 
    public TMP_Text scoreText;
    public GameManager gameManager; 

    public TMP_Text currentScore; 

    public TMP_Text achievementText;

    string path; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentScore.text = PlayerPrefs.GetInt("Score").ToString();
    }

    public void MainMenu(){
        Application.LoadLevel("MainMenu");
    }

    public void SubmitScore(){
        int.TryParse(scoreText.text, out int score);
        gameManager.AddHighscoreEntry(PlayerPrefs.GetString("Name"), score);
        Application.LoadLevel("LeaderboardScene");
    }

    public void PlayAgain(){
        Application.LoadLevel("GameScene");
    }

    public void EnableEndUI(){
        panel.SetActive(true);
        scoreText.text = PlayerPrefs.GetInt("Score").ToString();
    }

    public void Achievement(string achievementName){
        achievementText.GetComponent<TMP_Text>().text = achievementName;
        achievementText.gameObject.SetActive(true); 
        Invoke(nameof(AchievementOver), 5);

    }
    public void AchievementOver(){
        achievementText.gameObject.SetActive(false); 
    }   
}
