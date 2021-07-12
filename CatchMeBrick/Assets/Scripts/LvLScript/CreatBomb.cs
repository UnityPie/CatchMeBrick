using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatBomb : MonoBehaviour
{
    public AudioSource ClickSound;
    public GameObject LittleRedBrick;
    public Image Red;
    public Image Green;
    public Image Yellow;
    public GameObject[] Bombs;
    public GameObject EnemyBomb;
    public GameObject[] ParticlesEffect;
    public GameObject[] Points;
    public GameObject TextPref;
    public GameObject TextMassDamage;
    public GameObject SlowText;
    public GameObject TextShardsDamage;
    public int Cells = 0;
    void Start()
    {
        Points = GameObject.FindGameObjectsWithTag("Points");
        for (int i = 0; i < Points.Length; i++)
        {
            Instantiate(Bombs[Random.Range(0,3)],Points[i].transform.position,Quaternion.identity);
        }
    }
    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Cells--;
            if(MenuScript.SoundsOnOff && Cells >= 0)
            {
                ClickSound.Play();
            }
        }
        if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            Cells++;
            if(MenuScript.SoundsOnOff && Cells <= 2)
            {
                ClickSound.Play();
            }
        }
        if(Cells < 0)
        {
            Cells = 0;
        }
        if(Cells > 2)
        {
            Cells = 2;
        }
        if(Cells == 0)
        {
            Red.color = new Color(1,0,0,1);
        }
        else
        {
            Red.color = new Color(1,0,0,0.4f);
        }
        if(Cells == 1)
        {
            Green.color = new Color(0,1,0,1);
        }
        else
        {
            Green.color = new Color(0,1,0,0.4f);
        }
        if(Cells == 2)
        {
            Yellow.color = new Color(1,1,0,1);
        }
        else
        {
            Yellow.color = new Color(1,1,0,0.4f);
        }
    }
    
}
