using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu_ChracterChoose : MonoBehaviour
{
	[Tooltip("The unique character ID")]
	public int characterID;
	public int price;
	public PlayerController character;
	public GameObject notEnoughMoney;
	public Animator anim;
	public bool unlockDefault = false;

	//	public GameObject Locked;
	public GameObject UnlockButton;

	public Text pricetxt;
	public Text state;

	bool isUnlock;
	SoundManager soundManager;

	void Start()
	{
	
		//anim.SetBool("notenough",false);
		soundManager = FindObjectOfType<SoundManager>();

		if (unlockDefault)
			isUnlock = true;
		else
			isUnlock = GlobalValue.IsCharUnlocked(characterID) ? true : false;

		UnlockButton.SetActive(!isUnlock);

		pricetxt.text = price.ToString();
	}

	void Update()
	{
		if (!isUnlock)
			return;

		if (PlayerPrefs.GetInt(GlobalValue.ChoosenCharacterID, 1) == characterID)
		{
			state.color = Color.white;
			state.text = "SELECTED";
			
		}
		else
		{
			state.color = Color.white;
			state.text = "SELECT";
			
		}
	}

	public void Unlock()
	{
		if (GlobalValue.SavedCoins >= price)
		{
			GlobalValue.SavedCoins -= price;
			DoUnlock();
		}

		if (GlobalValue.SavedCoins < price)
		{
			//notEnoughMoney.SetActive(true);
			//anim.GetComponent<Animator>().enabled=true;
			anim.SetBool("notenough",true);
		}
	}

	void DoUnlock()
    {
		//PlayerPrefs.SetInt(GlobalValue.Character + characterID, 1);
		GlobalValue.UnlockChar(characterID);
		isUnlock = true;
		//Locked.SetActive (false);
		UnlockButton.SetActive(false);
		SoundManager.PlaySfx(SoundManager.Instance.soundPurchased);
	}

	public void Pick()
	{
		SoundManager.Click();
		if (!isUnlock)
		{
			Unlock();
			//Invoke("Deactivate",1.75f);
			return;
		}

		PlayerPrefs.SetInt(GlobalValue.ChoosenCharacterID, characterID);
		//PlayerPrefs.SetInt(GlobalValue.ChoosenCharacterInstanceID, CharacterPrefab.GetInstanceID());
		GlobalValue.ChooseCharacterID = character.playerID;
		//CharacterHolder.Instance.CharacterPicked = CharacterPrefab;
	}

	public void Deactivate()
	{
		
		anim.SetBool("notenough",false);
	}
}
