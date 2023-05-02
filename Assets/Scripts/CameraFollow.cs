using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;

    public float smoothspeed = 0.125f;

    public Vector3 offset; 

    void FixedUpdate(){
        Vector3 desiredPosition = target.position + offset; 
        Vector3 smoothePosition = Vector3.Lerp(transform.position, desiredPosition, smoothspeed);
        transform.position = smoothePosition; 
    }
}
