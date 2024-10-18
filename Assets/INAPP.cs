using System;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;


public class INAPP : MonoBehaviour, IStoreListener
{
    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;

    //----------------------------------------------------- Product ID Create----------------------------------------------------//

   // [Tooltip("Menu Script Reference")][SerializeField] Menu menu;
    
    [Header("Product ID's")]
    public string Product_MeetBasic = "";
    public string Product_MeetBasicPlus = "";
    public string Product_MeetBasicHighest = "";
    public string Product_MeetBumper = "";
    public string Product_UnlockAllCrocodiles = "";
    public string Product_UnlockAllBeachLevels = "";
    public string Product_UnlockAllSnowLevels = "";
    public string Product_UnlockAllForestLevels = "";
    public string Product_Removead = "";
    public string environment = "production";


    [Header("Text References")]
    public Text removed;
    public Text meetBasic;
    public Text meetBasicPlus;
    public Text meetBasicHighest;
    public Text meetBumper;
    public Text UnlockAllBeachtxt;
    public Text UnlockAllSnowtxt;
    public Text UnlockAllForesttxt;
    public Text UnlockAll;

    // static instance
    public static INAPP instance;

    //---------------------------------------------------------------------------------------------------------------------------//
    private void Awake()
    {
        //if (instance == null)
        //{
        //    instance = this;
        //    DontDestroyOnLoad(this.gameObject);
        //}

        //else
        //{
        //    Destroy(this.gameObject);
        //}
    }

    async void Start()
    {
        try
        {
            var options = new InitializationOptions().SetEnvironmentName(environment);

            await UnityServices.InitializeAsync(options);
            InitializePurchasing();
        }
        catch (Exception exception)
        {
            // An error occurred during initialization.
        }
    }

    //private void Start()
    //{
    //    InitializePurchasing();
    //}

    public void InitializePurchasing()
    {
        //// If we have already connected to Purchasing ...
        //if (IsInitialized())
        //{
        //    // ... we are done here.
        //    //return;
        //}

        // Create a builder, first passing in a suite of Unity provided stores.
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        // Add a product to sell / restore by way of its identifier, associating the general identifier
        // with its store-specific identifiers.

        builder.AddProduct(Product_Removead, ProductType.NonConsumable);
        builder.AddProduct(Product_UnlockAllCrocodiles, ProductType.NonConsumable);
        builder.AddProduct(Product_UnlockAllBeachLevels, ProductType.NonConsumable);
        builder.AddProduct(Product_UnlockAllSnowLevels, ProductType.NonConsumable);
        builder.AddProduct(Product_UnlockAllForestLevels, ProductType.NonConsumable);
        builder.AddProduct(Product_MeetBasic, ProductType.Consumable);
        builder.AddProduct(Product_MeetBasicPlus, ProductType.Consumable);
        builder.AddProduct(Product_MeetBasicHighest, ProductType.Consumable);
        builder.AddProduct(Product_MeetBumper, ProductType.Consumable);





        // And finish adding the subscription product. Notice this uses store-specific IDs, illustrating
        // if the Product ID was configured differently between Apple and Google stores. Also note that
        // one uses the general kProductIDSubscription handle inside the game - the store-specific IDs 
        // must only be referenced here. 
        //			builder.AddProduct(kProductIDSubscription, ProductType.Subscription, new IDs(){
        //				{ kProductNameAppleSubscription, AppleAppStore.Name },
        //				{ kProductNameGooglePlaySubscription, GooglePlay.Name },
        //			});

        UnityPurchasing.Initialize(this, builder);
    }


    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    //--------------------------------------------------------------------------- Function Call of Purchase Products-------------------------------------//
    public void MeetBuyRemovead()
    {

        BuyProductID(Product_Removead);
       // menu.SetIAPLoading(true);
        //IAPLoading.SetActive(true);
        Invoke("TurnOff", 4f);

    }

    public void BuyMeetBasic()
    {

        BuyProductID(Product_MeetBasic);
      //  menu.SetIAPLoading(true);
        // IAPLoading.SetActive(true);
        Invoke("TurnOff", 4f);

    }

    public void BuyMeetBasicPlus()
    {

        BuyProductID(Product_MeetBasicPlus);
      //  menu.SetIAPLoading(true);
        //IAPLoading.SetActive(true);
        Invoke("TurnOff", 4f);

    }

    public void BuyMeetHighest()
    {

        BuyProductID(Product_MeetBasicHighest);
      //  menu.SetIAPLoading(true);
        //IAPLoading.SetActive(true);
        Invoke("TurnOff", 4f);

    }
    public void BuyMeetBumper()
    {

        BuyProductID(Product_MeetBumper);
      //  menu.SetIAPLoading(true);
        //IAPLoading.SetActive(true);
        Invoke("TurnOff", 4f);
        MeetBuyRemovead();

    }
    public void BuyUnlockAllCrocodile()
    {

        BuyProductID(Product_UnlockAllCrocodiles);
      //  menu.SetIAPLoading(true);
        //IAPLoading.SetActive(true);
        Invoke("TurnOff", 4f);
    }
    public void BuyUnlockAllBeachLevels()
    {

        BuyProductID(Product_UnlockAllBeachLevels);
     //   menu.SetIAPLoading(true); 
        //IAPLoading.SetActive(true);
        Invoke("TurnOff", 4f);
    }
    public void BuyUnlockAllSnowLevels()
    {

        BuyProductID(Product_UnlockAllSnowLevels);
       // menu.SetIAPLoading(true);
        //IAPLoading.SetActive(true);
        Invoke("TurnOff", 4f);
    }
    public void BuyUnlockAllForestLevels()
    {

        BuyProductID(Product_UnlockAllForestLevels);
      //  menu.SetIAPLoading(true);
        // IAPLoading.SetActive(true);
        Invoke("TurnOff", 4f);
    }


