using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitController : MonoBehaviour
{
    public float a;
    public float b;
    public float x;
    public float y;
    public float alpha;
    public float X;
    public float Y;
    public float Power; 

    public GameObject anotherObject;

    void Update()
    {
        alpha += 10;
        X = x + (a * Mathf.Cos((float)(alpha * .005)));
        Y = y + (b * Mathf.Sin((float)(alpha * .005)));
        this.gameObject.transform.position = anotherObject.transform.position + new Vector3(X,0, Y);
    }
}

