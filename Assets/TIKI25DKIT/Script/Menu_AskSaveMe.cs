using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Menu_AskSaveMe : MonoBehaviour
{
    public Text timerTxt;

    public int timer = 3;
    public int watchVideoFreeLives = 3;
    public Button btnSaveByHeart;
    public Button btnWatchVideoAd;
    public  int rewardNum;
    public static Menu_AskSaveMe Instance;
    void OnEnable()
    {
      //  btnSaveByHeart.interactable = GlobalValue.SavedLive >= 1;
#if UNITY_ANDROID || UNITY_IOS
      //  btnWatchVideoAd.interactable = AdsManager.Instance && AdsManager.Instance.isRewardedAdReady();
#else
            btnWatchVideoAd.interactable = false;
            btnWatchVideoAd.gameObject.SetActive(false);
#endif

        if (GameManager.Instance)
        {
            if (GameManager.Instance.isTestLevel)
                Continue();
            else if (!btnSaveByHeart.interactable && !btnWatchVideoAd.interactable)
                Close();
            else
            {
                StartCoroutine(StartCountingDown());
            }
        }
    }

    private void Awake()
    {
        Instance = this;
        
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator StartCountingDown()
    {
        var currentTime = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup - currentTime < timer)
        {
            timerTxt.text = (timer - (int)(Time.realtimeSinceStartup - currentTime)) + "";
            yield return null;
        }

        Close();
    }
    
    public void Close()
    {
        StopAllCoroutines();
        //if (AdsManager.Instance)
        //    AdsManager.Instance.ShowAdmobBanner(true);
        GameManager.Instance.gameState = GameManager.GameState.Waiting;
        GameManager.Instance.GameOver(true);
        Time.timeScale = 1;
        gameObject.SetActive(false);
        Destroy(this);      //destroy this script
        GoogleAdMobController.Instance.HideRectBanner();
    }

    public void SaveByHeart()
    {
       
            StopAllCoroutines();
            SoundManager.Click();
            GlobalValue.SavedLive--;
            Continue();
        
    }

    public void GiveLives()
    {
       
        StopAllCoroutines();
        SoundManager.Click();
        Continue();
        Close();

    }

    IEnumerator RewardAd()
    {

        if (PlayerPrefs.GetInt("Removeads") != 1)
        {
            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                //MenuManager.Instance.adMightApear.SetActive(true);
                yield return new WaitForSecondsRealtime(0f);
                GoogleAdMobController.Instance.ShowRewardedAd();

                WatchVideoAd();
                //MenuManager.Instance.adMightApear.SetActive(false);

            }
            //else
            //{
            //    //MenuManager.Instance.adMightApear.SetActive(false);
            //    //MenuManager.Instance.adNotAvailable.SetActive(true);
            //    yield return new WaitForSecondsRealtime(1f);
            //    //MenuManager.Instance.adNotAvailable.SetActive(false);
            //}
        }
        else
        {
            WatchVideoAd();
        }
    }
    public void WatchVideoAd()
    {
        print("FreeLive");
        StopAllCoroutines();
        SoundManager.Click();
        MenuManager.Instance.askForContinue.SetActive(false);
        Continue();
        if (timer <= 1)
        {
            //MenuManager.Instance.adNotAvailable.SetActive(false);
        }

    }

    public void FreeliveRewards()
    {
        
        StartCoroutine(RewardAd());
    }

    private void AdsManager_AdResult(bool isSuccess)
    {
        if (isSuccess)
        {
            //reset to avoid play Unity video ad when finish game
            GlobalValue.SavedLive += watchVideoFreeLives;
            Continue();
        }
    }

    void Continue()
    {
        MenuManager.Instance.Continue();

    }

    private void Update()
    {
        if (timer <= 1)
        {
           // MenuManager.Instance.adNotAvailable.SetActive(false);
        }

        if (GlobalValue.SavedLive >= 0)
        {
            btnSaveByHeart.interactable = false;
        }

        if (GlobalValue.SavedLive >= 1)
        {
            btnSaveByHeart.interactable = true;
        }
    }
}
