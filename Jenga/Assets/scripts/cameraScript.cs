using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{

    float AxisX;
    float AxisY;

    public float distance = 50;
    public float speed = 5f;



    void Start()
    {
        SetCamera();
    }

    
    void Update()
    {

        if (Input.GetMouseButton(1))
            SetCamera();
    }

    void SetCamera()
    {
        float deltaX = Input.GetAxis("Mouse X");
        float deltaY = Input.GetAxis("Mouse Y");

        AxisX += deltaY * speed;
        AxisY += deltaX * speed;


        AxisX = Mathf.Clamp(AxisX, -85, 0);


        var rotation = Quaternion.Euler(AxisX, AxisY, 0);
        transform.position = rotation * Vector3.forward * distance;

        transform.LookAt(Vector3.up * 5f);
    }
}
