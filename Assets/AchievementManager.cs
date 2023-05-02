using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Text.RegularExpressions;
public class AchievementManager : MonoBehaviour
{
    string path; 
    public Transform entryContainer; 
    public Transform entryTemplate;
    public Transform statsContainer; 
    AchievementList achievements;
    private List<Transform> achievementEntryTransformList;    
    void Awake()
    {
        string playerName = Regex.Replace(PlayerPrefs.GetString("Name"), @"\s+", "");
        path = Application.dataPath + Path.AltDirectorySeparatorChar + playerName + ".json";
        entryTemplate.gameObject.SetActive(false);
        using StreamReader reader = new StreamReader(path);
        string json = reader.ReadToEnd();
        achievements = JsonUtility.FromJson<AchievementList>(json);
        achievementEntryTransformList = new List<Transform>();
        PopulateStats();
        foreach (Achievement achievement in achievements.killTotalAchievements) {
            CreateAchievementEntryTransform(achievement, entryContainer, achievementEntryTransformList);
            
        }
        foreach (Achievement achievement in achievements.killSessionAchievements) {
            CreateAchievementEntryTransform(achievement, entryContainer, achievementEntryTransformList);
            
        }
        foreach (Achievement achievement in achievements.collectAchievements) {
            CreateAchievementEntryTransform(achievement, entryContainer, achievementEntryTransformList);
            
        }
    }

    public void PopulateStats(){
        statsContainer.Find("time").GetComponent<TMP_Text>().text = achievements.timeSurvived.ToString();
        statsContainer.Find("points").GetComponent<TMP_Text>().text = achievements.points.ToString();
        statsContainer.Find("pointsInLife").GetComponent<TMP_Text>().text = achievements.pointsInSession.ToString();
        statsContainer.Find("powerUps").GetComponent<TMP_Text>().text = achievements.pickUps.ToString();
        statsContainer.Find("deaths").GetComponent<TMP_Text>().text = achievements.deaths.ToString();
    }
    private void CreateAchievementEntryTransform(Achievement achievement, Transform container, List<Transform> transformList){

        float templateHeight = 50f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);
        entryTransform.Find("nameText").GetComponent<TMP_Text>().text = achievement.name;
        if(achievement.completed){
            entryTransform.Find("completeText").GetComponent<TMP_Text>().text = "Yes" ;
        } else{
            entryTransform.Find("completeText").GetComponent<TMP_Text>().text = "No" ;
        }
        entryTransform.Find("requiredText").GetComponent<TMP_Text>().text = achievement.requiredAmount.ToString();
        transformList.Add(entryTransform);


    }
    public void MainMenu(){
        Application.LoadLevel("MenuScene");
    }

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


    // Update is called once per frame

}
