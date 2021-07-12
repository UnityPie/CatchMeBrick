using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{   
    public static bool SoundsOnOff;
    public float Power = 3f;
    public static float BrickDamageRadius = 5;
    public static float Force = 300;
    public static float BeSlowRadius = 4;
    public static float SlowTimeYellowBrick = 2f;
    public static float TimeToDestroyRedShard = 20f;
    public float timer;
    public static float SlowPower = 1.5f;
    public static float speed = 8f;
    public static float jumpHight = 2f;
    public static int Credits;
    public static int EnemyDamage = 10;
    public static int RedDamage = 20;
    public static int GreenDamage = 10;
    public static int Shards = 20;
    public int MinTimeForAttack = 4, MaxTimeForAttack = 7;
    public int EvilMixerBrickCount = 5;
    public static int Armor;
    public int RedBombCount,GreenBombCount,YellowBombCount;
    public static int ShardsDamage = 0;
    public static bool Esc = false;
    public AudioSource BattleSound;
    public GameObject Cam1;
    public GameObject Cam2;
    public GameObject Cam3;
    public GameObject MenuImage;
    public GameObject WarningText;
    public Text RedText;
    public Text GreenText;
    public Text YellowText;
    public Text CreditsText;
    public Material[] Invisible;
    public Material[] Visible;
    GameObject[] Enemy;
    GameObject[] EnemyXY;
    GameObject[] EvilMixer;
    GameObject[] Slowler;
    public GameObject[] Stars;
    public Text TotalText;
    public Text CreditsOnLvLText;
    public GameObject LvLCompliteMenu;
    public Text Timer;
    Health HP;
    GameObject Player;
    public GameObject WastedBanner;
    public Transform[] EnemyPoints;
    public GameObject[] Dekor;
    public GameObject[] EnemysPref;
    GameObject Pref;
    Health EHP;
    public GameObject ReclameButton;
    GameObject SoundMaster;
    public AudioSource WinnSound;
    public AudioSource FailSound;
    public GameObject Tacher;
    public int EnemyCount;
    public int AllCredits;
    bool Lock = false;
    bool Lock2 = false;
    bool WaitLock = false;
    public static int StarsCount;
    public void ShowAdReward()
    {
        Application.ExternalCall("ShowAdReward");
    }
    void Start()
    {
        BattleSound = GetComponent<AudioSource>();
        if(MenuScript.SoundsOnOff)
        {
            BattleSound.Play();
        }
        SoundMaster = GameObject.FindGameObjectWithTag("SoundMaster");
        AllCredits = 0;
        Player = GameObject.FindGameObjectWithTag("Player");
        HP = Player.GetComponent<Health>();
        Credits = PlayerPrefs.GetInt("Money");
        CreditsText.text = "Credits: " + Credits.ToString();

        MenuScript.jumpHight = PlayerPrefs.GetFloat("jumpHIGHT");
        if(jumpHight == 0)
        {
            jumpHight = 2;
        }
        MenuScript.speed = PlayerPrefs.GetFloat("PlayerSpeed");
        if(speed == 0)
        {
            speed = 8;
        }
        MenuScript.Armor = PlayerPrefs.GetInt("PlayerArmor");
        if(Armor == 0)
        {
            Armor = 0;
        }
        MenuScript.Force = PlayerPrefs.GetFloat("GreenForce");
        if(Force == 0)
        {
            Force = 300;
        }
        MenuScript.BrickDamageRadius = PlayerPrefs.GetFloat("GreenRadius");
        if(BrickDamageRadius == 0)
        {
            BrickDamageRadius = 5;
        }
        MenuScript.GreenDamage = PlayerPrefs.GetInt("GreenDamage");
        if(GreenDamage == 0)
        {
            GreenDamage = 10;
        }
        MenuScript.RedDamage = PlayerPrefs.GetInt("RedDamage");
        if(RedDamage == 0)
        {
            RedDamage = 20;
        }
        MenuScript.Shards = PlayerPrefs.GetInt("RedShardCount");
        if(Shards == 0)
        {
            Shards = 20;
        }
        MenuScript.ShardsDamage = PlayerPrefs.GetInt("RedShardDamage");
        if(ShardsDamage == 0)
        {
            ShardsDamage = 0;
        }
        MenuScript.TimeToDestroyRedShard = PlayerPrefs.GetFloat("RedFadeTime");
        if(TimeToDestroyRedShard == 0)
        {
            TimeToDestroyRedShard = 20;
        }
        MenuScript.BeSlowRadius = PlayerPrefs.GetFloat("YellowRadius");
        if(BeSlowRadius == 0)
        {
            BeSlowRadius = 4;
        }
        MenuScript.SlowTimeYellowBrick = PlayerPrefs.GetFloat("YellowSlowTime");
        if(SlowTimeYellowBrick == 0)
        {
            SlowTimeYellowBrick = 2;
        }
        MenuScript.SlowPower = PlayerPrefs.GetFloat("YellowSlowPower");
        if(SlowPower == 0)
        {
            SlowPower = 1.5f;
        }
        //Установки на уровень 1
        if(SelectLvLScript.ThisIsLvL == 0)
        {
            Time.timeScale = 0;
            Tacher.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Pref = Instantiate(EnemysPref[0],EnemyPoints[2].transform.position,Quaternion.identity);
            EHP = Pref.GetComponent<Health>();
            EHP.HP = 30;
        }
        //Установки на уровень 2
        if(SelectLvLScript.ThisIsLvL == 1)
        {
            Pref = Instantiate(EnemysPref[1],EnemyPoints[2].transform.position,Quaternion.identity);
            EHP = Pref.GetComponent<Health>();
            EHP.HP = 30;
            Dekor[2].SetActive(true);
        }
        //Установки на уровень 3
        if(SelectLvLScript.ThisIsLvL == 2)
        {
            Pref = Instantiate(EnemysPref[0],EnemyPoints[5].transform.position,Quaternion.identity);
            EHP = Pref.GetComponent<Health>();
            EHP.HP = 30;
            Pref = Instantiate(EnemysPref[0],EnemyPoints[2].transform.position,Quaternion.identity);
            EHP = Pref.GetComponent<Health>();
            EHP.HP = 30;
            Dekor[2].SetActive(true);
            Dekor[3].SetActive(true);
            Dekor[4].SetActive(true);
        }
        //Установки на уровень 4
        if(SelectLvLScript.ThisIsLvL == 3)
        {
            Pref = Instantiate(EnemysPref[3],EnemyPoints[3].transform.position,Quaternion.identity);
            EHP = Pref.GetComponent<Health>();
            EHP.HP = 60;
            Dekor[2].SetActive(true);
            Dekor[0].SetActive(true);
        }
        //Установки на уровень 5
        if(SelectLvLScript.ThisIsLvL == 4)
        {
            Pref = Instantiate(EnemysPref[2],EnemyPoints[5].transform.position,Quaternion.identity);
            EHP = Pref.GetComponent<Health>();
            EHP.HP = 20;
            Pref = Instantiate(EnemysPref[1],EnemyPoints[2].transform.position,Quaternion.identity);
            EHP = Pref.GetComponent<Health>();
            EHP.HP = 30;
            Dekor[0].SetActive(true);
        }
        //Установки на уровень 6
        if(SelectLvLScript.ThisIsLvL == 5)
        {
            Pref = Instantiate(EnemysPref[2],EnemyPoints[5].transform.position,Quaternion.identity);
            EHP = Pref.GetComponent<Health>();
            EHP.HP = 20;
            Pref = Instantiate(EnemysPref[4],EnemyPoints[2].transform.position,Quaternion.identity);
            EHP = Pref.GetComponent<Health>();
            EHP.HP = 60;
            Dekor[5].SetActive(true);
        }
        //Установки на уровень 7
        if(SelectLvLScript.ThisIsLvL == 6)
        {
            Pref = Instantiate(EnemysPref[3],EnemyPoints[5].transform.position,Quaternion.identity);
            EHP = Pref.GetComponent<Health>();
            EHP.HP = 60;
            Pref = Instantiate(EnemysPref[2],EnemyPoints[3].transform.position,Quaternion.identity);
            EHP = Pref.GetComponent<Health>();
            EHP.HP = 20;
            Dekor[2].SetActive(true);
            Dekor[3].SetActive(true);
            Dekor[4].SetActive(true);
        }
        //Установки на уровень 8
        if(SelectLvLScript.ThisIsLvL == 7)
        {
            Pref = Instantiate(EnemysPref[0],EnemyPoints[0].transform.position,Quaternion.identity);
            EHP = Pref.GetComponent<Health>();
            EHP.HP = 30;
            Pref = Instantiate(EnemysPref[0],EnemyPoints[1].transform.position,Quaternion.identity);
            EHP = Pref.GetComponent<Health>();
            EHP.HP = 30;
            Pref = Instantiate(EnemysPref[0],EnemyPoints[2].transform.position,Quaternion.identity);
            EHP = Pref.GetComponent<Health>();
            EHP.HP = 30;
            Dekor[2].SetActive(true);
            Dekor[5].SetActive(true);
            Dekor[4].SetActive(true);
        }
        //Установки на уровень 9
        if(SelectLvLScript.ThisIsLvL == 8)
        {
            Pref = Instantiate(EnemysPref[0],EnemyPoints[0].transform.position,Quaternion.identity);
            EHP = Pref.GetComponent<Health>();
            EHP.HP = 30;
            Pref = Instantiate(EnemysPref[0],EnemyPoints[1].transform.position,Quaternion.identity);
            EHP = Pref.GetComponent<Health>();
            EHP.HP = 30;
            Pref = Instantiate(EnemysPref[3],EnemyPoints[2].transform.position,Quaternion.identity);
            EHP = Pref.GetComponent<Health>();
            EHP.HP = 60;
        }
        //Установки на уровень 10
        if(SelectLvLScript.ThisIsLvL == 9)
        {
            Pref = Instantiate(EnemysPref[4],EnemyPoints[0].transform.position,Quaternion.identity);
            EHP = Pref.GetComponent<Health>();
            EHP.HP = 60;
            Pref = Instantiate(EnemysPref[4],EnemyPoints[1].transform.position,Quaternion.identity);
            EHP = Pref.GetComponent<Health>();
            EHP.HP = 60;
            Dekor[0].SetActive(true);
            Dekor[1].SetActive(true);
        }
        //Установки на уровень 11
        if(SelectLvLScript.ThisIsLvL == 10)
        {
            Pref = Instantiate(EnemysPref[4],EnemyPoints[0].transform.position,Quaternion.identity);
            EHP = Pref.GetComponent<Health>();
            EHP.HP = 60;
            Pref = Instantiate(EnemysPref[4],EnemyPoints[1].transform.position,Quaternion.identity);
            EHP = Pref.GetComponent<Health>();
            EHP.HP = 60;
            Pref = Instantiate(EnemysPref[3],EnemyPoints[5].transform.position,Quaternion.identity);
            EHP = Pref.GetComponent<Health>();
            EHP.HP = 60;
            Dekor[0].SetActive(true);
            Dekor[1].SetActive(true);
        }
        //Установки на уровень 12
        if(SelectLvLScript.ThisIsLvL == 11)
        {
            Pref = Instantiate(EnemysPref[2],EnemyPoints[0].transform.position,Quaternion.identity);
            EHP = Pref.GetComponent<Health>();
            EHP.HP = 20;
            Pref = Instantiate(EnemysPref[2],EnemyPoints[1].transform.position,Quaternion.identity);
            EHP = Pref.GetComponent<Health>();
            EHP.HP = 20;
            Pref = Instantiate(EnemysPref[2],EnemyPoints[2].transform.position,Quaternion.identity);
            EHP = Pref.GetComponent<Health>();
            EHP.HP = 20;
            Pref = Instantiate(EnemysPref[3],EnemyPoints[5].transform.position,Quaternion.identity);
            EHP = Pref.GetComponent<Health>();
            EHP.HP = 60;
        }
        Enemy = GameObject.FindGameObjectsWithTag("Enemy");
        EnemyXY = GameObject.FindGameObjectsWithTag("EnemyY");
        EvilMixer = GameObject.FindGameObjectsWithTag("EvilMixer");
        Slowler = GameObject.FindGameObjectsWithTag("SlowlerEnemy");
        EnemyCount = Enemy.Length + EnemyXY.Length + EvilMixer.Length + Slowler.Length;
        timer = EnemyCount * 60f;
    }
    public void TacherOff()
    {
        Time.timeScale = 1;
        Tacher.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Reward()
    {
        if(!WaitLock)
        {
            TotalText.text = "Total: " + AllCredits.ToString() + " * 2X = " + (AllCredits * 2).ToString();
            ReclameButton.SetActive(false);
            AllCredits = (AllCredits * 2);
            WaitLock = true;
        }
    }
    public void ToChooseLvL()
    {
        Credits += AllCredits;
        PlayerPrefs.SetInt("Money", Credits);
        PlayerPrefs.SetInt(SelectLvLScript.ThisIsLvL.ToString() + "LVL",StarsCount);
        Cursor.lockState = CursorLockMode.None;
        SelectLvLScript.LevelDetected = 0;
        SelectLvLScript.ThisIsLvL = 0;
        SceneManager.LoadScene("ChoiseLVL");
    }
    public void Wasted()
    {
        Cursor.lockState = CursorLockMode.None;
        SelectLvLScript.LevelDetected = 0;
        SelectLvLScript.ThisIsLvL = 0;
        SceneManager.LoadScene("ChoiseLVL");
    }

    void Update()
    {
        if(LvLCompliteMenu.activeSelf && !Lock2)
        {
            BattleSound.Stop();
            if(MenuScript.SoundsOnOff)
            {
                WinnSound.Play();
            }
            Lock2 = true;
        }
        if(WastedBanner.activeSelf && !Lock2)
        {
            BattleSound.Stop();
            if(MenuScript.SoundsOnOff)
            {
                FailSound.Play();
            }
            Lock2 = true;
        }
        timer -= Time.deltaTime;
        Timer.text = Mathf.Round(timer).ToString();
        if(timer < 0)
        {
            timer = 0;
        }
        if(EnemyCount == 0 && !Lock)
        {
            Cam1.SetActive(false);
            Cam2.SetActive(false);
            Cam3.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            CreditsOnLvLText.text = "Credits Received: " + AllCredits.ToString();
            LvLCompliteMenu.SetActive(true);
            if(timer > 0 && HP.HP >= 50)
            {
                StarsCount = 3;
                TotalText.text = "Total: " + AllCredits.ToString() + " * 3X = " + (AllCredits * 3).ToString();
                AllCredits = AllCredits * 3;
                Stars[0].SetActive(true);
                Stars[1].SetActive(true);
            }
            if(timer > 0 && HP.HP <= 50)
            {
                StarsCount = 2;
                TotalText.text = "Total: " + AllCredits.ToString() + " * 2X = " + (AllCredits * 2).ToString();
                AllCredits = AllCredits * 2;
                Stars[0].SetActive(true);
            }
            if(timer <= 0 && HP.HP >= 50)
            {
                StarsCount = 2;
                TotalText.text = "Total: " + AllCredits.ToString() + " * 2X = " + (AllCredits * 2).ToString();
                AllCredits = AllCredits * 2;
                Stars[1].SetActive(true);
            }
            if(timer <= 0 && HP.HP < 50)
            {
                StarsCount = 1;
                TotalText.text = "Total: " + AllCredits.ToString();
            }
            Lock = true;
        }
        if(RedBombCount > 0 || GreenBombCount > 0 || YellowBombCount > 0)
        {
            WarningText.SetActive(false);
        }
        if(Input.GetKeyDown(KeyCode.Q) && !Cam3.activeSelf && !Tacher.activeSelf)
        {
            Esc = !Esc;
            if(Esc)
            {
                MenuImage.SetActive(true);
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cam1.SetActive(false);
                Cam2.SetActive(true);
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                MenuImage.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }
    public void Return() 
    {
        Cursor.lockState = CursorLockMode.Locked;
        MenuImage.SetActive(false);
        Esc = false;
        Time.timeScale = 1;
    }
    public void ToMainMenu() 
    {
        SelectLvLScript.LevelDetected = 0;
        SelectLvLScript.ThisIsLvL = 0;
        SceneManager.LoadScene("MainMenu");
    }
}
