using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;
//using GameAnalyticsSDK;
using UnityEngine.PlayerLoop;
using Firebase.Analytics;
using TMPro;
using UnityEditor;


public class HomeMenu : MonoBehaviour
{
    public static HomeMenu Instance;
    public GameObject UI;
    public GameObject LevelUI;
    public GameObject LoadingUI;
    public GameObject SettingsUI;
    public GameObject RemoveAdPanel; //ShopUI,
    public Text livestxt, coinstxt, bulletstxt;
    public TextMeshProUGUI removeAdsPrice;

    [Header("Sound and Music")]
    public Image soundImage;
    public Image musicImage;
    public GameObject /*AdmightApear,*/exitPanel;
    public Sprite soundImageOn, soundImageOff, musicImageOn, musicImageOff;
    //private ShopUI _shopUI;
    public GameObject adBtn;
    public GameObject addsecondBtn;

    public TextMeshProUGUI versionCode;

    // public GameObject adMightApear,adNotAvailable;
    public void Awake()
    {

        //GameAnalytics.Initialize();
        Instance = this;
        UI.SetActive(true);
        LevelUI.SetActive(false);
        LoadingUI.SetActive(false);
        SettingsUI.SetActive(false);
        //ShopUI.SetActive(false);

        Time.timeScale = 1;

        if (soundImage)
            soundImage.sprite = GlobalValue.isSound ? soundImageOn : soundImageOff;
        if (musicImage)
            musicImage.sprite = GlobalValue.isMusic ? musicImageOn : musicImageOff;
        if (!GlobalValue.isSound)
            SoundManager.SoundVolume = 0;
        if (!GlobalValue.isMusic)
            SoundManager.MusicVolume = 0;
        livestxt.text = "X" + GlobalValue.SavedLive;
        coinstxt.text = "X" + GlobalValue.SavedCoins;
        bulletstxt.text = "X" + GlobalValue.Bullets;

    }

    private void Start()
    {

        //if (PlayerPrefs.GetInt("Removeads")==1)
        //{
        //     adBtn.SetActive(false);
        //    addsecondBtn.SetActive(false);
        //}
        if (!PlayerPrefs.HasKey("SettingsOpen"))
        {
            PlayerPrefs.SetInt("SettingsOpen", 0);

        }
        if (PlayerPrefs.GetInt("SettingsOpen") == 0)
        {
            SettingsUI.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("SettingsOpen") == 1)
        {
            SettingsUI.SetActive(false);

        }

        if (PlayerPrefs.GetInt("RemoveAdOpen") == 0 && PlayerPrefs.GetInt("Removeads") == 0)
        {
            //  RemoveAdPanel.SetActive(true);

        }
        else if (PlayerPrefs.GetInt("RemoveAdOpen") == 1)
        {
            RemoveAdPanel.SetActive(false);
        }

        CheckAdButtonStatus();

        // check banner ad, if it should be displayed or not
        if (PlayerPrefs.GetInt("Removeads") == 1)
            Invoke(nameof(HideBanner), 0.2f);
        else
        {
            GoogleAdMobController.Instance.ShowBanner();

        }



        SoundManager.PlayGameMusic();
        //
        //  GlobalValue.SavedCoins = 10000;

        // wait and get no ads price
        Invoke(nameof(UpdatePrice), 0.2f);
    }

    private void UpdatePrice()
    {
        removeAdsPrice.text = IAPManager.instance.GetNoAdsPrice();
    }

    private void HideBanner() => GoogleAdMobController.Instance.HideBanner();

    public void CheckAdButtonStatus() => adBtn.SetActive(PlayerPrefs.GetInt("Removeads") == 0);

    void Update()
    {

        livestxt.text = "X" + GlobalValue.SavedLive;
        coinstxt.text = "X" + GlobalValue.SavedCoins;
        bulletstxt.text = "X" + GlobalValue.Bullets;

#if UNITY_EDITOR 
        versionCode.text = "v " + PlayerSettings.bundleVersion + "." + PlayerSettings.Android.bundleVersionCode;
#endif
        //if (PlayerPrefs.GetInt("Removeads") == 1)
        //{
        //    adBtn.SetActive(false);
        //    addsecondBtn.SetActive(false);
        //    GoogleAdMobController.Instance.HideBanner();
        //}
        //if (PlayerPrefs.GetInt("Removeads") == 1)
        //{
        //    RemoveAdPanel.SetActive(false);
        //}
    }
    public void ShowLevelUI()
    {
        SoundManager.Click();
        LevelUI.SetActive(true);
        GoogleAdMobController.Instance.HideBanner();

    }
    public void HideLevelUI()
    {
        SoundManager.Click();
        LevelUI.SetActive(false);
        GoogleAdMobController.Instance.ShowBanner();
    }


    public void GameExit()
    {
        //StartCoroutine(ShowIntAd());
        ShowExitPanel();

        GoogleAdMobController.Instance.ShowRectBanner();
        GoogleAdMobController.Instance.HideBanner();
    }

