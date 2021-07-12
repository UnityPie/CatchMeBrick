using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraForGrenade : MonoBehaviour
{   
    void OnEnable()
    {
        if(!MenuScript.Esc)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
