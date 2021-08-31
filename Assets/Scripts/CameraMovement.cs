using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 dir = new Vector3(0, 0, 0);
    private float moveSpeed = 40f;
    private float moveAngle = 0;
    private float rotX;

    private void FixedUpdate()
    {
        KeyboardRotation();
        KeyboardMovement();
    }

    void KeyboardRotation()
    {
        if (Input.GetKey(KeyCode.E))
        {
            moveAngle = -0.5f;
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            moveAngle = 0.5f;
        }
        else
        {
            moveAngle = 0;
        }
           
        transform.eulerAngles = new Vector3(rotX, transform.eulerAngles.y + moveAngle, 0);
    }

    void KeyboardMovement()
    {       
        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");

        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }
}
