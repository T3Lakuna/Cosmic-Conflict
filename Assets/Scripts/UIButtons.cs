using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class UIButtons : MonoBehaviour
{
    public Image shop;
    public Text textClock;
    
    public Button qLvlUp;
    public Button wLvlUp;
    public Button eLvlUp;
    public Button rLvlUp;
    
    public Image qBar;
    public Image wBar;
    public Image eBar;
    public Image rBar;
    
    private int qLvl;
    private int wLvl; 
    private int eLvl;
    private int rLvl;

    void Start()
    {
        shop.gameObject.SetActive(false);
        qLvlUp.gameObject.SetActive(false);
        wLvlUp.gameObject.SetActive(false);
        eLvlUp.gameObject.SetActive(false);
        rLvlUp.gameObject.SetActive(false);
        
    }

    void update()
    {
        void Update (){
            DateTime time = DateTime.Now;
            string hour = LeadingZero( time.Hour );
            string minute = LeadingZero( time.Minute );
            string second = LeadingZero( time.Second );
            
            textClock.text = minute + ":" + second;
        }
    }
    string LeadingZero (int n){
        return n.ToString().PadLeft(2, '0');
    }
    

    public void shopPress()
    {
        shop.gameObject.SetActive(true);
    }

    public void exitButton()
    {
        shop.gameObject.SetActive(false);
    }

    public void lvlUp()
    {
        //if ()
        {
            qLvlUp.gameObject.SetActive(true);
            wLvlUp.gameObject.SetActive(true);
            eLvlUp.gameObject.SetActive(true);
            rLvlUp.gameObject.SetActive(true);
        }
    }

    public void qLevelUp()
    {
        qLvl++;
        qLvlUp.gameObject.SetActive(false);
        wLvlUp.gameObject.SetActive(false);
        eLvlUp.gameObject.SetActive(false);
        rLvlUp.gameObject.SetActive(false);
    }
}
