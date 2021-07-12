using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UpgraidMenu : MonoBehaviour
{
    
    public int Price;
    public int JumpLVL = 0;
    public int ArmorLVL = 0;
    public int SpeedLVL = 0;
    public int GreenRadiusLVL = 0;
    public int GreenForceLVL = 0;
    public int GreenDamageLVL = 0;
    public int RedShardCountLVL = 0;
    public int RedFadeTimeLVL = 0;
    public int RedShardDamageLVL = 0;
    public int RedDamageLVL = 0;
    public int YellowSlowTimeLVL = 0;
    public int YellowRadiusLVL = 0;
    public int YellowSlowPowerLVL = 0;
    public Text CreditText;
    public Text[] texts;
    GameObject SoundMaster;
    AudioSource ClickSound;
    AudioSource Music;
    public void ShowAd()
    {
        Application.ExternalCall("ShowAd");
    }
    void Start()
    {
        Music = GetComponent<AudioSource>();
        ShowAd();
        if(MenuScript.SoundsOnOff)
        {
            Music.Play();
        }
        SoundMaster = GameObject.FindGameObjectWithTag("SoundMaster");
        ClickSound = SoundMaster.GetComponent<AudioSource>();
        MenuScript.Credits = PlayerPrefs.GetInt("Money");
        CreditText.text = "Credits: " + MenuScript.Credits.ToString();
        JumpLVL = PlayerPrefs.GetInt("LVLofJump");
        SpeedLVL = PlayerPrefs.GetInt("LVLofSpeed");
        ArmorLVL = PlayerPrefs.GetInt("LVLofArmor");
        GreenRadiusLVL = PlayerPrefs.GetInt("LVLofGreenRadius");
        GreenForceLVL = PlayerPrefs.GetInt("LVLofGreenForce");
        GreenDamageLVL = PlayerPrefs.GetInt("LVLofGreenDamage");
        RedDamageLVL = PlayerPrefs.GetInt("LVLofRedDamage");
        RedShardCountLVL = PlayerPrefs.GetInt("LVLofRedShardCount");
        RedShardDamageLVL = PlayerPrefs.GetInt("LVLofRedShardDamage");
        RedFadeTimeLVL = PlayerPrefs.GetInt("LVLofRedFadeTime");
        YellowSlowTimeLVL = PlayerPrefs.GetInt("LVLofYellowSlowTime");
        YellowRadiusLVL = PlayerPrefs.GetInt("LVLofYellowRadius");
        YellowSlowPowerLVL = PlayerPrefs.GetInt("LVLofYellowSlowPower");
        texts[0].text = RedDamageLVL.ToString();
        texts[1].text = RedShardCountLVL.ToString();
        texts[2].text = RedShardDamageLVL.ToString();
        texts[3].text = RedFadeTimeLVL.ToString();
        texts[4].text = GreenRadiusLVL.ToString();
        texts[5].text = GreenForceLVL.ToString();
        texts[6].text = GreenDamageLVL.ToString();
        texts[7].text = YellowSlowPowerLVL.ToString();
        texts[8].text = YellowRadiusLVL.ToString();
        texts[9].text = YellowSlowTimeLVL.ToString();
        texts[10].text = SpeedLVL.ToString();
        texts[11].text = SpeedLVL.ToString();
        texts[12].text = JumpLVL.ToString();
    }
    public void MoreJump()
    {
        if(MenuScript.SoundsOnOff)
        {
            ClickSound.Play();
        }
        Price = ((JumpLVL * 50)+50);
        if(MenuScript.Credits >= Price && JumpLVL < 5)
        {
            MenuScript.Credits -= Price;
            PlayerPrefs.SetInt("Money", MenuScript.Credits);
            CreditText.text = "Credits: " + MenuScript.Credits.ToString();
            MenuScript.jumpHight += 1f;
            JumpLVL ++;
            PlayerPrefs.SetInt("LVLofJump", JumpLVL);
            PlayerPrefs.SetFloat("jumpHIGHT", MenuScript.jumpHight);
            texts[12].text = JumpLVL.ToString();
        }
    }
    public void MoreSpeed()
    {
        if(MenuScript.SoundsOnOff)
        {
            ClickSound.Play();
        }
        Price = ((SpeedLVL * 50)+50);
        if(MenuScript.Credits >= Price && SpeedLVL < 5)
        {
            MenuScript.Credits -= Price;
            PlayerPrefs.SetInt("Money", MenuScript.Credits);
            CreditText.text = "Credits: " + MenuScript.Credits.ToString();
            MenuScript.speed += 1f;
            SpeedLVL ++;
            PlayerPrefs.SetInt("LVLofSpeed", SpeedLVL);
            PlayerPrefs.SetFloat("PlayerSpeed", MenuScript.speed);
            texts[10].text = SpeedLVL.ToString();
        }
    }
    public void MoreArmor()
    {
        if(MenuScript.SoundsOnOff)
        {
            ClickSound.Play();
        }
        Price = ((ArmorLVL * 50)+50);
        if(MenuScript.Credits >= Price && ArmorLVL < 5)
        {
            MenuScript.Credits -= Price;
            PlayerPrefs.SetInt("Money", MenuScript.Credits);
            CreditText.text = "Credits: " + MenuScript.Credits.ToString();
            MenuScript.Armor += 1;
            ArmorLVL ++;
            PlayerPrefs.SetInt("LVLofArmor", ArmorLVL);
            PlayerPrefs.SetInt("PlayerArmor", MenuScript.Armor);
            texts[11].text = ArmorLVL.ToString();
        }
    }
    public void MoreGreenRadius()
    {
        if(MenuScript.SoundsOnOff)
        {
            ClickSound.Play();
        }
        Price = ((GreenRadiusLVL * 50)+50);       
        if(MenuScript.Credits >= Price && GreenRadiusLVL < 5)
        {
            MenuScript.Credits -= Price;
            PlayerPrefs.SetInt("Money", MenuScript.Credits);
            CreditText.text = "Credits: " + MenuScript.Credits.ToString();
            MenuScript.BrickDamageRadius += 1;
            GreenRadiusLVL ++;
            PlayerPrefs.SetInt("LVLofGreenRadius", GreenRadiusLVL);
            PlayerPrefs.SetFloat("GreenRadius", MenuScript.BrickDamageRadius);
            texts[4].text = GreenRadiusLVL.ToString();
        }
    }
    public void MoreGreenForce()
    {
        if(MenuScript.SoundsOnOff)
        {
            ClickSound.Play();
        }
        Price = ((GreenForceLVL * 50)+50);
        if(MenuScript.Credits >= Price && GreenForceLVL < 5)
        {
            MenuScript.Credits -= Price;
            PlayerPrefs.SetInt("Money", MenuScript.Credits);
            CreditText.text = "Credits: " + MenuScript.Credits.ToString();
            MenuScript.Force += 100;
            GreenForceLVL ++;
            PlayerPrefs.SetInt("LVLofGreenForce", GreenForceLVL);
            PlayerPrefs.SetFloat("GreenForce", MenuScript.Force);
            texts[5].text = GreenForceLVL.ToString();
        }
    }
    public void MoreGreenDamage()
    {
        if(MenuScript.SoundsOnOff)
        {
            ClickSound.Play();
        }
        Price = ((GreenDamageLVL * 50)+50);
        if(MenuScript.Credits >= Price && GreenDamageLVL < 5)
        {
            MenuScript.Credits -= Price;
            PlayerPrefs.SetInt("Money", MenuScript.Credits);
            CreditText.text = "Credits: " + MenuScript.Credits.ToString();
            MenuScript.GreenDamage += 5;
            GreenDamageLVL ++;
            PlayerPrefs.SetInt("LVLofGreenDamage", GreenDamageLVL);
            PlayerPrefs.SetInt("GreenDamage", MenuScript.GreenDamage);
            texts[6].text = GreenDamageLVL.ToString();
        }
    }
    public void MoreRedDamage()
    {
        if(MenuScript.SoundsOnOff)
        {
            ClickSound.Play();
        }
        Price = ((RedDamageLVL * 50)+50);
        if(MenuScript.Credits >= Price && RedDamageLVL < 5)
        {
            MenuScript.Credits -= Price;
            PlayerPrefs.SetInt("Money", MenuScript.Credits);
            CreditText.text = "Credits: " + MenuScript.Credits.ToString();
            MenuScript.RedDamage += 10;
            RedDamageLVL ++;
            PlayerPrefs.SetInt("LVLofRedDamage", RedDamageLVL);
            PlayerPrefs.SetInt("RedDamage", MenuScript.RedDamage);
            texts[0].text = RedDamageLVL.ToString();
        }
    }
    public void MoreRedShardCount()
    {
        if(MenuScript.SoundsOnOff)
        {
            ClickSound.Play();
        }
        Price = ((RedShardCountLVL * 50)+50);
        if(MenuScript.Credits >= Price && RedShardCountLVL < 5)
        {
            MenuScript.Credits -= Price;
            PlayerPrefs.SetInt("Money", MenuScript.Credits);
            CreditText.text = "Credits: " + MenuScript.Credits.ToString();
            MenuScript.Shards += 5;
            RedShardCountLVL ++;
            PlayerPrefs.SetInt("LVLofRedShardCount", RedShardCountLVL);
            PlayerPrefs.SetInt("RedShardCount", MenuScript.Shards);
            texts[1].text = RedShardCountLVL.ToString();
        }
    }
    public void MoreRedShardDamage()
    {
        if(MenuScript.SoundsOnOff)
        {
            ClickSound.Play();
        }
        Price = ((RedShardDamageLVL * 50)+50);
        if(MenuScript.Credits >= Price && RedShardDamageLVL < 5)
        {
            MenuScript.Credits -= Price;
            PlayerPrefs.SetInt("Money", MenuScript.Credits);
            CreditText.text = "Credits: " + MenuScript.Credits.ToString();
            MenuScript.ShardsDamage += 1;
            RedShardDamageLVL ++;
            PlayerPrefs.SetInt("LVLofRedShardDamage", RedShardDamageLVL);
            PlayerPrefs.SetInt("RedShardDamage", MenuScript.ShardsDamage);
            texts[2].text = RedShardDamageLVL.ToString();
        }
    }
    public void MoreRedShardFadeTime()
    {
        if(MenuScript.SoundsOnOff)
        {
            ClickSound.Play();
        }
        Price = ((RedFadeTimeLVL * 50)+50);
        if(MenuScript.Credits >= Price && RedFadeTimeLVL < 5)
        {
            MenuScript.Credits -= Price;
            PlayerPrefs.SetInt("Money", MenuScript.Credits);
            CreditText.text = "Credits: " + MenuScript.Credits.ToString();
            MenuScript.TimeToDestroyRedShard += 10;
            RedFadeTimeLVL ++;
            PlayerPrefs.SetInt("LVLofRedFadeTime", RedFadeTimeLVL);
            PlayerPrefs.SetFloat("RedFadeTime", MenuScript.TimeToDestroyRedShard);
            texts[3].text = RedFadeTimeLVL.ToString();
        }
    }
    public void MoreYellowPower()
    {
        if(MenuScript.SoundsOnOff)
        {
            ClickSound.Play();
        }
        Price = ((YellowSlowPowerLVL * 50)+50);
        if(MenuScript.Credits >= Price && YellowSlowPowerLVL < 5)
        {
            MenuScript.Credits -= Price;
            PlayerPrefs.SetInt("Money", MenuScript.Credits);
            CreditText.text = "Credits: " + MenuScript.Credits.ToString();
            MenuScript.SlowPower += 0.5f;
            YellowSlowPowerLVL ++;
            PlayerPrefs.SetInt("LVLofYellowSlowPower", YellowSlowPowerLVL);
            PlayerPrefs.SetFloat("YellowSlowPower", MenuScript.SlowPower);
            texts[7].text = YellowSlowPowerLVL.ToString();
        }
    }
    public void MoreYellowRadius()
    {
        if(MenuScript.SoundsOnOff)
        {
            ClickSound.Play();
        }
        Price = ((YellowRadiusLVL * 50)+50);
        if(MenuScript.Credits >= Price && YellowRadiusLVL < 5)
        {
            MenuScript.Credits -= Price;
            PlayerPrefs.SetInt("Money", MenuScript.Credits);
            CreditText.text = "Credits: " + MenuScript.Credits.ToString();
            MenuScript.BeSlowRadius += 1f;
            YellowRadiusLVL ++;
            PlayerPrefs.SetInt("LVLofYellowRadius", YellowRadiusLVL);
            PlayerPrefs.SetFloat("YellowRadius", MenuScript.BeSlowRadius);
            texts[8].text = YellowRadiusLVL.ToString();
        }
    }
    public void MoreYellowTime()
    {
        if(MenuScript.SoundsOnOff)
        {
            ClickSound.Play();
        }
        Price = ((YellowSlowTimeLVL * 50)+50);
        if(MenuScript.Credits >= Price && YellowSlowTimeLVL < 5)
        {
            MenuScript.Credits -= Price;
            PlayerPrefs.SetInt("Money", MenuScript.Credits);
            CreditText.text = "Credits: " + MenuScript.Credits.ToString();
            MenuScript.SlowTimeYellowBrick += 1f;
            YellowSlowTimeLVL ++;
            PlayerPrefs.SetInt("LVLofYellowSlowTime", YellowSlowTimeLVL);
            PlayerPrefs.SetFloat("YellowSlowTime", MenuScript.SlowTimeYellowBrick);
            texts[9].text = YellowSlowTimeLVL.ToString();
        }
    }
    IEnumerator ToSelect()
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("ChoiseLVL");
    }
    public void ToLVL()
    {
        if(MenuScript.SoundsOnOff)
        {
            ClickSound.Play();
        }
        StartCoroutine(ToSelect());
    }
}
