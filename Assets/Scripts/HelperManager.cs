using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperManager : MonoBehaviour
{
    // Start is called before the first frame update

	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.tag == "Enemy")
		{
			Destroy(col.gameObject);
			PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") +1 );
            Destroy(this.gameObject);

		} 
    }
}
