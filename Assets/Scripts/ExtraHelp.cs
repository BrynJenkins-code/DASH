using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ExtraHelp : MonoBehaviour
{
    // Start is called before the first frame update
    public static event Action OnItemPickup;
    void Awake()
    {
        Vector3 direction = UnityEngine.Random.insideUnitCircle.normalized;
        GetComponent<Rigidbody2D>().AddForce(GetComponent<Rigidbody2D>().mass * (100 * direction));
    }

    void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.tag == "Player")
		{
            col.gameObject.GetComponent<PowerUpManager>().ExtraHelp(); 
            Destroy(this.gameObject);
            OnItemPickup(); 

		} 
    }
}
