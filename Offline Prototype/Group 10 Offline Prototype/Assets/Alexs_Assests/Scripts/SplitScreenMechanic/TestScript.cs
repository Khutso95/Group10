using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public Camera cam;
    public float X;
    public float Y;
    public float Width;
    public float Height;
    void Start()
    {
       
    }

    void Update()
    {
        if (Input.GetKey("space"))
        {
            cam.rect = new Rect(X, Y, Width, Height);
        }
    }
}
