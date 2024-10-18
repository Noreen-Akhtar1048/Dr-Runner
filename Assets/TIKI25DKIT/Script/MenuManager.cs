using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using GameAnalyticsSDK;
using System.Xml;
using Firebase.Analytics;
using TMPro;
using UnityEngine.Video;

public class MenuManager : MonoBehaviour
{
    //[SerializeField] VideoPlayer V1, V2, V3, V4;
    

    public static MenuManager Instance;
    
    public GameObject uI, gameOver, finish, pauseUI, askForContinue/*, LoadingUI*/, storyBoarding;
    public GameObject InAppPanel,NECPanel;
    public Text[] txtStars;

    public Sprite[] imgStars;
    //public Sprite[] LvlObjectives;
    public Image LvlTask;
    public GameObject FinishPoint;
    //public bool lvlobj;
    public GameObject[] Tasks;
    public static int tasksDone = 0;

    public Text[] txtLives;
    public Text[] txtLevels;
    public Text bulletsText;
    public Text txtBullet, txtCoins;
    public GameObject[] butNext;

    [Header("Progressing Bar")]
    public Slider progressSlider;
    float startPos/*, finishPos*/;
    public int levelNumber;
    [Header("Jetpack bar")]
    public Slider jetpackSlider;
    public Slider waterlevelSlider;
    public Text txtJetpackRemainPercent;
    public Text txtwaterRemainPercent;
    public int coin;
    public Image star1, star2, star3;
    //public Color collectColor = Color.yellow;
    bool isCollectStar1, isCollectStar2, isCollectStar3;
    public GameObject /*adMightApear, adNotAvailable,*/ bulletButton;
    [Header("----------------GamePlay----------------------------")]
    public Button Rocketbtn;
       public Button gunfirebtn,get1000Coins,get25000Coins;

    public GameObject BombBtn, bombUI;
    public TextMeshProUGUI BulletText;

    public GameObject RocketBtn, GunfireBtn;

    #region STARS
    [Header("Sound and Music")]
    public Image soundImage;
    public Image musicImage;
    public Sprite soundImageOn, soundImageOff, musicImageOn, musicImageOff;
    public bool gameOverCheck;

    public static bool starCollectedCheck;
    public void CollectStar(int ID)
    {
        switch (ID)
        {
            case 1:
                star1.sprite = imgStars[0];
                isCollectStar1 = true;
                break;
            case 2:
                star2.sprite = imgStars[1];
                isCollectStar2 = true;
                break;
            case 3:
                star3.sprite = imgStars[2];
                isCollectStar3 = true;
                break;
            default:
                break;
        }
    }


    

    #endregion

    private void OnEnable()
    {

        if (isCollectStar1)
        {
            star1.sprite = imgStars[0];
        }
        if (isCollectStar2)
        {
            star2.sprite = imgStars[1];
        }
        if (isCollectStar3)
        {
            star3.sprite = imgStars[0];
        }
    }

