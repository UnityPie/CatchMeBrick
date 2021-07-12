using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    GameObject SoundMaster;
    AudioSource ClickSound;
    AudioSource MenuSound;
    public Text SoundDetected;

    void OnEnable()
    {
        ShowAd();
        MenuSound = GetComponent<AudioSource>();
        SoundMaster = GameObject.FindGameObjectWithTag("SoundMaster");
        ClickSound = SoundMaster.GetComponent<AudioSource>();
        Cursor.lockState = CursorLockMode.None;
        if(MenuScript.SoundsOnOff)
        {
            SoundDetected.text = "Sound ON";
            MenuSound.Play();
        }
        else
        {
            SoundDetected.text = "Sound OFF";
        }
    }
    IEnumerator toLVLselector()
    {
        MenuScript.Esc = false;
        Time.timeScale = 1;
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("ChoiseLVL");
    }
    public void ShowAd()
    {
        Application.ExternalCall("ShowAd");
    }
    public void NewGame() 
    {
        if(MenuScript.SoundsOnOff)
        {
            ClickSound.Play();
        }
        PlayerPrefs.DeleteAll();
        StartCoroutine(toLVLselector());
    }
    public void Continue() 
    {
        if(MenuScript.SoundsOnOff)
        {
            ClickSound.Play();
        }
        StartCoroutine(toLVLselector());
    }

    public void Sounder()
    {
        MenuScript.SoundsOnOff = !MenuScript.SoundsOnOff;
        if(MenuScript.SoundsOnOff)
        {
            SoundDetected.text = "Sound ON";
            MenuSound.Play();
            ClickSound.Play();
        }
        else
        {
            SoundDetected.text = "Sound OFF";
            MenuSound.Stop();
        }
    }
    public void Quit() 
    {
        if(MenuScript.SoundsOnOff)
        {
            ClickSound.Play();
        }
        Application.Quit();
    }
}
