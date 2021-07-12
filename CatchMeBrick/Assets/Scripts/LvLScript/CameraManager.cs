using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject Teacher;
    public float mouseSensivity = 100f;

    public Transform Character;

    private float xRotation = 0f;

    private void OnEnable()
    {
        if(!MenuScript.Esc && Time.timeScale != 0)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        Character.Rotate(Vector3.up * mouseX);
    }
}
