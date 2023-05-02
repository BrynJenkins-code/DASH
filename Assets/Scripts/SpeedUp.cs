using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpeedUp : MonoBehaviour
{
    public static event Action OnItemPickup;
    // Start is called before the first frame update
    void Awake()
    {
        Vector3 direction = UnityEngine.Random.insideUnitCircle.normalized;
        GetComponent<Rigidbody2D>().AddForce(GetComponent<Rigidbody2D>().mass * (100 * direction));
    }

    void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.tag == "Player")
		{
            col.gameObject.GetComponent<PowerUpManager>().SpeedUp(); 
            Destroy(this.gameObject);
            OnItemPickup();

		} 
    }

}