    void ShowExitPanel()
    {
        exitPanel.SetActive(true);
    }
    public void ExitYes()
    {
        Application.Quit();
    }
    public void ExitNo()
    {
        exitPanel.SetActive(false);
        GoogleAdMobController.Instance.HideRectBanner();
        GoogleAdMobController.Instance.ShowBanner();

    }
    public void ShowSettings(bool open)
    {
        GoogleAdMobController.Instance.HideBanner();

        SoundManager.Click();
        SettingsUI.SetActive(open);

        if (!open)
        {
            PlayerPrefs.SetInt("SettingsOpen", 1);
        }

    }

    public void showSetting()
    {
        GoogleAdMobController.Instance.ShowRectBanner();
        GoogleAdMobController.Instance.HideBanner();
    }

    public void hideCredits()
    {
        GoogleAdMobController.Instance.ShowBanner();
        GoogleAdMobController.Instance.HideRectBanner();
        
    }

    public void hideSettings()
    {
        GoogleAdMobController.Instance.ShowBanner();
        GoogleAdMobController.Instance.HideRectBanner();
        //GoogleAdMobController.Instance.ShowInterstitialAd();
    }

    public void ShowRemoveAdPanel(/*bool open*/)
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            //GoogleAdMobController.Instance.ShowRewardedAdRemoveAdPanel();
            GoogleAdMobController.Instance.ShowRewardedAd(() => RemoveAdPanel.SetActive(false));
            FirebaseAnalytics.LogEvent("Player_pressed_the_watch_add_button");
            SoundManager.Click();
            // RemoveAdPanel.SetActive(open);
        }
        //if (!open)
        //{
        //PlayerPrefs.SetInt("RemoveAdOpen", 1);
        //}

    }



    public void ShowShop(bool open)
    {

        SoundManager.Click();
        //ShopUI.SetActive(open);
    }
    IEnumerator ShowIntAd()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            //AdmightApear.SetActive(true);
            yield return new WaitForSeconds(1f);
            //AdmightApear.SetActive(false);
            //GoogleAdMobController.Instance.ShowInterstitialAd();
            //GoogleAdMobController.Instance.ShowBanner();


        }
    }
    public void LoadLevel()
    {
        LoadingUI.SetActive(true);
        exitPanel.SetActive(false);
        Invoke("WaitloadingScene", 3f);
    }

    public void WaitloadingScene()
    {
        SceneManager.LoadSceneAsync("Level " + GlobalValue.levelPlaying);
    }

    public void LoadTestFeatureScene()
    {
        StartCoroutine(WaitforTestingScene());
    }

    IEnumerator WaitforTestingScene()
    {
        //AdmightApear.SetActive(true);
        exitPanel.SetActive(false);
        yield return new WaitForSeconds(1f);
        LoadingUI.SetActive(true);
        StartCoroutine(ShowAds());
        yield return new WaitForSeconds(1f);
        //AdmightApear.SetActive(false);
        SceneManager.LoadSceneAsync("Test All Features");
        GlobalValue.levelPlaying = -1;
        //   yield return new WaitForSeconds(0.75f);
        // LoadingUI.SetActive(false);
    }
    IEnumerator ShowAds()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            //AdmightApear.SetActive(true);
            yield return new WaitForSeconds(0);
            // AdmightApear.SetActive(false);
           // GoogleAdMobController.Instance.ShowInterstitialAd();
            //  GoogleAdMobController.Instance.showRect();

        }
    }

    public void PurchaseNoAds() => IAPManager.instance.RemoveAdsBtnPreessed();
    public void RewardCoins() { GoogleAdMobController.Instance.ShowRewardedAd(Reward); }


    public void Reward()
    {
        GlobalValue.SavedNewCoins += 50;


    }

    public void Purchase10000Coins() => IAPManager.instance.OnPurchaseConsumable1();
    public void Purchase25000Coins() => IAPManager.instance.OnPurchaseConsumable2();

    #region Music and Sound
    public void TurnSound()
    {
        GlobalValue.isSound = !GlobalValue.isSound;
        soundImage.sprite = GlobalValue.isSound ? soundImageOn : soundImageOff;

        SoundManager.SoundVolume = GlobalValue.isSound ? 1 : 0;
        SoundManager.Click();
    }

    public void TurnMusic()
    {
        GlobalValue.isMusic = !GlobalValue.isMusic;
        musicImage.sprite = GlobalValue.isMusic ? musicImageOn : musicImageOff;

        SoundManager.MusicVolume = GlobalValue.isMusic ? SoundManager.Instance.musicsGameVolume : 0;
        SoundManager.Click();
    }
    
    //public void RewardCoins()
    //{
        
    //    StartCoroutine(Coins());
       
    //}
    //IEnumerator Coins()
    //{
        //if (Application.internetReachability != NetworkReachability.NotReachable && GoogleAdMobController.isAdmobInitialized)
        //{

        //    adMightApear.SetActive(true);
        //    //adNotAvailable.SetActive(false);
        //    yield return new WaitForSecondsRealtime(1f);
        //    //GoogleAdMobController.Instance.ShowRewardedAd(AddCoins);
        //    adMightApear.SetActive(false);
        //    print("Love Ho gaya");
        //}
        //else
        //{

        //    adMightApear.SetActive(false);
        //    adNotAvailable.SetActive(true);
        //    yield return new WaitForSecondsRealtime(1.5f);
        //    adNotAvailable.SetActive(false);

        //}
    //}


    public void AddCoins()
    {
        GlobalValue.SavedCoins += 100;
        print("Added");
    }
    #endregion
}