using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    public Image shop;
    void Start()
    {
        shop.gameObject.SetActive(false);
    }

    public void shopPress()
    {
        shop.gameObject.SetActive(true);
    }

    public void exitButton()
    {
        shop.gameObject.SetActive(false);
    }
}