    private void Awake()
    {
        Instance = this;
        PlayerPrefs.SetInt("Bullets", GlobalValue.Bullets);
        // Debug.Log("Bullets "+GlobalValue.Bullets);
        if (PlayerPrefs.GetInt("Bullets") >= 1)
        {
            bulletButton.SetActive(false);
        }
        //ShowLvlObj();
        
    }
    void purchase1000Coins()
    {
        IAPManager.instance.OnPurchaseConsumable1();
      
    }
    void purchase500Coins()
    {
        IAPManager.instance.OnPurchaseConsumable2();
       

    }
    void Start()
    {
        Time.timeScale = 1;
        GoogleAdMobController.Instance.ShowBanner();
        GoogleAdMobController.Instance.HideRectBanner();
        txtLevels[0].text = "Level " + GlobalValue.levelPlaying;
        if (SceneManager.GetActiveScene().buildIndex < 12)
        {
            FinishPoint.SetActive(false);

        }
        else
        {
            FinishPoint.SetActive(true);
        }

        //if (SceneManager.GetActiveScene().buildIndex==21) 
        //{ 
        //    V4.loopPointReached += offStoryLVL20;
        //}
        if (SceneManager.GetActiveScene().buildIndex > 21)
        {
           
            BombBtn.SetActive(false);
            bulletButton.SetActive(false);
            bombUI.SetActive(false);
        }

        get1000Coins.onClick.AddListener(purchase1000Coins);
        get25000Coins.onClick.AddListener(purchase500Coins);
        BulletText.text = "0";
        InAppPanel.SetActive(false);
       
        if (!PlayerPrefs.HasKey("FirstRun"))
        {
            PlayerPrefs.SetInt("FirstRun", 0);

        }
        if (!PlayerPrefs.HasKey("RetryStory1"))
        {
            PlayerPrefs.SetInt("RetryStory1", 0);

        }
        if (!PlayerPrefs.HasKey("RetryStory2"))
        {
            PlayerPrefs.SetInt("RetryStory2", 0);

        }
        if (!PlayerPrefs.HasKey("RetryStory3"))
        {
            PlayerPrefs.SetInt("RetryStory3", 0);

        }
      

        

        PlayerPrefs.SetInt("SavedLive", 2);
        //if (SceneManager.GetActiveScene().buildIndex == 2)
        //{

        //    if (PlayerPrefs.GetInt("RetryStory1") == 0)
        //    {
        //        storyBoarding.SetActive(true);
        //        V1.loopPointReached += V1_loopPointReached;
                

                
        //    }
        //    else if (PlayerPrefs.GetInt("RetryStory1") == 1)
        //    {
        //        V1.loopPointReached -= V1_loopPointReached;
        //        V2.loopPointReached -= V2_loopPointReached;
        //        storyBoarding.SetActive(false);
        //    }
        //}
        
        if(SceneManager.GetActiveScene().buildIndex < 22)
        {
            RocketBtn.SetActive(false);
            GunfireBtn.SetActive(false);
        }

        //if (SceneManager.GetActiveScene().buildIndex == 12)
        //{

        //    if (PlayerPrefs.GetInt("RetryStory2") == 0)
        //    {
        //        storyBoarding.SetActive(true);
        //        V3.loopPointReached += offStoryLVL11;
        //    }
        //    else if (PlayerPrefs.GetInt("RetryStory2") == 1)
        //    {
        //        V3.loopPointReached -= offStoryLVL11;
        //        storyBoarding.SetActive(false);
        //    }
        //}

        

       
        //if (PlayerPrefs.GetInt("RetryStory3") == 1)
        //{
        //    storyBoarding.SetActive(false);
        //    V4.loopPointReached -= V4_loopPointReached;
        //}

        
       
        
        print("Tasks Done" + tasksDone);

        levelNumber = MainMenu_Level.instance.levelNumber;



        //FinishPoint.transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;

        uI.SetActive(true);
        gameOver.SetActive(false);
        pauseUI.SetActive(false);
        askForContinue.SetActive(false);
        //LoadingUI.SetActive(false);
        coin = 0;
        //StartCoroutine(ShowDefaultBanner());
        if (Time.timeScale == 0)
            Time.timeScale = 1;

        if (GameManager.Instance)
            startPos = GameManager.Instance.Player.transform.position.x;

        //var hasFinishPoint = GameObject.FindGameObjectWithTag("Finish");
        //if (hasFinishPoint)
        //  finishPos = hasFinishPoint.transform.position.x;
        //else
        // Debug.LogError("NO FINISH POINT ON SCENE!");

        //foreach (var txt in txtLevels)
        //{
        //    if (GlobalValue.levelPlaying == -1)
        //        txt.text = "TEST ALL FEATURES";
        //    else
        //        txt.text = "Level " + GlobalValue.levelPlaying;
        //}
       
           
            txtLevels[0].text = "Level " + GlobalValue.levelPlaying;
        

        if (soundImage)
            soundImage.sprite = GlobalValue.isSound ? soundImageOn : soundImageOff;
        if (musicImage)
            musicImage.sprite = GlobalValue.isMusic ? musicImageOn : musicImageOff;
        if (!GlobalValue.isSound)
            SoundManager.SoundVolume = 0;
        if (!GlobalValue.isMusic)
            SoundManager.MusicVolume = 0;
        // CheckBullets();

        //GameAnalytics.NewDesignEvent("LevelStarted_ " + GlobalValue.levelPlaying);
        
        if (GlobalValue.Bullets == 2)
        {
            bulletButton.SetActive(false);
        }
        
    }

