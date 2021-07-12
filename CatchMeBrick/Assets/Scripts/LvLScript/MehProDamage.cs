using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MehProDamage : MonoBehaviour
{
    GameObject Player;
    public TextMeshProUGUI DamageText;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        
        if(gameObject.tag == "Sniper")
        {
            DamageText.text = (-1 * MenuScript.RedDamage).ToString() + " hp";
        }
        if(gameObject.tag == "Mass")
        {
            DamageText.text = (-1 * MenuScript.GreenDamage).ToString() + " hp";
        }
        if(gameObject.tag == "ShardText")
        {
            DamageText.text = (-1 * MenuScript.ShardsDamage).ToString() + " hp";
        }
    }
    void Update()
    {
        transform.LookAt(Player.transform.position);
    }
}
