﻿using UnityEngine;
using System.Collections;

public class CharacterHolder : MonoBehaviour {
	public static CharacterHolder Instance;
	//[HideInInspector]
	//public GameObject CharacterPicked;
	public PlayerController[] Characters;
	void Awake () {
        DontDestroyOnLoad(gameObject);
        if (CharacterHolder.Instance == null) {
			
            Instance = this;
            return;
		}
		else
		{
			Destroy(gameObject);
		}
		
		
		
	}

	public GameObject GetPickedCharacter()
	{
		GameObject CharacterPicked = null;
		var characterIDChoosen = GlobalValue.ChooseCharacterID;
		Debug.Log(characterIDChoosen);

		if (characterIDChoosen == 0)		//no select any character yet
		{
			CharacterPicked = Characters[0].gameObject;
		}
		else
		{
			foreach (var character in Characters)
			{
				var ID = character.playerID;
				Debug.Log("ID" + ID);
				if (ID == characterIDChoosen)
				{
					CharacterPicked = character.gameObject;
					break;
				}
			}
		}

		return CharacterPicked;
	}
}
