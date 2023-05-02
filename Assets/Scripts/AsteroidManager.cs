using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.name == "Planet"){
            col.gameObject.GetComponent<planetManager>().ReduceHealth();
            Destroy(this.gameObject);
        }
    }
}
