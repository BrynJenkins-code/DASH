using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject panel; 

    public Button achievementButton; 
    public ProfileManager profileManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGame(){
        Application.LoadLevel("GameScene");
        PlayerPrefs.SetInt("Score", 0);
    }

    public void LogIn(){
        panel.SetActive(true);
        profileManager.LoadUsers();
    }

    public void LoggedIn(){
        panel.SetActive(false);
        achievementButton.gameObject.SetActive(true);
    }

    public void LoadAchievements(){
        Application.LoadLevel("AchievementsScene");
    }

    public void LoadLeaderboard(){
        Application.LoadLevel("LeaderboardScene");
    }
}
