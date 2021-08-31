using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public enum Axis
    {
        XZ,
        XY,
    }

    [SerializeField] private Axis axis = Axis.XZ;
    private float moveSpeed = 40f;
    //[SerializeField] private Vector3 rotation = new Vector3(25,0,0);
    float moveAngle = 0;

    public float turnSpeed = 4.0f;
    public float minTurnAngle = -90.0f;
    public float maxTurnAngle = 90.0f;
    private float rotX;

    private Vector3 moveDir;

    private void FixedUpdate()
    {
        MouseAiming();
        KeyboardMovement();
    }

    void MouseAiming()
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
            moveAngle = 0;

        transform.eulerAngles = new Vector3(rotX, transform.eulerAngles.y + moveAngle, 0);
    }
    void KeyboardMovement()
    {
        Vector3 dir = new Vector3(0, 0, 0);
        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");
        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }
}
