using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    public GameObject titleScreen;
    public GameObject heroScreen;
    void Start()
    {
        titleScreen.gameObject.SetActive(true);
        heroScreen.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toHeroScreen()
    {
        titleScreen.gameObject.SetActive(false);
        heroScreen.gameObject.SetActive(true);
    }

    public void toTitleScreen()
    {
        titleScreen.gameObject.SetActive(true);
        heroScreen.gameObject.SetActive(false);
    }
}
