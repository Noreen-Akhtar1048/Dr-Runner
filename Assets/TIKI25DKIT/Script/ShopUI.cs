using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public Text txtHearth, txtCoin,Bulletstxt;
    public static ShopUI instance;

    private void Start()
    {
        instance = this;
    }

    void Update()
    {
        txtHearth.text = "x" + GlobalValue.SavedLive;
        txtCoin.text = "x" + GlobalValue.SavedCoins;
        Bulletstxt.text = "x" + GlobalValue.Bullets;

    }
}