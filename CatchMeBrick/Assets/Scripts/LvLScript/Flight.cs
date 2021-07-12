using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flight : MonoBehaviour
{
    Vector3 Pos;
    void Start()
    {
        Pos = transform.position;
    }
    void FixedUpdate()
    {
        if(Vector3.Distance(transform.position,Pos) > 3000)
        {
            transform.position += transform.right * Time.deltaTime * 100f;
            Destroy(gameObject,5f);
        }
        else
        {
           transform.position += transform.right * Time.deltaTime * 10f; 
        }
        
    }
}
