using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombsCount : MonoBehaviour
{
    GameObject MasterOfBomb;
    CreatBomb CB;
    GameObject GameSettings;
    GameObject Player;
    MenuScript MS;
    Vector3 Position;
    float Dist;
    float Rotate;

    void Start()
    {
        Position = transform.position;
        GameSettings = GameObject.FindGameObjectWithTag("GameSettings");
        MasterOfBomb = GameObject.FindGameObjectWithTag("Master");
        Player = GameObject.FindGameObjectWithTag("Player");
        CB = MasterOfBomb.GetComponent<CreatBomb>();
        MS = GameSettings.GetComponent<MenuScript>();
    }
    IEnumerator ReRoll()
    {
        yield return new WaitForSeconds(7);
        Instantiate(CB.Bombs[Random.Range(0,CB.Bombs.Length)], Position, Quaternion.identity);
        Destroy(gameObject);
    }
    void Update()
    {
        Dist = Vector3.Distance(transform.position,Player.transform.position);
        if(Dist < 3f)
        {
            if(gameObject.tag == "RedBomb")
            {
                MS.RedBombCount ++;
                MS.RedText.text = MS.RedBombCount.ToString();
            }
            if(gameObject.tag == "YellowBomb")
            {
                MS.YellowBombCount ++;
                MS.YellowText.text = MS.YellowBombCount.ToString();
            }
            if(gameObject.tag == "GreenBomb")
            {
                MS.GreenBombCount ++;
                MS.GreenText.text = MS.GreenBombCount.ToString();
            }
            StartCoroutine(ReRoll());
            transform.position = new Vector3(0,0,-1000f);
        }
    }
    void FixedUpdate()
    {
        Rotate += 1f;
        if(Rotate > 360f)
        {
            Rotate = 0;
        }
        transform.rotation = Quaternion.Euler(0,Rotate,0);
    }
}
