using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    public GameObject titleScreen;
    public GameObject heroScreen;
    public GameObject optionScreen;
    public GameObject setUpScene;
    public GameObject theHero1;
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

    public void toOptions()
    {
        titleScreen.gameObject.SetActive(false);
        optionScreen.gameObject.SetActive(true);
    }

    public void toExitGame()
    {
        Application.Quit();
    }

    public void backToHero()
    {
        heroScreen.gameObject.SetActive(true);
        theHero1.gameObject.SetActive(false);
    }

    public void toHero1()
    {
        heroScreen.gameObject.SetActive(false);
        theHero1.gameObject.SetActive(true);
    }
}
