using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    public enum ITEM_TYPE { none, watchVideo, buyLive,buyBullets}

    public ITEM_TYPE itemType;
    public int rewarded = 100;
    public float price = 100;
    public GameObject watchVideocontainer;
    public int coin;
    public AudioClip soundRewarded;
    public static ShopItemUI instance;
    public Text priceTxt, rewardedTxt, rewardTimeCountDownTxt;
    private ShopUI _shopUI;
    public GameObject adMightApear,adNotAvailable;
   // public GameObject rewardbuton;
   public int rewardNum;

    private void Start()
    {
        instance = this;
    }
    private void Update()
    {
        UpdateStatus();
        ChangeButton();
    }

    void UpdateStatus()
    {
        if (itemType == ITEM_TYPE.buyLive)
        {
            priceTxt.text = price + "";
            rewardedTxt.text = "+" + rewarded;
        }
        else if(itemType== ITEM_TYPE.buyBullets)
        {
            priceTxt.text = price + "";
            rewardedTxt.text = "+" + rewarded;
        }
        else if (itemType == ITEM_TYPE.watchVideo)
        {
            priceTxt.text = "FREE";
            rewardedTxt.text = "+" + rewarded;


           
        }
    }
    public void ChangeButton()
    {
        if (GlobalValue.SavedCoins >= price || itemType == ITEM_TYPE.buyLive)
        {
            priceTxt.text = price + "";
            rewardedTxt.text = "+" + rewarded;
        }
        else if (GlobalValue.SavedCoins>=price||itemType == ITEM_TYPE.buyBullets)
        {
            priceTxt.text = price + "";
            rewardedTxt.text = "+" + rewarded;
        }
        else if (GlobalValue.SavedCoins <= price || itemType == ITEM_TYPE.watchVideo)
        {
            priceTxt.text = "FREE";
            rewardedTxt.text = "+" + rewarded;
        }

        
    }
        public void Buy()
    {
        switch (itemType)
        {
            case ITEM_TYPE.buyLive:
                if (GlobalValue.SavedCoins >= price)
                {
                    GlobalValue.SavedCoins -= (int)price;
                    GlobalValue.SavedLive += rewarded;
                    SoundManager.PlaySfx(soundRewarded);
                }
                break;
            case ITEM_TYPE.buyBullets:
                if (GlobalValue.SavedCoins >= price)
                {
                    GlobalValue.SavedCoins -= (int)price;
                    GlobalValue.Bullets += rewarded;
                    SoundManager.PlaySfx(soundRewarded);
                  
                }
                break;
            case ITEM_TYPE.watchVideo:
              
               

                break;
        }

        if (rewarded <= price)
        {
          //  rewardbuton.SetActive(true);
        }
        else
        {
          //  rewardbuton.SetActive(false);
        }
       
       
    }

    public void AdsManager_AdResult(bool isSuccess)
    {
       // AdsManager.AdResult -= AdsManager_AdResult;
        if (isSuccess)
        {
            GlobalValue.SavedCoins += rewarded;
            SoundManager.PlaySfx(soundRewarded);
            ChangeButton();
          //  UpdateStatus();

        }
    }
    public void RewardBullets()
    {
      StartCoroutine(Bullets());
    }
  
    IEnumerator Bullets()
    {
        // if (Application.internetReachability != NetworkReachability.NotReachable && GoogleAdMobController.initialized)
        // {
        //    
        //     adMightApear.SetActive(true);
        //     adNotAvailable.SetActive(false);
        //     yield return new WaitForSecondsRealtime(1f);
        //     GoogleAdMobController.instance.ShowRewardedAd();
        //     adMightApear.SetActive(false);
        //   
        // }
        //else
       // {

            adMightApear.SetActive(false);
            adNotAvailable.SetActive(true);
            yield return new WaitForSecondsRealtime(1.5f);
            adNotAvailable.SetActive(false);
       // }
    }

    //IEnumerator Coins()
    //{
    //    if (Application.internetReachability != NetworkReachability.NotReachable && GoogleAdMobController.isAdmobInitialized)
    //    {

    //        adMightApear.SetActive(true);
    //        adNotAvailable.SetActive(false);
    //        yield return new WaitForSecondsRealtime(1f);
    //        GoogleAdMobController.Instance.ShowRewardedVideo(AddCoins);
    //        adMightApear.SetActive(false);
    //        print("Love Ho gaya");
    //    }
    //    else
    //    {

    //        adMightApear.SetActive(false);
    //        adNotAvailable.SetActive(true);
    //        yield return new WaitForSecondsRealtime(1.5f);
    //        adNotAvailable.SetActive(false);

    //    }
    //}


    public void AddCoins()
    {
        GlobalValue.SavedCoins += 100;
        print("Added");
    }

   // public void AddBullets()
   // {
   //     ShopUI.instance.Bulletstxt.text = (rewarded + 20).ToString();
   //     print(ShopUI.instance.Bulletstxt.text);
   //     ShopUI.instance.Bulletstxt.text = (coin + 10).ToString();
   //     PlayerPrefs.SetInt("Bullets", coin);
   //GoogleAdMobController.Instance.ShowRewardedVideo(AddCoins);
        
   // }

    
}
