using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoScript : MonoBehaviour
{
    GameObject UPGCost;
    Text UPGCostText;
    Text Count;
    int ReString;
    bool Lock = false;
    void Start()
    {
        UPGCost = GameObject.FindGameObjectWithTag("UPGCost");
        UPGCostText = UPGCost.GetComponent<Text>();
        Count = GetComponentInChildren<Text>();
        UPGCostText.text = "";
    }
    public void OnMouseHere()
    {
        Lock = true;
        ReString = int.Parse(Count.text);
        UPGCost.SetActive(true);
    }
    public void OnMouseThere()
    {
        UPGCost.SetActive(false);
        Lock = false;
    }
    void Update()
    {
        if(Lock && ReString < 5)
        {
            ReString = int.Parse(Count.text);
            UPGCostText.text = "PRICE: " + ((ReString * 50)+50).ToString();
        }
    }
}
