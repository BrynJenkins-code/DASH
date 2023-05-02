using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{

    Dash dash; 

    public ObjectSpawner helperSpawner; 
    public int powerUpDuration; 

    // Start is called before the first frame update
    void Awake()
    {   
        helperSpawner.enabled = false; 
        dash = GetComponent<Dash>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpeedUp(){
        dash.power = dash.power *2; 
        Invoke(nameof(ResetSpeed), powerUpDuration);
    }

    public void ResetSpeed(){
        dash.power = dash.power/2;
    }

    public void ExtraHelp(){
        helperSpawner.enabled= true; 
        Invoke(nameof(ResetHelp), powerUpDuration);
    }

    public void ResetHelp(){
        helperSpawner.StopSpawning();
    }
}
