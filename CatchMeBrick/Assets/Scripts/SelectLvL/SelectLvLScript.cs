using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectLvLScript : MonoBehaviour
{
    GameObject SoundMaster;
    AudioSource ClickSound;
    public GameObject[] Pass;
    public Image[] LVLStars;
    public Sprite[] Stars;
    GameObject[] FINDLVL;
    AudioSource Music;
    public static int ThisIsLvL;
    public static int LevelDetected;
    public int[] LVL;
    public int[] UNPASS;
    public void ShowAd()
    {
        Application.ExternalCall("ShowAd");
    }
    void OnEnable()
    {
        Music = GetComponent<AudioSource>();
        if(MenuScript.SoundsOnOff)
        {
            Music.Play();
        }
        ShowAd();
        SoundMaster = GameObject.FindGameObjectWithTag("SoundMaster");
        ClickSound = SoundMaster.GetComponent<AudioSource>();
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        FINDLVL = GameObject.FindGameObjectsWithTag("Level");
        for (int i = 0; i < FINDLVL.Length; i++)
        {
            UNPASS[i] = PlayerPrefs.GetInt((i).ToString() + "UnPass");
        }
        for (int i = 0; i < FINDLVL.Length; i++)
        {
            LVL[i] = PlayerPrefs.GetInt((i).ToString() + "LVL");
            LVLStars[i].sprite = Stars[LVL[i]];
        } 
        for (int i = 0; i < FINDLVL.Length; i++)
        {
            if(LVL[i] > 1)
            {
                if(i + 1 < FINDLVL.Length)
                {
                    Pass[i + 1].SetActive(false);
                    PlayerPrefs.SetInt((i + 1).ToString() + "UnPass", 1);
                }
            }
        }
        for (int i = 0; i < FINDLVL.Length; i++)
        {
            if(UNPASS[i] > 0)
            {
                Pass[i].SetActive(false);
            }
        }
        Pass[0].SetActive(false);
    }
    IEnumerator Main()
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("MainMenu");
    }
    IEnumerator Upgraid()
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("UpgraidMenu");
    }
    public void ToMainMenu()
    {
        StartCoroutine(Main());
        if(MenuScript.SoundsOnOff)
        {
            ClickSound.Play();
        }
    }
    public void ToUpgrade()
    {
        StartCoroutine(Upgraid());
        if(MenuScript.SoundsOnOff)
        {
            ClickSound.Play();
        }
    }
    void Update()
    {
        if(LevelDetected != 0)
        {
            if(!Pass[ThisIsLvL].activeSelf)
            {
                SceneManager.LoadScene("LvL (1)");
            }
        }
    }
}
