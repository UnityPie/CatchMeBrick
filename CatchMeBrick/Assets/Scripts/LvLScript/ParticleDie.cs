using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDie : MonoBehaviour
{
    AudioSource Sounds;
    void Start()
    {
        if(MenuScript.SoundsOnOff)
        {
            Sounds = GetComponent<AudioSource>();
            Sounds.Play();
        }
        Destroy(gameObject,0.5f);
    }
}
