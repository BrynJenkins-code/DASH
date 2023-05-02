using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour
{
	public static event Action<int> OnEnemyKill; 
	public int playerHealth; 
	
    // Start is called before the first frame update


	//Managing collisions 
	/*
		Enemy damage should be managed in it's own script

		Player damage should be based somewhat on speed. 
	*/ 
	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.tag == "Enemy")
		{
			Destroy(col.gameObject);
			PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") +1 );
			OnEnemyKill(1); 

		} 
	}
}
