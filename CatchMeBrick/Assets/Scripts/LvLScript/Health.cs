using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    AudioSource Deathsound;
    GameObject GameSettings;
    MenuScript MS;
    GameObject Player;
    PlayerController PC;
    GameObject NewText;
    public Text HpText;
    public Image Pain;
    public Image Slow;
    Vector3 UnitPos;
    public RectTransform HPBar;
    GameObject NewEffect;
    GameObject MasterOfBomb;
    CreatBomb CB;
    public int HP = 100;
    bool Lock = false;
    public float TimeToRespawnForEnemy = 7f;
    public bool Respawn = false;
    bool IsSlow = false;
    public void PainColor()
    {
        StartCoroutine(ColorPain());
    }
    public void SlowColor()
    {
        StartCoroutine(SlowPlayer());
    }
    IEnumerator SlowPlayer()
    {
        if(!IsSlow)
        {
            IsSlow = true;
            Slow.color = new Color(1,1,0,0.3f);
            MenuScript.speed = MenuScript.speed /2;
            yield return new WaitForSeconds(3);
            MenuScript.speed = MenuScript.speed *2;
            Slow.color = new Color(1,1,0,0);
            IsSlow = false;
        }
    }
    IEnumerator ColorPain()
    {
        Pain.color = new Color(1,0,0,0.1f);
        yield return new WaitForSeconds(0.3f);
        Pain.color = new Color(1,0,0,0);
    }

    IEnumerator WaitForRespawn()
    {
        yield return new WaitForSeconds(TimeToRespawnForEnemy);
        transform.position = UnitPos;
        HP = 100;
        Lock = false;
    }
    void Start()
    {
        GameSettings = GameObject.FindGameObjectWithTag("GameSettings");
        MS = GameSettings.GetComponent<MenuScript>();
        MasterOfBomb = GameObject.FindGameObjectWithTag("Master");
        CB = MasterOfBomb.GetComponent<CreatBomb>();
        Player = GameObject.FindGameObjectWithTag("Player");
        PC = Player.GetComponent<PlayerController>();
        UnitPos = transform.position;
        Application.targetFrameRate = 60;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Water"))
        {
            if(!Respawn)
            {
                HP = 0; 
            }
        }
    }
    void Update()
    {
        if(gameObject.tag == "Player")
        {
            HpText.text = HP.ToString();
            HPBar.sizeDelta = new Vector2(HP,42.2f);
        }
        if(HP <= 0 && !Lock)
        {
            if(gameObject.tag == "Enemy" || gameObject.tag == "EvilMixer" || gameObject.tag == "EnemyY" || gameObject.tag == "SlowlerEnemy")
            {
                if(Respawn)
                {
                    transform.position = new Vector3(0,0,-1000);
                    StartCoroutine(WaitForRespawn());
                    Lock = true;
                }
                else
                {
                    MS.AllCredits += 5;
                    MS.EnemyCount --;
                    Destroy(gameObject);
                }
                NewEffect = Instantiate(CB.ParticlesEffect[4],new Vector3(transform.position.x,transform.position.y + 2f,transform.position.z),Quaternion.identity);
                Deathsound = NewEffect.GetComponent<AudioSource>();
                if(MenuScript.SoundsOnOff)
                {
                    Deathsound.Play();
                }
                Destroy(NewEffect,1f);
            }
        }
        if(HP <= 0 && gameObject.tag == "Player")
        {
            MS.Cam1.SetActive(false);
            MS.Cam2.SetActive(false);
            MS.Cam3.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            MS.WastedBanner.SetActive(true);
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Shard")
        {
            HP -= MenuScript.ShardsDamage;
            if(gameObject.tag == "Enemy" || gameObject.tag == "EvilMixer" || gameObject.tag == "EnemyY" || gameObject.tag == "SlowlerEnemy")
            {
                NewText = Instantiate(CB.TextShardsDamage,new Vector3(transform.position.x,transform.position.y + 7.5f,transform.position.z),Quaternion.identity);
                Destroy(NewText,1f);
            }
            Destroy(other.gameObject);
        }
    }
}
