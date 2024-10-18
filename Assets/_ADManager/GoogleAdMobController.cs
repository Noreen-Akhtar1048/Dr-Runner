using UnityEngine.Events;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine.SceneManagement;


public partial class GoogleAdMobController : MonoBehaviour
{
    public static class TestIDs
    {
        public const string AppId = "ca-app-pub-3156473355527189~7231685190";
        public const string BannerId = "ca-app-pub-3940256099942544/6300978111";
        public const string RectBannerId = "ca-app-pub-3940256099942544/6300978111";
        public const string InterstitialId = "ca-app-pub-3940256099942544/1033173712";
        public const string RewardedId = "ca-app-pub-3940256099942544/5224354917";
        public const string AppOpenId = "ca-app-pub-3940256099942544/3419835294";
    }

    [Serializable]
    public class AdsIds
    {
        [SerializeField] public string AppId = "[App ID]";
        [SerializeField] public string BannerId = "[Banner ID]";
        [SerializeField] public string RectBannerId = "[RectBanner ID]";
        [SerializeField] public string InterstitialId = "[Interstitial ID]";
        [SerializeField] public string RewardedId = "[Rewarded ID]";
        [SerializeField] public string AppOpenId = "[AppOpen ID]";
    }

    //public AdsIds TestIds = new AdsIds();
    public AdsIds AndroidIds = new AdsIds();
    //public AdsIds IosIds = new AdsIds();
    AdsIds currentIds = new AdsIds();

    public AdPosition BannerAdPosition;
    public AdPosition RecBannerAdPosition;
    public bool TestAds;
    public bool ShowBannerFromGameLaunching;
    public bool IsAppOpenAd;
    public int RamRangeToRestrictAds = 1024;
    //public Text BannerStatusText;
    //public Text InterstitialStatusText;
    //public Text RewardedStatusText;
    public GameObject DummyRewardedAd;
    //[Range(1024, 2048)]


    private readonly TimeSpan APPOPEN_TIMEOUT = TimeSpan.FromHours(4);
    private DateTime appOpenExpireTime;
    private AppOpenAd appOpenAd;
    private BannerView bannerView;
    private BannerView recBannerView;
    private InterstitialAd interstitialAd;
    private RewardedAd rewardedAd;
    private bool isShowingAppOpenAd;

    //private float deltaTime;
    //private RewardedInterstitialAd rewardedInterstitialAd;
    private bool pluginInitialized;
    [Space(30)]
    [HideInInspector] public UnityEvent OnAdLoadedEvent;
    [HideInInspector] public UnityEvent OnAdFailedToLoadEvent;
    [HideInInspector] public UnityEvent OnAdOpeningEvent;
    [HideInInspector] public UnityEvent OnAdFailedToShowEvent;
    [HideInInspector] public UnityEvent OnUserEarnedRewardEvent;
    [HideInInspector] public UnityEvent OnAdClosedEvent;
    //public bool showFpsMeter = true;
    //public Text fpsMeter;
    //public Text statusText;


    #region UNITY MONOBEHAVIOR METHODS

    public static GoogleAdMobController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(HandleInitCompleteAction);
        //AppLovin.Initialize();

        //List<String> deviceIds = new List<String>() { AdRequest.TestDeviceSimulator };

        // Add some test device IDs (replace with your own device IDs).
        //#if UNITY_IPHONE
        //        deviceIds.Add("96e23e80653bb28980d3f40beb58915c");
        //#elif UNITY_ANDROID
        //            deviceIds.Add("75EF8D155528C04DACBBA6F36F433035");
        //#endif

        // Configure TagForChildDirectedTreatment and test device IDs.
        //RequestConfiguration requestConfiguration =
        //    new RequestConfiguration.Builder()
        //    .SetTagForChildDirectedTreatment(TagForChildDirectedTreatment.Unspecified)
        //    /*.SetTestDeviceIds(deviceIds)*/.build();
        //MobileAds.SetRequestConfiguration(requestConfiguration);

        //UnityAds.SetConsentMetaData("Unity", true);
        //#if UNITY_IOS
        //            MobileAds.SetiOSAppPauseOnBackground(true);
        //#endif

        // Listen to application foreground / background events.
        if (IsAppOpenAd)
            AppStateEventNotifier.AppStateChanged += OnAppStateChanged;