    //--------------------------------------------------------------------------------------------------------------------------------------------//

    void TurnOff()
    {
      //  menu.SetIAPLoading(false); 
        //IAPLoading.SetActive(false);
    }

    private void Update()
    {

    }

    public void BuyProductID(string productId)
    {
        // If Purchasing has been initialized ...
        if (IsInitialized())
        {

            Product product = m_StoreController.products.WithID(productId);

            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));

                m_StoreController.InitiatePurchase(product);
            }

            else
            {
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }

        else
        {

            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }


    public void RestorePurchases()
    {

        if (!IsInitialized())
        {
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        // If we are running on an Apple device ... 
        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            // ... begin restoring purchases
            Debug.Log("RestorePurchases started ...");

            // Fetch the Apple store-specific subsystem.
            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();

            apple.RestoreTransactions((result) =>
            {

                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        // Otherwise ...
        else
        {
            // We are not running on an Apple device. No work is necessary to restore purchases.
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }


    //  
    // --- IStoreListener
    //

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInitialized: PASS");
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
        string reomveadv = m_StoreController.products.WithStoreSpecificID(Product_Removead).metadata.localizedPriceString;
        removed.text = reomveadv;
        string unlockall = m_StoreController.products.WithStoreSpecificID(Product_UnlockAllCrocodiles).metadata.localizedPriceString;
        UnlockAll.text = unlockall;

        string unlockbeach, unlocksnow, unlockforest;

        unlockbeach = m_StoreController.products.WithStoreSpecificID(Product_UnlockAllBeachLevels).metadata.localizedPriceString;
        UnlockAllBeachtxt.text = unlockbeach;
        unlocksnow = m_StoreController.products.WithStoreSpecificID(Product_UnlockAllSnowLevels).metadata.localizedPriceString;
        UnlockAllSnowtxt.text = unlocksnow;
        unlockforest = m_StoreController.products.WithStoreSpecificID(Product_UnlockAllForestLevels).metadata.localizedPriceString;
        UnlockAllForesttxt.text = unlockforest;


        string gb = m_StoreController.products.WithStoreSpecificID(Product_MeetBasic).metadata.localizedPriceString;
        meetBasic.text = gb;

        string gbp = m_StoreController.products.WithStoreSpecificID(Product_MeetBasicPlus).metadata.localizedPriceString;
        meetBasicPlus.text = gbp;

        string gs = m_StoreController.products.WithStoreSpecificID(Product_MeetBasicHighest).metadata.localizedPriceString;
        meetBasicHighest.text = gs;
        string gbu = m_StoreController.products.WithStoreSpecificID(Product_MeetBumper).metadata.localizedPriceString;
        meetBumper.text = gbu;

    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }


    //---------------------------------------------------------------------- After Purchase--------------------------------------------------------/

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (String.Equals(args.purchasedProduct.definition.id, Product_MeetBasic, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            
            // in package one in meat shop reward player with 2000 Meat
         //   menu.RewardMeat(2000);

            // GA Event
          //  GameAnalytics.NewDesignEvent("InApps_MeatPack1");
        }
        else if (String.Equals(args.purchasedProduct.definition.id, Product_MeetBasicPlus, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            
            // in package one in meat shop reward player with 5000 Meat
          //  menu.RewardMeat(5000);

            // GA Event
         //   GameAnalytics.NewDesignEvent("InApps_MeatPack2");
        }
        else if (String.Equals(args.purchasedProduct.definition.id, Product_MeetBasicHighest, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            
            // in package one in meat shop reward player with 8000 Meat
          //  menu.RewardMeat(8000);

            // GA Event
          //  GameAnalytics.NewDesignEvent("InApps_MeatPack3");
        }
        else if (String.Equals(args.purchasedProduct.definition.id, Product_MeetBumper, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            
            // Bumper offer which unlocks everything
         //   menu.BumperOffer();

            // GA Event
          //  GameAnalytics.NewDesignEvent("InApps_BumperOffer");
        }

        else if (String.Equals(args.purchasedProduct.definition.id, Product_Removead, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

            // remove ads
          //  menu.RemoveAds();

            // GA Event
          ///  GameAnalytics.NewDesignEvent("InApps_NoAdsPurchase");
        }
        else if (String.Equals(args.purchasedProduct.definition.id, Product_UnlockAllCrocodiles, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

            // give player all skins unlocked
          //  menu.UnlockAllCrcodiles();

            // GA Event
          //  GameAnalytics.NewDesignEvent("InApps_UnlockAllCrocodiles");
        }
        else if (String.Equals(args.purchasedProduct.definition.id, Product_UnlockAllBeachLevels, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

            // give Beach levels
          //  menu.RewardLevels("Beach");

            // GA Event
        //    GameAnalytics.NewDesignEvent("InApps_UnlockAllBeach");
        }
        else if (String.Equals(args.purchasedProduct.definition.id, Product_UnlockAllSnowLevels, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            
            // give Beach levels
        //    menu.RewardLevels("Snow");

            // GA Event
        //    GameAnalytics.NewDesignEvent("InApps_UnlockAllSnow");
        }
        else if (String.Equals(args.purchasedProduct.definition.id, Product_UnlockAllForestLevels, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            
            // give Beach levels
         //   menu.RewardLevels("Jungle");

            // GA Event
           // GameAnalytics.NewDesignEvent("InApps_UnlockAllJungle");
        }

        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        }

        return PurchaseProcessingResult.Complete;
    }

    //-------------------------------------------------------------------------------------------------------------------------------------------------/


    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {

        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new NotImplementedException();
    }
}
