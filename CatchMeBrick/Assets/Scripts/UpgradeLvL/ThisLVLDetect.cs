using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThisLVLDetect : MonoBehaviour
{
    GameObject SoundMaster;
    AudioSource ClickSound;

    void Start()
    {
        SoundMaster = GameObject.FindGameObjectWithTag("SoundMaster");
        ClickSound = SoundMaster.GetComponent<AudioSource>();
    }
    int LVL;
    IEnumerator Click()
    {
        yield return new WaitForSeconds(0.1f);
        LVL = int.Parse(gameObject.name);
        SelectLvLScript.LevelDetected = 1;
        SelectLvLScript.ThisIsLvL = LVL;
    }
    public void DetectLVL()
    {
        if(MenuScript.SoundsOnOff)
        {
            ClickSound.Play();
        }
        StartCoroutine(Click());
    }
}
