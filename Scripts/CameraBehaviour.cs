using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    public float mouseSensitivity = 100;

    float rotX;
    float rotY;

    void Update ()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0,0,5) * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0,0,-5) * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-5,0,0) * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(5,0,0) * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            transform.position += new Vector3(0,-5,0) * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.E))
        {
            transform.position += new Vector3(0,5,0) * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            rotY += mouseX * mouseSensitivity * Time.deltaTime;
            rotX -= mouseY * mouseSensitivity * Time.deltaTime;

            transform.rotation = Quaternion.Euler(rotX, rotY, 0.0f);
        }
	}
}
