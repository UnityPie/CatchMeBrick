using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAI : MonoBehaviour
{
    public Transform ShootPos;
    GameObject GameSettings;
    MenuScript MS;
    Health HP;
    GameObject MasterOfBomb;
    GameObject NewBomb;
    Rigidbody rb;
    Rigidbody Rigid;
    CreatBomb CB;
    GameObject Player;
    PlayerController PC;
    Vector3 Speed;
    Vector3 PlayerPos;
    float timer = 0;
    float Dist;
    float Rand;
    public float EnemySpeed = 2f;
    public float PowerOfBrick = 2f;
    bool LeftOrRight;
    bool IsSlow = false;
    public bool SuperLock = false;
    int BrickCount;
    int RandomMove;
    float RandTimer;
    bool InPole = false;
    void Start()
    {
        HP = GetComponent<Health>();
        Rand = Random.Range(0,2);
        RandTimer = Random.Range(3,7);
        if(Rand == 0)
        {
            LeftOrRight = true;
        }
        if(Rand == 1)
        {
            LeftOrRight = false;
        }
        Player = GameObject.FindGameObjectWithTag("Player");
        PC = Player.GetComponent<PlayerController>();
        MasterOfBomb = GameObject.FindGameObjectWithTag("Master");
        CB = MasterOfBomb.GetComponent<CreatBomb>();
        Rigid = GetComponent<Rigidbody>();
        GameSettings = GameObject.FindGameObjectWithTag("GameSettings");
        MS = GameSettings.GetComponent<MenuScript>();
        timer = Random.Range(MS.MinTimeForAttack,MS.MaxTimeForAttack);
        BrickCount = MS.EvilMixerBrickCount;
    }
    IEnumerator SlowEnemy()
    {
        if(!IsSlow)
        {
            IsSlow = true;
            EnemySpeed = EnemySpeed/MenuScript.SlowPower;
            yield return new WaitForSeconds(MenuScript.SlowTimeYellowBrick);
            IsSlow = false;
            EnemySpeed = EnemySpeed*MenuScript.SlowPower; 
        }
    }
    IEnumerator EvilMixer()
    {
        if(BrickCount > 0)
        {
            PlayerPos = Player.transform.position - new Vector3(transform.position.x,transform.position.y - 15f,transform.position.z);
            NewBomb = Instantiate(CB.EnemyBomb,ShootPos.transform.position,Quaternion.identity);    
            rb = NewBomb.GetComponent<Rigidbody>();
            rb.AddForce(PlayerPos / PowerOfBrick,ForceMode.Impulse);
            BrickCount --;
            yield return new WaitForSeconds(0.3f);
            if(IsSlow == true)
            {
                BrickCount = 0;
            }
            if(BrickCount <= 0)
            {
                BrickCount = MS.EvilMixerBrickCount;
                timer = Random.Range(MS.MinTimeForAttack,MS.MaxTimeForAttack);
                SuperLock = false;
            }
            else
            {
                StartCoroutine(EvilMixer());
            }
        }
    }
    public void StartSlow()
    {
        StartCoroutine(SlowEnemy());
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "VectorPole")
        {
            LeftOrRight = !LeftOrRight;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "VectorPole")
        {
            InPole = true;
        }
        else
        {
            InPole = false;
        }
    }
    void FixedUpdate()
    {
        RandTimer -= Time.deltaTime;
        if(RandTimer < 0 && InPole)
        {
            RandomMove = Random.Range(0,2);
            RandTimer = Random.Range(3,7);
        }
        Dist = Vector3.Distance(ShootPos.transform.position,Player.transform.position);
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        RaycastHit hit;
        transform.LookAt(Player.transform.position);
        if(gameObject.tag == "Enemy")
        {
            if(LeftOrRight)
            {
                Rigid.MovePosition(transform.position + Vector3.left * Time.deltaTime * EnemySpeed);
            }
            if(!LeftOrRight)
            {
                Rigid.MovePosition(transform.position + Vector3.right * Time.deltaTime * EnemySpeed);
            }
        }
        if(gameObject.tag == "EnemyY" || gameObject.tag == "SlowlerEnemy")
        {
            if(RandomMove == 0)
            {
                if(LeftOrRight)
                {
                    Rigid.MovePosition(transform.position + Vector3.forward * Time.deltaTime * EnemySpeed);
                }
                if(!LeftOrRight)
                {
                    Rigid.MovePosition(transform.position + Vector3.back * Time.deltaTime * EnemySpeed);
                }
            }
            if(RandomMove == 1)
            {
                if(LeftOrRight)
                {
                    Rigid.MovePosition(transform.position + Vector3.left * Time.deltaTime * EnemySpeed);
                }
                if(!LeftOrRight)
                {
                    Rigid.MovePosition(transform.position + Vector3.right * Time.deltaTime * EnemySpeed);
                }
            }
        }
        timer -= Time.deltaTime;
        if(timer < 0 && HP.HP > 0 && !IsSlow && !Physics.Raycast(ShootPos.transform.position, Player.transform.position - transform.position, out hit, Dist, layerMask))
        {
            if(gameObject.tag == "Enemy" || gameObject.tag == "EnemyY")
            {
                PlayerPos = Player.transform.position - new Vector3(transform.position.x,transform.position.y - 15f,transform.position.z + 5f);
                NewBomb = Instantiate(CB.EnemyBomb,ShootPos.transform.position,Quaternion.identity);
                rb = NewBomb.GetComponent<Rigidbody>();
                rb.AddForce(PlayerPos / PowerOfBrick,ForceMode.Impulse);
                timer = Random.Range(MS.MinTimeForAttack,MS.MaxTimeForAttack);    
            }
            if(gameObject.tag == "SlowlerEnemy")
            {
                PlayerPos = Player.transform.position - new Vector3(transform.position.x,transform.position.y - 15f,transform.position.z + 5f);
                NewBomb = Instantiate(PC.PrefabCollections[2],ShootPos.transform.position,Quaternion.identity);
                rb = NewBomb.GetComponent<Rigidbody>();
                rb.AddForce(PlayerPos / PowerOfBrick,ForceMode.Impulse);
                timer = Random.Range(MS.MinTimeForAttack,MS.MaxTimeForAttack);
            }
            if(gameObject.tag == "EvilMixer" && !SuperLock)
            {
                StartCoroutine(EvilMixer());
                SuperLock = true;
            }
        }
    }
    public void RunVector()
    {
        Rand = Random.Range(0,2);
        if(Rand == 0)
        {
            LeftOrRight = true;
        }
        if(Rand == 1)
        {
            LeftOrRight = false;
        }
    }
}
