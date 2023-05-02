using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planetManager : MonoBehaviour
{
    public int planetHealth;

    public GameManager gameManager; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (planetHealth < 0 ){
            gameManager.EndGame();
        }
    }

    public void ReduceHealth(){
        planetHealth = planetHealth - 10; 
    }
}