      ///MobileAds.raise

        //ShowBanner();
    }

    private void HandleInitCompleteAction(InitializationStatus initstatus)
    {
        if (TestAds)
        {
            //currentIds = TestIds;
            currentIds.AppId = "ca-app-pub-3156473355527189~7231685190";
            currentIds.BannerId = "ca-app-pub-3940256099942544/6300978111";
            currentIds.RectBannerId = "ca-app-pub-3940256099942544/6300978111";
            currentIds.InterstitialId = "ca-app-pub-3940256099942544/1033173712";
            currentIds.RewardedId = "ca-app-pub-3940256099942544/5224354917";
            currentIds.AppOpenId = "ca-app-pub-3940256099942544/3419835294";
        }
        else
        {
#if UNITY_ANDROID || PLATFORM_ANDROID
            currentIds = AndroidIds;
#elif UNITY_IOS
            currentIds = IosIds;
#endif
        }

        //Debug.Log("Initialization complete.");

        // Callbacks from GoogleMobileAds are not guaranteed to be called on
        // the main thread.
        // In this example we use MobileAdsEventExecutor to schedule these calls on
        // the next Update() loop.
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            //statusText.text = "Initialization complete.";
            pluginInitialized = true;
            if (SceneManager.GetActiveScene().buildIndex > 0)
                RequestBannerAd();
            RequestRectBannerAd();
            RequestAndLoadInterstitialAd();
            RequestAndLoadRewardedAd();
            if (IsAppOpenAd)
                RequestAndLoadAppOpenAd();
        });
    }


    //private void Update()
    //{
    //    if (showFpsMeter)
    //    {
    //        fpsMeter.gameObject.SetActive(true);
    //        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
    //        float fps = 1.0f / deltaTime;
    //        fpsMeter.text = string.Format("{0:0.} fps", fps);
    //    }
    //    else
    //    {
    //        fpsMeter.gameObject.SetActive(false);
    //    }
    //}

    #endregion

    #region HELPER METHODS

    private AdRequest CreateAdRequest()
    {
        return new AdRequest();
    }

    #endregion

    #region BANNER ADS

    public void RequestBannerAd()
    {
        //if (Application.internetReachability == NetworkReachability.NotReachable)
        //    return;

        //PrintStatus("Requesting Banner ad.");

        //        // These ad units are configured to always serve test ads.
        //#if UNITY_EDITOR
        //        string adUnitId = "unused";
        //#elif UNITY_ANDROID
        //        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
        //#elif UNITY_IPHONE
        //        string adUnitId = "ca-app-pub-3940256099942544/2934735716";
        //#else
        //        string adUnitId = "unexpected_platform";
        //#endif

        // Clean up banner before reusing
        if (bannerView != null)
        {
            bannerView.Destroy();
        }

        // Create a 320x50 banner at top of the screen
        //AdSize adSize = new AdSize(600, 25);
        bannerView = new BannerView(currentIds.BannerId, AdSize.Banner, BannerAdPosition);

        // Add Event Handlers
        bannerView.OnBannerAdLoaded += () =>
        {
            PrintStatus("Banner ad loaded.");
            //BannerStatusText.text = "Banner Loaded";
            OnAdLoadedEvent.Invoke();

        };
        bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            PrintStatus("Banner ad failed to load with error: " + error.GetMessage());
            //BannerStatusText.text = "Banner Failed To Load";
            OnAdFailedToLoadEvent.Invoke();
        };
        bannerView.OnAdImpressionRecorded += () =>
        {
            PrintStatus("Banner ad recorded an impression.");
        };
        bannerView.OnAdClicked += () =>
        {
            PrintStatus("Banner ad recorded a click.");
        };
        bannerView.OnAdFullScreenContentOpened += () =>
        {
            PrintStatus("Banner ad opening.");
            OnAdOpeningEvent.Invoke();
        };
        bannerView.OnAdFullScreenContentClosed += () =>
        {
            PrintStatus("Banner ad closed.");
            OnAdClosedEvent.Invoke();
        };
        bannerView.OnAdPaid += (AdValue adValue) =>
        {
            string msg = string.Format("{0} (currency: {1}, value: {2}",
                                        "Banner ad received a paid event.",
                                        adValue.CurrencyCode,
                                        adValue.Value);
            PrintStatus(msg);
        };

        // Load a banner ad
        bannerView.LoadAd(CreateAdRequest());
        //if (!ShowBannerFromGameLaunching)
        //    bannerView.Hide();


    }

    public void ShowBanner()
    {
        if (PlayerPrefs.GetInt("Removeads") == 1)
        {
            return;
        }
        else
        {
            if (bannerView != null)
                bannerView.Show();
            else
                RequestBannerAd();
            Debug.Log("Showing Banner", gameObject);
        }
    }

    public void HideBanner()
    {
        if (this.bannerView != null)
            this.bannerView.Hide();
    }

    public void DestroyBannerAd()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
        }
    }

    public void RequestRectBannerAd()
    {
        //if (Application.internetReachability == NetworkReachability.NotReachable)
        //    return;

        //PrintStatus("Requesting Rec Banner ad.");

        //        // These ad units are configured to always serve test ads.
        //#if UNITY_EDITOR
        //        string adUnitId = "unused";
        //#elif UNITY_ANDROID
        //        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
        //#elif UNITY_IPHONE
        //        string adUnitId = "ca-app-pub-3940256099942544/2934735716";
        //#else
        //        string adUnitId = "unexpected_platform";
        //#endif

        // Clean up banner before reusing
        if (recBannerView != null)
        {
            recBannerView.Destroy();
        }

        // Create a 320x50 banner at top of the screen
        recBannerView = new BannerView(currentIds.RectBannerId, AdSize.MediumRectangle, RecBannerAdPosition);

        // Add Event Handlers
        recBannerView.OnBannerAdLoaded += () =>
        {
            PrintStatus("Rec Banner ad loaded.");
            OnAdLoadedEvent.Invoke();
        };
        recBannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            PrintStatus("Rec Banner ad failed to load with error: " + error.GetMessage());
            OnAdFailedToLoadEvent.Invoke();
        };
        recBannerView.OnAdImpressionRecorded += () =>
        {
            PrintStatus("Rec Banner ad recorded an impression.");
        };
        recBannerView.OnAdClicked += () =>
        {
            PrintStatus("Rec Banner ad recorded a click.");
        };
        recBannerView.OnAdFullScreenContentOpened += () =>
        {
            PrintStatus("Rec Banner ad opening.");
            OnAdOpeningEvent.Invoke();
        };
        recBannerView.OnAdFullScreenContentClosed += () =>
        {
            PrintStatus("Rec Banner ad closed.");
            OnAdClosedEvent.Invoke();
        };
        recBannerView.OnAdPaid += (AdValue adValue) =>
        {
            string msg = string.Format("{0} (currency: {1}, value: {2}",
                                        "Rec Banner ad received a paid event.",
                                        adValue.CurrencyCode,
                                        adValue.Value);
            PrintStatus(msg);
        };

        // Load a banner ad
        recBannerView.LoadAd(CreateAdRequest());
        recBannerView.Hide();
    }

    public void ShowRectBanner()
    {


        if (PlayerPrefs.GetInt("Removeads") == 1)
        {
        }
        else
        {
            if (recBannerView != null)
                recBannerView.Show();
            else
            {
                RequestRectBannerAd();
                if (ShowBannerFromGameLaunching)
                {
                    if (bannerView == null)
                        RequestBannerAd();
                }
            }
        }
    }

    public void HideRectBanner()
    {
        if (this.recBannerView != null)
            this.recBannerView.Hide();
    }

    #endregion

    #region INTERSTITIAL ADS

    public void RequestAndLoadInterstitialAd()
    {
        if (SystemInfo.systemMemorySize < RamRangeToRestrictAds) return;

        //if (Application.internetReachability == NetworkReachability.NotReachable)
        //    return;

        //PrintStatus("Requesting Interstitial ad.");

        // Clean up interstitial before using it
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
        }

        #region Old Loading

        //interstitialAd = new InterstitialAd(currentIds.InterstitialId);

        //// Add Event Handlers
        //interstitialAd.OnAdLoaded += (sender, args) =>
        //{
        //    //PrintStatus("Interstitial ad loaded.");
        //    InterstitialStatusText.text = "Interstitial ad loaded.";
        //    OnAdLoadedEvent.Invoke();
        //};
        //interstitialAd.OnAdFailedToLoad += (sender, args) =>
        //{
        //    //PrintStatus("Interstitial ad failed to load with error: " + args.LoadAdError.GetMessage());
        //    InterstitialStatusText.text = "Interstitial ad failed to loaded.";
        //    OnAdFailedToLoadEvent.Invoke();
        //};
        //interstitialAd.OnAdOpening += (sender, args) =>
        //{
        //    //PrintStatus("Interstitial ad opening.");
        //    OnAdOpeningEvent.Invoke();
        //};
        //interstitialAd.OnAdClosed += (sender, args) =>
        //{
        //    //PrintStatus("Interstitial ad closed.");
        //    //InterstitialStatusText.text = "Interstitial ad closed.";
        //    RequestAndLoadInterstitialAd();
        //    OnAdClosedEvent.Invoke();
        //};
        //interstitialAd.OnAdDidRecordImpression += (sender, args) =>
        //{
        //    //PrintStatus("Interstitial ad recorded an impression.");
        //};
        //interstitialAd.OnAdFailedToShow += (sender, args) =>
        //{
        //    //PrintStatus("Interstitial ad failed to show.");
        //};
        //interstitialAd.OnPaidEvent += (sender, args) =>
        //{
        //    string msg = string.Format("{0} (currency: {1}, value: {2}",
        //                                "Interstitial ad received a paid event.",
        //                                args.AdValue.CurrencyCode,
        //                                args.AdValue.Value);
        //    //PrintStatus(msg);
        //};

        //// Load an interstitial ad
        //interstitialAd.LoadAd(CreateAdRequest());

        #endregion


        #region New Loading

        // Load an interstitial ad
        InterstitialAd.Load(currentIds.InterstitialId, CreateAdRequest(),
            (InterstitialAd ad, LoadAdError loadError) =>
            {
                if (loadError != null)
                {
                    PrintStatus("Interstitial ad failed to load with error: " +
                        loadError.GetMessage());
                    //InterstitialStatusText.text = "Interstitial failed to load" + loadError.GetMessage();
                    return;
                }
                else if (ad == null)
                {
                    PrintStatus("Interstitial ad failed to load.");
                    //InterstitialStatusText.text = "Interstitial failed to load";
                    return;
                }

                PrintStatus("Interstitial ad loaded.");
                //InterstitialStatusText.text = "Interstitial loaded";
                interstitialAd = ad;

                ad.OnAdFullScreenContentOpened += () =>
                {
                    PrintStatus("Interstitial ad opening.");
                    OnAdOpeningEvent.Invoke();
                };
                ad.OnAdFullScreenContentClosed += () =>
                {
                    PrintStatus("Interstitial ad closed.");
                    OnAdClosedEvent.Invoke();
                    RequestAndLoadInterstitialAd();

                    // on ad closed
                    CheckOldTimeScale();
                };
                ad.OnAdImpressionRecorded += () =>
                {
                    PrintStatus("Interstitial ad recorded an impression.");
                };
                ad.OnAdClicked += () =>
                {
                    PrintStatus("Interstitial ad recorded a click.");
                };
                ad.OnAdFullScreenContentFailed += (AdError error) =>
                {
                    PrintStatus("Interstitial ad failed to show with error: " +
                                error.GetMessage());
                };
                ad.OnAdPaid += (AdValue adValue) =>
                {
                    string msg = string.Format("{0} (currency: {1}, value: {2}",
                                               "Interstitial ad received a paid event.",
                                               adValue.CurrencyCode,
                                               adValue.Value);
                    PrintStatus(msg);
                };
            });

        #endregion
    }

    private async void CheckOldTimeScale()
    {
        await Task.Yield();

        // if in gameplay
        if (GameManager.Instance)
        {
            Time.timeScale = 0f;
        }
    }

    public void ShowInterstitialAd()
    {
        
        if (PlayerPrefs.GetInt("Removeads") != 1)
        {
            if (SystemInfo.systemMemorySize < RamRangeToRestrictAds) return;
            if (interstitialAd != null && interstitialAd.CanShowAd())
            {
                interstitialAd.Show();
            }
            else
            {
                RequestAndLoadInterstitialAd();
                //PrintStatus("Interstitial ad is not ready yet.");
            }
        }

        else
        {
            print("Removed Interstitial Ad");
        }

    }

    public void DestroyInterstitialAd()
    {
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
        }
    }

    #endregion

    #region REWARDED ADS

    public void RequestAndLoadRewardedAd()
    {
        //if (Application.internetReachability == NetworkReachability.NotReachable)
        //    return;

        //PrintStatus("Requesting Rewarded ad.");
        //#if UNITY_EDITOR
        //        string adUnitId = "unused";
        //#elif UNITY_ANDROID
        //        string adUnitId = "ca-app-pub-3940256099942544/5224354917";
        //#elif UNITY_IPHONE
        //        string adUnitId = "ca-app-pub-3940256099942544/1712485313";
        //#else
        //        string adUnitId = "unexpected_platform";
        //#endif

        #region Old Loading

        //// create new rewarded ad instance
        //rewardedAd = new RewardedAd(currentIds.RewardedId);

        //// Add Event Handlers
        //rewardedAd.OnAdLoaded += (sender, args) =>
        //{
        //    //PrintStatus("Reward ad loaded.");
        //    //RewardedStatusText.text = "Reward ad loaded.";
        //    OnAdLoadedEvent.Invoke();
        //};
        //rewardedAd.OnAdFailedToLoad += (sender, args) =>
        //{
        //    //PrintStatus("Reward ad failed to load.");
        //    //RewardedStatusText.text = "Reward ad failed to load." + args.LoadAdError.GetMessage();
        //    OnAdFailedToLoadEvent.Invoke();
        //};
        //rewardedAd.OnAdOpening += (sender, args) =>
        //{
        //    //PrintStatus("Reward ad opening.");
        //    OnAdOpeningEvent.Invoke();
        //};
        //rewardedAd.OnAdFailedToShow += (sender, args) =>
        //{
        //    //PrintStatus("Reward ad failed to show with error: " + args.AdError.GetMessage());
        //    OnAdFailedToShowEvent.Invoke();
        //};
        //rewardedAd.OnAdClosed += (sender, args) =>
        //{
        //    //PrintStatus("Reward ad closed.");
        //    //RewardedStatusText.text = "Reward ad watched.";
        //    RequestAndLoadRewardedAd();
        //    OnAdClosedEvent.Invoke();
        //};
        //rewardedAd.OnUserEarnedReward += (sender, args) =>
        //{
        //    //PrintStatus("User earned Reward ad reward: " + args.Amount);
        //    GrantRewards();
        //    OnUserEarnedRewardEvent.Invoke();
        //};
        //rewardedAd.OnAdDidRecordImpression += (sender, args) =>
        //{
        //    //PrintStatus("Reward ad recorded an impression.");
        //};
        //rewardedAd.OnPaidEvent += (sender, args) =>
        //{
        //    string msg = string.Format("{0} (currency: {1}, value: {2}",
        //                                "Rewarded ad received a paid event.",
        //                                args.AdValue.CurrencyCode,
        //                                args.AdValue.Value);
        //    //PrintStatus(msg);
        //};

        //// Create empty ad request
        //rewardedAd.LoadAd(CreateAdRequest());

        #endregion


        #region New Loading

        // create new rewarded ad instance
        RewardedAd.Load(currentIds.RewardedId, CreateAdRequest(),
            (RewardedAd ad, LoadAdError loadError) =>
            {
                if (loadError != null)
                {
                    PrintStatus("Rewarded ad failed to load with error: " +
                                loadError.GetMessage());
                    //RewardedStatusText.text = "Rewarded ad failed to load with error: " + loadError.GetMessage();
                    return;
                }
                else if (ad == null)
                {
                    PrintStatus("Rewarded ad failed to load.");
                    //RewardedStatusText.text = "Rewarded ad failed to load";
                    return;
                }

                PrintStatus("Rewarded ad loaded.");
                //RewardedStatusText.text = "Rewarded ad loaded";
                rewardedAd = ad;

                ad.OnAdFullScreenContentOpened += () =>
                {
                    PrintStatus("Rewarded ad opening.");
                    OnAdOpeningEvent.Invoke();
                };
                ad.OnAdFullScreenContentClosed += () =>
                {
                    PrintStatus("Rewarded ad closed.");
                    OnAdClosedEvent.Invoke();
                    RequestAndLoadRewardedAd();
                };
                ad.OnAdImpressionRecorded += () =>
                {
                    PrintStatus("Rewarded ad recorded an impression.");
                };
                ad.OnAdClicked += () =>
                {
                    PrintStatus("Rewarded ad recorded a click.");
                };
                ad.OnAdFullScreenContentFailed += (AdError error) =>
                {
                    PrintStatus("Rewarded ad failed to show with error: " +
                               error.GetMessage());
                };
                ad.OnAdPaid += (AdValue adValue) =>
                {
                    string msg = string.Format("{0} (currency: {1}, value: {2}",
                                               "Rewarded ad received a paid event.",
                                               adValue.CurrencyCode,
                                               adValue.Value);
                    PrintStatus(msg);
                };
            });

        #endregion
    }

    public void ShowRewardedAdBullets()
    {
        //if (TestAds)
        //{
        //    if (Application.internetReachability != NetworkReachability.NotReachable)
        //    {
        //        if (!givingReward)
        //            StartCoroutine(ShowTestingRewardedTestAd());
        //    }
        //}
        //else
        //{
        if (PlayerPrefs.GetInt("Removeads") != 1)
        {
            if (rewardedAd != null && rewardedAd.CanShowAd())
            {
                rewardedAd.Show((Reward reward) =>
                {
                    PrintStatus("Rewarded ad granted a reward: " + reward.Amount);
                    GrantRewards();
                });
            }
            else
            {
                RequestAndLoadRewardedAd();
                //Rewarded Video Not Avail
            }
        }
        else
        {
            print("Removed Rewarded Ad");
        }
    }


    public void ShowRewardedAdCoins()
    {
        //if (TestAds)
        //{
        //    if (Application.internetReachability != NetworkReachability.NotReachable)
        //    {
        //        if (!givingReward)
        //            StartCoroutine(ShowTestingRewardedTestAd());
        //    }
        //}
        //else
        //{

       /* if (PlayerPrefs.GetInt("Removeads") != 1)
        {*/
            if (rewardedAd != null && rewardedAd.CanShowAd())
            {
                rewardedAd.Show((Reward reward) =>
                {
                    // PrintStatus("Rewarded ad granted a reward: " + reward.Amount);
                    GrantRewardsCoins();

                });
            }
            else
            {
                RequestAndLoadRewardedAd();
                //Rewarded Video Not Avail
            }
        //}
        //else
        //{
        //    print("Removed Rewarded Ad");
        //}
        //}
    }
    void GrantRewardsCoins()
    {
        print("rewards Granted");

        if (GameManager.Instance)
        {
            // if in gameplay, then reward player with Refuel for TRUCK
            GlobalValue.SavedNewCoins += 50;
            return;
        }

        // but if in Menu



        //RewardedStatusText.text = "reward granted";
    }

    public void ShowRewardedAd()
    {
        //if (TestAds)
        //{
        //    if (Application.internetReachability != NetworkReachability.NotReachable)
        //    {
        //        if (!givingReward)
        //            StartCoroutine(ShowTestingRewardedTestAd());
        //    }
        //}
        //else
        //{

        if (PlayerPrefs.GetInt("Removeads") != 1)
        {
            if (rewardedAd != null && rewardedAd.CanShowAd())
            {
                rewardedAd.Show((Reward reward) =>
                {
                    // PrintStatus("Rewarded ad granted a reward: " + reward.Amount);
                    //GrantRewards();

                });
            }
            else
            {
                RequestAndLoadRewardedAd();
                //Rewarded Video Not Avail
            }
        }
        else 
        {
            print("Removed Rewarded Ad");
        }
        //}
    }

    public void ShowRewardedAd(Action rewardCallback)
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                //DebugStatus("Rewarded ad granted a reward: " + reward.Amount);
                rewardCallback?.Invoke();
            });
        }
        else
        {
            //Rewarded Video Not Avail
            RequestAndLoadRewardedAd();
        }
    }

    public void ShowRewardedAdRemoveAdPanel()
    {
        //if (TestAds)
        //{
        //    if (Application.internetReachability != NetworkReachability.NotReachable)
        //    {
        //        if (!givingReward)
        //            StartCoroutine(ShowTestingRewardedTestAd());
        //    }
        //}
        //else
        //{

        if (PlayerPrefs.GetInt("Removeads") != 1)
        {
            if (rewardedAd != null && rewardedAd.CanShowAd())
            {
                rewardedAd.Show((Reward reward) =>
                {
                    // PrintStatus("Rewarded ad granted a reward: " + reward.Amount);
                    HomeMenu.Instance.RemoveAdPanel.SetActive(false);

                });
            }
            else
            {
                RequestAndLoadRewardedAd();
                //Rewarded Video Not Avail
            }
        }
        else
        {
            print("Removed Rewarded Ad");
        }
        //}
    }




    


    #region Dummy Rewarded Ad

    //bool givingReward;
    //[HideInInspector]
    //public GameObject dummyRewardedAd;
    //Button closeButton;
    //IEnumerator ShowTestingRewardedTestAd()
    //{
    //    Time.timeScale = 1f;
    //    givingReward = true;
    //    dummyRewardedAd = Instantiate(DummyRewardedAd);
    //    Text rewardRemainingTime = GameObject.Find("rewardRemainingTime").GetComponent<Text>();
    //    GameObject _closeButton = GameObject.Find("Close ad btn");
    //    _closeButton.SetActive(false);
    //    closeButton = _closeButton.GetComponent<Button>();
    //    closeButton.onClick.AddListener(ButtonClicked);

    //    yield return new WaitForSeconds(1f);

    //    //var t = 1;
    //    //while (t > 0)
    //    //{
    //    //    t -= 1;
    //    //    rewardRemainingTime.text = "Reward will be given in " + t.ToString();
    //    //    yield return new WaitForSeconds(1f);
    //    //}
    //    rewardRemainingTime.text = "Reward Granted";
    //    _closeButton.SetActive(true);
    //    GrantRewards();
    //    givingReward = false;
    //    yield return null;
    //}

    //void ButtonClicked() => Destroy(dummyRewardedAd);

    #endregion

    void GrantRewards()
    {
        print("rewards Granted");

        if (GameManager.Instance)
        {
            // if in gameplay, then reward player with Refuel for TRUCK
            MenuManager.Instance.AddBullets();
            return;
        }

        // but if in Menu
       


        //RewardedStatusText.text = "reward granted";
    }

    

    //    public void RequestAndLoadRewardedInterstitialAd()
    //    {
    //        PrintStatus("Requesting Rewarded Interstitial ad.");

    //        // These ad units are configured to always serve test ads.
    //#if UNITY_EDITOR
    //        string adUnitId = "unused";
    //#elif UNITY_ANDROID
    //            string adUnitId = "ca-app-pub-3940256099942544/5354046379";
    //#elif UNITY_IPHONE
    //            string adUnitId = "ca-app-pub-3940256099942544/6978759866";
    //#else
    //            string adUnitId = "unexpected_platform";
    //#endif

    //        // Create an interstitial.
    //        RewardedInterstitialAd.LoadAd(adUnitId, CreateAdRequest(), (rewardedInterstitialAd, error) =>
    //        {
    //            if (error != null)
    //            {
    //                PrintStatus("Rewarded Interstitial ad load failed with error: " + error);
    //                return;
    //            }

    //            this.rewardedInterstitialAd = rewardedInterstitialAd;
    //            PrintStatus("Rewarded Interstitial ad loaded.");

    //            // Register for ad events.
    //            this.rewardedInterstitialAd.OnAdDidPresentFullScreenContent += (sender, args) =>
    //            {
    //                PrintStatus("Rewarded Interstitial ad presented.");
    //            };
    //            this.rewardedInterstitialAd.OnAdDidDismissFullScreenContent += (sender, args) =>
    //            {
    //                PrintStatus("Rewarded Interstitial ad dismissed.");
    //                this.rewardedInterstitialAd = null;
    //            };
    //            this.rewardedInterstitialAd.OnAdFailedToPresentFullScreenContent += (sender, args) =>
    //            {
    //                PrintStatus("Rewarded Interstitial ad failed to present with error: " +
    //                                                                        args.AdError.GetMessage());
    //                this.rewardedInterstitialAd = null;
    //            };
    //            this.rewardedInterstitialAd.OnPaidEvent += (sender, args) =>
    //            {
    //                string msg = string.Format("{0} (currency: {1}, value: {2}",
    //                                            "Rewarded Interstitial ad received a paid event.",
    //                                            args.AdValue.CurrencyCode,
    //                                            args.AdValue.Value);
    //                PrintStatus(msg);
    //            };
    //            this.rewardedInterstitialAd.OnAdDidRecordImpression += (sender, args) =>
    //            {
    //                PrintStatus("Rewarded Interstitial ad recorded an impression.");
    //            };
    //        });
    //    }

    //    public void ShowRewardedInterstitialAd()
    //    {
    //        if (rewardedInterstitialAd != null)
    //        {
    //            rewardedInterstitialAd.Show((reward) =>
    //            {
    //                PrintStatus("Rewarded Interstitial ad Rewarded : " + reward.Amount);
    //            });
    //        }
    //        else
    //        {
    //            PrintStatus("Rewarded Interstitial ad is not ready yet.");
    //        }
    //    }

    #endregion

    #region APPOPEN ADS

    public bool IsAppOpenAdAvailable
    {
        get
        {
            return (appOpenAd != null
                    && appOpenAd.CanShowAd()
                    && DateTime.Now < appOpenExpireTime);
        }
    }

    public void OnAppStateChanged(AppState state)
    {
        // Display the app open ad when the app is foregrounded.
        UnityEngine.Debug.Log("App State is " + state);

        // OnAppStateChanged is not guaranteed to execute on the Unity UI thread.
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (state == AppState.Foreground)
            {
                ShowAppOpenAd();
            }
        });
    }

    public void RequestAndLoadAppOpenAd()
    {
        PrintStatus("Requesting App Open ad.");


        // destroy old instance.
        if (appOpenAd != null)
        {
            DestroyAppOpenAd();
        }

        // Create a new app open ad instance.
        AppOpenAd.Load(currentIds.AppOpenId, CreateAdRequest(),
            (AppOpenAd ad, LoadAdError loadError) =>
            {
                if (loadError != null)
                {
                    PrintStatus("App open ad failed to load with error: " +
                        loadError.GetMessage());
                    return;
                }
                else if (ad == null)
                {
                    PrintStatus("App open ad failed to load.");
                    return;
                }

                PrintStatus("App Open ad loaded. Please background the app and return.");
                this.appOpenAd = ad;
                this.appOpenExpireTime = DateTime.Now + APPOPEN_TIMEOUT;

                ad.OnAdFullScreenContentOpened += () =>
                {
                    PrintStatus("App open ad opened.");
                    OnAdOpeningEvent.Invoke();
                };
                ad.OnAdFullScreenContentClosed += () =>
                {
                    PrintStatus("App open ad closed.");
                    OnAdClosedEvent.Invoke();
                    RequestAndLoadAppOpenAd();
                };
                ad.OnAdImpressionRecorded += () =>
                {
                    PrintStatus("App open ad recorded an impression.");
                };
                ad.OnAdClicked += () =>
                {
                    PrintStatus("App open ad recorded a click.");
                };
                ad.OnAdFullScreenContentFailed += (AdError error) =>
                {
                    PrintStatus("App open ad failed to show with error: " +
                        error.GetMessage());
                };
                ad.OnAdPaid += (AdValue adValue) =>
                {
                    string msg = string.Format("{0} (currency: {1}, value: {2}",
                                               "App open ad received a paid event.",
                                               adValue.CurrencyCode,
                                               adValue.Value);
                    PrintStatus(msg);
                };
            });
    }

    public void DestroyAppOpenAd()
    {
        if (this.appOpenAd != null)
        {
            this.appOpenAd.Destroy();
            this.appOpenAd = null;
        }
    }

    public void ShowAppOpenAd()
    {
        if (!IsAppOpenAdAvailable)
        {
            return;
        }
        appOpenAd.Show();
        
        
    }

    #endregion


    #region AD INSPECTOR

    public void OpenAdInspector()
    {
        //PrintStatus("Open ad Inspector.");

        MobileAds.OpenAdInspector((error) =>
        {
            if (error != null)
            {
                PrintStatus("ad Inspector failed to open with error: " + error);
            }
            else
            {
                PrintStatus("Ad Inspector opened successfully.");
            }
        });
    }

    #endregion

    #region Utility

    ///<summary>
    /// Log the message and update the status text on the main thread.
    ///<summary>

    [SerializeField] bool IsPrint;
    private void PrintStatus(string message)
    {
        if (!IsPrint) return;

        Debug.Log(message);
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            //statusText.text = message;
        });
    }

    #endregion
}