    private void V4_loopPointReached(VideoPlayer source)
    {
        throw new System.NotImplementedException();
    }

    private void V3_loopPointReached(VideoPlayer source)
    {
        throw new System.NotImplementedException();
    }

    //private void V2_loopPointReached(VideoPlayer source)
    //{
    //    offStoryLVL1p2();
    //}

    //private void V1_loopPointReached(VideoPlayer source)
    //{
    //    offStoryLVL1();
    //}

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
    #endregion

    void Update()
    {

        
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 0;
            }

        }


        foreach (var txt in txtStars)
        {
            txt.text = GlobalValue.SavedCoins.ToString();
        }

        foreach (var txt in txtLives)
        {
            txt.text = GameManager.Instance.isTestLevel ? "Test level" : GlobalValue.SavedLive.ToString();
        }
        
        txtCoins.text = GameManager.Instance.isTestLevel ? "Test level" : GlobalValue.SavedNewCoins.ToString();
        
        txtBullet.text = GameManager.Instance.isTestLevel ? "Test Level" : (GlobalValue.Bullets + "");


        //progressSlider.value = Mathf.InverseLerp(startPos, finishPos, GameManager.Instance.Player.transform.position.x);

        //update jetpack
        jetpackSlider.gameObject.SetActive(GameManager.Instance.Player.isJetpackActived);
        waterlevelSlider.gameObject.SetActive(GameManager.Instance.Player.PlayerState == PlayerState.Water);
        jetpackSlider.value = GameManager.Instance.Player.jetpackRemainTime / GameManager.Instance.Player.jetpackDrainTimeOut;
        waterlevelSlider.value = GameManager.Instance.Player.waterlevelRemainTime;// GameManager.Instance.Player.waterlevelwaDrainTimeOut;
        txtJetpackRemainPercent.text = ((GameManager.Instance.Player.jetpackRemainTime / GameManager.Instance.Player.jetpackDrainTimeOut) * 100).ToString("0") + "%";
        //  txtwaterRemainPercent.text = ((GameManager.Instance.Player.waterlevelRemainTime / GameManager.Instance.Player.waterlevelwaDrainTimeOut) * 100).ToString("0") + "%";
        float percentValue = (GameManager.Instance.Player.waterlevelRemainTime / waterlevelSlider.maxValue) * 100;
        percentValue = Mathf.Clamp(percentValue, 0, 100);
        txtwaterRemainPercent.text = (percentValue).ToString("0") + "%";
        if (GameManager.Instance.Player.waterlevelRemainTime >= waterlevelSlider.maxValue &&
            GameManager.Instance.Player.PlayerState == PlayerState.Water)
        {
            if (!gameOverCheck)
            {
                GameManager.Instance.gameState = GameManager.GameState.GameOver;
                // GameOver();
                ShowAskForContinue();
                gameOverCheck = true;
            }

        }

        CheckBullets();
        //checkLvlObj();

        if (tasksDone == Tasks.Length && SceneManager.GetActiveScene().buildIndex < 12)
        {
            FinishPoint.SetActive(true);
            
        }


        if ((isCollectStar1 && isCollectStar2 && isCollectStar3 == true) && starCollectedCheck == false)
        {
            GlobalValue.SavedLive += 1;
            starCollectedCheck = true;
        }


    }
    //public GameObject SB1, SB2;
    //public void offStoryLVL1()
    //{
    //    SB1.SetActive(false);
    //    SB2.SetActive(true);
        
        
    //    V2.loopPointReached += V2_loopPointReached;

    //}

    //public void offStoryLVL1p2()
    //{
    //    PlayerPrefs.SetInt("RetryStory1", 1);
    //    SB2.SetActive(false);
    //    storyBoarding.SetActive(false);
       
    //}


    //public void offStoryLVL11(VideoPlayer source)
    //{
    //    PlayerPrefs.SetInt("RetryStory2", 1);
        
    //    storyBoarding.SetActive(false);
        
    //}

    //public void offStoryLVL20( VideoPlayer source)
    //{
        
        
    //    storyBoarding.SetActive(false);
    //    PlayerPrefs.SetInt("RetryStory3", 1);
    //    print("i am completed");
    //    StartCoroutine(ShowRecBannerAd());

    //}

    public void Finish()
    {
        Invoke("FinishCo", 2);
    }

    void CheckBullets()
    {
        if (PlayerPrefs.GetInt("Bullets") >= 1)
        {

            bulletButton.SetActive(false);

        }
        else if (PlayerPrefs.GetInt("Bullets") == 0)
        {

            bulletButton.SetActive(true);
        }

    }
    void FinishCo()
    {
        //foreach (var but in butNext)
        //{
        //    if (!GameManager.Instance.isTestLevel)
        //        but.SetActive(GlobalValue.levelPlaying < GlobalValue.LevelHighest);
        //    else
        //    {
        //        but.SetActive(false);
        //    }
        //}
        //  GameAnalytics.NewDesignEvent ("LevelCompleted_ "+GlobalValue.levelPlaying);
        if (SceneManager.GetActiveScene().buildIndex == 21)
        {
            uI.SetActive(false);
            finish.SetActive(true);

            FirebaseAnalytics.LogEvent("LevelCompleted_" + GlobalValue.levelPlaying);
        }
        else
        {
            StartCoroutine(ShowGoogleAdmob());
            StartCoroutine(ShowRecBannerAd());
            uI.SetActive(false);
            finish.SetActive(true);

            FirebaseAnalytics.LogEvent("LevelCompleted_" + GlobalValue.levelPlaying);
        }
        //StartCoroutine(ShowDefaultBanner());
        
        
    }

    public void ShowAskForContinue()
    {
        Invoke("ShowAskForContinueCo", 1);
    }

    void ShowAskForContinueCo()
    {
        uI.SetActive(false);
        askForContinue.SetActive(true);

        StartCoroutine(ShowRecBannerAd());
    }

    public void Continue()
    {
        uI.SetActive(true);
        askForContinue.SetActive(false);
        GameManager.Instance.Continue();
        GoogleAdMobController.Instance.ShowBanner();

        GoogleAdMobController.Instance.HideRectBanner();
        BulletText.text = "0";
        
    }

    public void GameOver()
    {
        Invoke("GameOverCo", 1);
    }

    void GameOverCo()
    {
        //foreach (var but in butNext)
        //{
        //    if (!GameManager.Instance.isTestLevel)
        //        but.SetActive(GlobalValue.levelPlaying < GlobalValue.LevelHighest);
        //    else
        //        but.SetActive(false);
        //}
        StartCoroutine(ShowGoogleAdmob());
        
        StartCoroutine(ShowRecBannerAd());
        uI.SetActive(false);
        gameOver.SetActive(true);
        

        //GameAnalytics.NewDesignEvent("LevelFaild_ " + GlobalValue.levelPlaying);
        FirebaseAnalytics.LogEvent("LevelFailed_" + GlobalValue.levelPlaying);
    }

    public void Restart()
    {
        tasksDone = 0;
        GoogleAdMobController.Instance.HideRectBanner();
        GoogleAdMobController.Instance.ShowBanner();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

    public void Pause(bool pause)
    {
        pauseUI.SetActive(pause);
        Time.timeScale = pause ? 0 : 1;
        if (!pause)
        {
            print("If");
           GoogleAdMobController.Instance.HideRectBanner();
            StartCoroutine(ShowDefaultBanner());
        }
        else
        {
            print("else");
            SoundManager.Instance.PauseMusic(pause);
            StartCoroutine(ShowUnityAdmob());
            StartCoroutine(ShowRecBannerAd());
        }

    }

    public void NextLevel()
    {
        tasksDone = 0;
        GoogleAdMobController.Instance.ShowBanner();

        GoogleAdMobController.Instance.HideRectBanner();
        GlobalValue.levelPlaying++;
        //LoadingUI.SetActive(true);
        SceneManager.LoadSceneAsync("Level " + GlobalValue.levelPlaying);

        
        if (SceneManager.GetActiveScene().buildIndex == 31)
        {
            SceneManager.LoadScene(1);
        }

    }

    public void SkipLevel()
    {
        tasksDone = 0;
        if (PlayerPrefs.GetInt("Removeads") != 1)
        {
            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                GoogleAdMobController.Instance.ShowRewardedAd();
                
                GameManager.Instance.SkipLvlGame();
                GlobalValue.levelPlaying++;
                ////LoadingUI.SetActive(true);
                SceneManager.LoadSceneAsync("Level " + GlobalValue.levelPlaying);
                GoogleAdMobController.Instance.ShowBanner();

                GoogleAdMobController.Instance.HideRectBanner();

                if (SceneManager.GetActiveScene().buildIndex == 31)
                {
                    SceneManager.LoadScene(1);
                }
            }
        }
        else
        {
            GameManager.Instance.FinishGame();


            GlobalValue.levelPlaying++;
            ////LoadingUI.SetActive(true);
            SceneManager.LoadSceneAsync("Level " + GlobalValue.levelPlaying);


            if (SceneManager.GetActiveScene().buildIndex == 31)
            {
                SceneManager.LoadScene(1);
            }
        }
        print("Level " + GlobalValue.levelPlaying);

    }



    public void Home()
    {
        

        GoogleAdMobController.Instance.HideRectBanner();
        
        GlobalValue.levelPlaying = -1;
        //LoadingUI.SetActive(true);
        SceneManager.LoadSceneAsync("HomeScene");
        PlayerPrefs.SetInt("RemoveAdOpen", 1);
    }


    IEnumerator ShowUnityAdmob()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            //adMightApear.SetActive(true);
            yield return new WaitForSecondsRealtime(0f);
            //adMightApear.SetActive(false);
            //GoogleAdMobController.Instance.ShowInterstitialAd();

        }
    }
    IEnumerator ShowGoogleAdmob()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            //adMightApear.SetActive(true);
            yield return new WaitForSecondsRealtime(0f);
            //adMightApear.SetActive(false);
            GoogleAdMobController.Instance.ShowInterstitialAd();

        }
    }
    IEnumerator ShowRecBannerAd()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            yield return new WaitForSecondsRealtime(0.01f);
            GoogleAdMobController.Instance.ShowRectBanner();
            GoogleAdMobController.Instance.HideBanner();
        }
    }

    IEnumerator ShowDefaultBanner()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            yield return new WaitForSecondsRealtime(0.01f);
            GoogleAdMobController.Instance.ShowBanner();
        }
    }


    public void RewardCoins()
    {
        GoogleAdMobController.Instance.ShowRewardedAdCoins();

    }


    public void RewardBullets()
    {

        StartCoroutine(Bullets());
       
    }
    IEnumerator Bullets()
    {
        if (PlayerPrefs.GetInt("Removeads") != 1)
        {
            if (Application.internetReachability != NetworkReachability.NotReachable)
            {

                //adMightApear.SetActive(true);
                yield return new WaitForSecondsRealtime(0f);
                GoogleAdMobController.Instance.ShowRewardedAdBullets();
                //AddBullets();
                //adMightApear.SetActive(false);


            }
        }
        else
        {
            AddBullets();
        }
        //else
        //{
        //    //adMightApear.SetActive(false);
        //    //adNotAvailable.SetActive(true);
        //    yield return new WaitForSecondsRealtime(1.5f);
        //    //adNotAvailable.SetActive(false);
        //}

    }

   

    public void AddBullets()
    {
        coin = 2;
        txtBullet.text = (coin).ToString();
        PlayerPrefs.SetInt("Bullets", coin);
    }



    public void skipStory()
    {
        PlayerPrefs.SetInt("FirstRun", 1);
        storyBoarding.SetActive(false);
        
    }



    public void showTaskPanel()
    {
        StartCoroutine(ShowRecBannerAd());

    }
    public void HideTaskPanel()
    {
        StartCoroutine(ShowDefaultBanner());
        GoogleAdMobController.Instance.HideRectBanner();
    }

    public void ShowInAppPanel()
    {
        InAppPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void HideInAppPanel()
    {
        InAppPanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void ShowNECPanel()
    {
        NECPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void HideNECPanel()
    {
        NECPanel.SetActive(false);
        Time.timeScale = 1f;
    }

}
