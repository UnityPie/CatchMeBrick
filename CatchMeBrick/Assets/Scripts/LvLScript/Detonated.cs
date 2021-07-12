using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Detonated : MonoBehaviour
{
    public AudioSource ShootSound;
    public AudioSource CrashSound;
    public AudioSource BangSound;
    GameObject GameSettings;
    MenuScript MS;
    GameObject MasterOfBomb;
    CreatBomb CB;
    GameObject[] UnitInZone;
    GameObject NewHpDetector;
    GameObject NewSlowWord;
    GameObject NewBrick;
    GameObject UnitForSniper;
    GameObject Player;
    Collider[] overlappedColliders;
    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        MasterOfBomb = GameObject.FindGameObjectWithTag("Master");
        CB = MasterOfBomb.GetComponent<CreatBomb>();
        GameSettings = GameObject.FindGameObjectWithTag("GameSettings");
        MS = GameSettings.GetComponent<MenuScript>();
    }
    void Start()
    {
        if(MenuScript.SoundsOnOff)
        {
            ShootSound.Play();
        }
    }
    void Bang()
    {
        if(gameObject.tag == "Player")
        {
            overlappedColliders = Physics.OverlapSphere(transform.position, MenuScript.BrickDamageRadius);
        }
        else
        {
            overlappedColliders = Physics.OverlapSphere(transform.position, 7f);
        }
        for (int i = 0; i < overlappedColliders.Length; i++)
        {
            if(!overlappedColliders[i].isTrigger)
            {
                Rigidbody rb = overlappedColliders[i].attachedRigidbody;
                GameObject Unit = overlappedColliders[i].gameObject;
                if(rb)
                {
                    rb.AddExplosionForce(MenuScript.Force, transform.position, MenuScript.BrickDamageRadius);
                }
                if(Unit.tag == "Player")
                {
                    Health HP = Unit.GetComponent<Health>();
                    HP.HP -= (MenuScript.EnemyDamage - MenuScript.Armor);
                    HP.PainColor();
                }
                if(Unit.tag == "Enemy" || Unit.tag == "EvilMixer" || Unit.tag == "EnemyY" || Unit.tag == "SlowlerEnemy")
                {
                    SimpleAI SAI = Unit.GetComponent<SimpleAI>();
                    Health HP = Unit.GetComponent<Health>();
                    HP.HP -= MenuScript.GreenDamage;
                    NewHpDetector = Instantiate(CB.TextMassDamage,new Vector3(Unit.transform.position.x,Unit.transform.position.y + 7.5f,Unit.transform.position.z),Quaternion.identity);
                    Destroy(NewHpDetector,1f);
                    SAI.RunVector();
                }
            }
        }
    }
    void Slow()
    {
        Collider[] SlowColliders = Physics.OverlapSphere(transform.position, MenuScript.BeSlowRadius);
        for (int i = 0; i < SlowColliders.Length; i++)
        {
            GameObject SlowUnit = SlowColliders[i].gameObject;
            if(SlowUnit.tag == "Enemy" || SlowUnit.tag == "EvilMixer" || SlowUnit.tag == "EnemyY" || SlowUnit.tag == "SlowlerEnemy")
            {
                SimpleAI SAI = SlowUnit.GetComponent<SimpleAI>();
                NewSlowWord = Instantiate(CB.SlowText,new Vector3(SlowUnit.transform.position.x,SlowUnit.transform.position.y + 7.5f,SlowUnit.transform.position.z),Quaternion.identity);
                Destroy(NewSlowWord,1f);
                SAI.StartSlow();
            }
            if(SlowUnit.tag == "Player")
            {
                Health Hp = Player.GetComponent<Health>();
                Hp.SlowColor();
                Hp.HP -= 1;
            }
        }
    }
    void Sniper()
    {
        for (int i = 0; i < MenuScript.Shards; i++)
        {
            if(MenuScript.ShardsDamage > 0)
            {
                NewBrick = Instantiate(CB.LittleRedBrick,transform.position,Random.rotation);
                Rigidbody rb = NewBrick.GetComponent<Rigidbody>();
                if(rb)
                {
                    rb.AddExplosionForce(1000f, transform.position, 5f);
                }
                Destroy(NewBrick, MenuScript.TimeToDestroyRedShard);
            }
        }
        if(UnitForSniper.gameObject.tag == "Enemy" || UnitForSniper.gameObject.tag == "EvilMixer" || UnitForSniper.gameObject.tag == "EnemyY" || UnitForSniper.gameObject.tag == "SlowlerEnemy")
        {
            SimpleAI SAI = UnitForSniper.gameObject.GetComponent<SimpleAI>();
            Health HP = UnitForSniper.gameObject.GetComponent<Health>();
            HP.HP -= MenuScript.RedDamage;
            NewHpDetector = Instantiate(CB.TextPref,new Vector3(UnitForSniper.transform.position.x,UnitForSniper.transform.position.y + 7.5f,UnitForSniper.transform.position.z),Quaternion.identity);
            Destroy(NewHpDetector,1f);
            SAI.RunVector();
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if(gameObject.tag == "Red")
        {
            UnitForSniper = other.gameObject;
            Instantiate(CB.ParticlesEffect[0],transform.position,Quaternion.identity);
            Sniper();
        }
        if(gameObject.tag == "Green")
        {
            Bang();
            Instantiate(CB.ParticlesEffect[1],transform.position,Quaternion.identity);
        }
        if(gameObject.tag == "Yellow")
        {
            Slow();
            Instantiate(CB.ParticlesEffect[2],transform.position,Quaternion.identity);
        }
        if(gameObject.tag == "EnemyGrenade")
        {
            if(other.gameObject.tag != "Enemy" && other.gameObject.tag != "EvilMixer" && other.gameObject.tag != "EnemyY" && other.gameObject.tag != "SlowlerEnemy")
            {
                Bang();
                Instantiate(CB.ParticlesEffect[3],transform.position,Quaternion.identity);
            }
        }
        Destroy(gameObject);
    }
}
