using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase.Analytics;
//using GameAnalyticsSDK;
public class MainMenu_Level : MonoBehaviour
{
	public	int levelNumber = 1;
	public GameObject starGroup;
	public GameObject star1;
	public GameObject star2;
	public GameObject star3;

	public Text TextLevel;
	//public GameObject Locked;

	public GameObject bg, backgroundNormal, backgroundInActive;
	public static MainMenu_Level instance;

	void Start()
	{
		instance = this;
		levelNumber = int.Parse(gameObject.name);
		backgroundNormal.SetActive(true);
		backgroundInActive.SetActive(false);

		var levelReached = GlobalValue.LevelHighest;
		
		if ((levelNumber <= levelReached))
		{
			TextLevel.text = levelNumber.ToString();
			//Locked.SetActive(false);
			bg.SetActive(true);
			var openLevel = levelReached + 1 >= levelNumber /*int.Parse(gameObject.name)*/;

			star1.SetActive(openLevel && GlobalValue.IsScrollLevelAte(1, levelNumber));
			star2.SetActive(openLevel && GlobalValue.IsScrollLevelAte(2, levelNumber));
			star3.SetActive(openLevel && GlobalValue.IsScrollLevelAte(3, levelNumber));

			//Locked.SetActive(!openLevel);
			starGroup.SetActive(openLevel);

			bool isInActive = levelNumber == levelReached;
			
			backgroundNormal.SetActive(!isInActive);
			backgroundInActive.SetActive(isInActive);

			GetComponent<Button>().interactable = openLevel;
		}
		else
		{
			TextLevel.gameObject.SetActive(false);
			starGroup.SetActive(false);
			//Locked.SetActive(true);
			GetComponent<Button>().interactable = false;
		}
	}

	public void LoadScene()
	{
		if (Application.internetReachability != NetworkReachability.NotReachable)
		{
			//GoogleAdMobController.Instance.ShowInterstitialAd();
			//GoogleAdMobController.Instance.ShowBanner();
			GlobalValue.levelPlaying = levelNumber;
			HomeMenu.Instance.LoadLevel();
			print("This is level " + levelNumber);
			//GameAnalytics.NewDesignEvent ("LevelStarted_" + levelNumber);

			// new level started event
			FirebaseAnalytics.LogEvent("LevelStarted_" + levelNumber);
        }
		else
		{
			GlobalValue.levelPlaying = levelNumber;
			HomeMenu.Instance.LoadLevel();
			
		}
		
	}
}

