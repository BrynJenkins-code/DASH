using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;

public class ProfileManager : MonoBehaviour
{
    string path; 
    public MenuManager menuManager; 
    public TMP_InputField playerName; 
    public Transform entryContainer; 
    public Transform entryTemplate;

    private List<Transform> profileEntryTransformList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake(){
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "Users.json";
        entryTemplate.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void selectProfile(string name){
        PlayerPrefs.SetString("Name", name);
        menuManager.LoggedIn();

    }

    public void addProfile(){
        PlayerInfo newPlayer = new PlayerInfo();
        newPlayer.name = playerName.text; 
        using StreamReader reader = new StreamReader(path);
        string jsonRead = reader.ReadToEnd();
        Players players = JsonUtility.FromJson<Players>(jsonRead);
        reader.Close();

        players.players.Add(newPlayer);

        string json = JsonUtility.ToJson(players);
        using StreamWriter writer = new StreamWriter(path);
        writer.Write(json);
        Debug.Log(json);
        writer.Close();
        PlayerPrefs.SetString("Name", playerName.text);
        menuManager.LoadGame();
    }
    public void LoadUsers(){
        using StreamReader reader = new StreamReader(path);
        string jsonRead = reader.ReadToEnd();
        Players players = JsonUtility.FromJson<Players>(jsonRead);
        profileEntryTransformList = new List<Transform>();
        foreach(PlayerInfo player in players.players){
            Debug.Log("creating user");
            CreateProfileButtons(player, entryContainer, profileEntryTransformList);
        } 
    }
    private void CreateProfileButtons(PlayerInfo player, Transform container, List<Transform> transformList){

        float templateHeight = 80f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);
        GameObject buttonObject = GameObject.Find("ProfileButton");
        buttonObject.transform.GetChild(0).GetComponent<TMP_Text>().text = player.name;
        buttonObject.GetComponent<Button>().onClick.AddListener(() => selectProfile(player.name));
        transformList.Add(entryTransform);
    }
    
    [System.Serializable]
    private class Players{
        public List<PlayerInfo> players = new List<PlayerInfo>();
    }
    [System.Serializable]
    private class PlayerInfo{
        public string name; 
    }
}
