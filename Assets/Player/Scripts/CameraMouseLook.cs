using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouseLook : MonoBehaviour
{
    [SerializeField] public float sensitivity = 2.0f; // Adjust the sensitivity of mouse movement

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Rotate the camera based on mouse movement
        transform.Rotate(Vector3.up, mouseX * sensitivity);
        transform.Rotate(Vector3.left, mouseY * sensitivity);

        // Clamp the vertical rotation to prevent flipping
        float xRotation = transform.eulerAngles.x;
        if (xRotation > 180.0f)
            xRotation -= 360.0f;
        xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);

        // Apply the clamped rotation
        transform.rotation = Quaternion.Euler(xRotation, transform.eulerAngles.y, 0.0f);
    }
}
