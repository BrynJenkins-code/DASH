using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker : MonoBehaviour
{

    /*
        When score is set up, link score/time played to number of enemies and the acceleration range. 
    */
    public GameObject[] targetArr; 
    public string targetTag; 
    Transform target; 
    Vector3 targetPos;
    public float acceleration; 

    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        targetArr = GameObject.FindGameObjectsWithTag(targetTag);
        if (targetArr != null){
            target = GetClosestEnemy(targetArr);
            targetPos = target.transform.position;

            Vector3 direction = (targetPos - transform.position).normalized;
            GetComponent<Rigidbody2D>().AddForce(GetComponent<Rigidbody2D>().mass * (acceleration * direction));
        }

    }
    Transform GetClosestEnemy(GameObject[] targets)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in targets)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t.transform;
                minDist = dist;
            }
        }

        return tMin;
    }
}
