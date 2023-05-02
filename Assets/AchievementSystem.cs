using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 
using System.IO;
using System.Text.RegularExpressions;
public class AchievementSystem : MonoBehaviour
{
    string path;

    string defaultPath;  
    AchievementList achievements; 
    public UIManager uIManager; 
    Achievement currentTotalKillAchievement; 
    Achievement currentSessionKillAchievement;

    Achievement currentCollectAchievement; 
    int points; 
    int pointsInSession; 
    int pickUps; 
    //float timeSurvived; 

    // Start is called before the first frame update
    void Awake()
    {
        defaultPath = Application.dataPath + Path.AltDirectorySeparatorChar + "Achievements.json";
        string playerName = Regex.Replace(PlayerPrefs.GetString("Name"), @"\s+", "");
        path = Application.dataPath + Path.AltDirectorySeparatorChar + playerName + ".json";
        CheckUserExists();
        LoadAchievements();
        GameManager.OnGameEnd += GameEnd; 
        PlayerManager.OnEnemyKill += ScoreAdded; 
        SpeedUp.OnItemPickup += ItemCollected; 
        ExtraHelp.OnItemPickup += ItemCollected; 

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void CheckUserExists(){
        if(!File.Exists(path)){
            using StreamReader reader = new StreamReader(defaultPath);
            string jsonRead = reader.ReadToEnd();
            Debug.Log(jsonRead);
            reader.Close();

            using StreamWriter writer = new StreamWriter(path);
            writer.Write(jsonRead);
            writer.Close();

        }
    }

    private void ScoreAdded(int scoreToBeAdded){
        points += scoreToBeAdded; 
        pointsInSession += scoreToBeAdded; 
        if (currentSessionKillAchievement.requiredAmount <= pointsInSession){
            Unlock(currentSessionKillAchievement);
        }
        if (currentSessionKillAchievement.requiredAmount <= pointsInSession){
            Unlock(currentTotalKillAchievement);
        }
    }
    private void ItemCollected(){
        pickUps++; 
        if(currentCollectAchievement.requiredAmount <= pickUps){
            Unlock(currentCollectAchievement);
        }
    }

    private void GameEnd(float time){
        if(pointsInSession > achievements.pointsInSession){
            achievements.pointsInSession = pointsInSession;
        }
        if(time > achievements.timeSurvived){
            achievements.timeSurvived = time; 
        }
        achievements.deaths++; 
        achievements.points = points;
        achievements.pickUps = pickUps; 
    }

    private void Unlock(Achievement achievement){
        achievement.completed = true; 

        SaveAchievements(); 
        UpdateAchievements();
        uIManager.Achievement(achievement.name);
        //Some UI thing
    }
    private void LoadAchievements(){
        using StreamReader reader = new StreamReader(path);
        string jsonRead = reader.ReadToEnd();
        Debug.Log(jsonRead);
        reader.Close();
        achievements = JsonUtility.FromJson<AchievementList>(jsonRead);
        Debug.Log(achievements.killSessionAchievements.Count);
        points = achievements.points; 
        pickUps = achievements.pickUps; 
        UpdateAchievements();
    }
    private void UpdateAchievements(){
        Debug.Log("updating achievements");
        foreach(Achievement achievement in achievements.killSessionAchievements){
            if(achievement.completed != true){
                currentSessionKillAchievement = achievement; 
                break;
                Debug.Log(currentSessionKillAchievement.name);
            }
        }
        foreach(Achievement achievement in achievements.killTotalAchievements){
            if(achievement.completed != true){
                currentTotalKillAchievement = achievement; 
                break;
            }
        }
        foreach(Achievement achievement in achievements.collectAchievements){
            if(achievement.completed != true){
                currentCollectAchievement = achievement; 
                break;
            }
        }
    }

    private void SaveAchievements(){
        string json = JsonUtility.ToJson(achievements);
        using StreamWriter writer = new StreamWriter(path);
        writer.Write(json);
        Debug.Log(json);
        writer.Close();
    }

    [System.Serializable]
    private class AchievementList{
        public int points; 
        public int pointsInSession; 
        public int deaths; 
        public int pickUps; 
        public float timeSurvived; 
        public List<Achievement> killTotalAchievements = new List<Achievement>();
        public List<Achievement> killSessionAchievements = new List<Achievement>();
        public List<Achievement> collectAchievements = new List<Achievement>();
    }
    [System.Serializable]
    private class Achievement{
        public string name; 
        public bool completed; 

        public int requiredAmount;
        public Achievement(string name, bool completed, int requiredAmount){
            name = name; 
            completed = completed;
            requiredAmount = requiredAmount;
        }
    }
}
