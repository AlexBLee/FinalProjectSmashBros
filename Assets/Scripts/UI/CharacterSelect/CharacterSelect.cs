using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UniRx;

public class CharacterSelect : MonoBehaviour 
{
	// Lists for characters.
	public List<GameObject> characterList;
	public Sprite[] spriteList;
	public int listNumber = 1;

	// Cursor
	public Cursor cursor;

	// For rendering the character image.
	private Image rend;
	
	// To determine if player has chosen their character.
	private static bool p1Chosen;
	private static bool p2Chosen;

    // --------------------------------------------------------------------------------------------------------- //    


	private void Start() 
	{
		
		rend = GetComponent<Image>();

		// When both players have chosen their characters, the game is ready to start.
		Observable.EveryUpdate()
		.Where(_ => p1Chosen)// && p2Chosen)
		.Subscribe(_ => GameManager.instance.ready = true);
		
		
	}


	private void Update() 
	{
		if(cursor != null)
		{

			if(cursor.overlap.Length > 1 && cursor.overlap[1] != null)
			{	
				// Loop through the list to find out where to put the character for which player.
				for(int i = 0; i < spriteList.Length; i++)
				{
					if(cursor.overlap[1].tag == spriteList[i].name)
					{
						listNumber = i;
					}
				}


				// Put the character into the game manager list accordingly with the player.
				
				// NOTE: The sprite list and the character list have the same characters in the same order, the only exception is
				// that the sprite list has an additional blank image at the beginning hence why the -1 is needed.

				// For player 1.
				if(cursor.name == "P1Cursor(Clone)")
				{
					GameManager.instance.players[0] = characterList[listNumber - 1];
					p1Chosen = true;
				}

				// For player 2.
				if(cursor.name == "P2Cursor(Clone)")
				{
					GameManager.instance.players[1] = characterList[listNumber - 1];
					p2Chosen = true;
					
				}

				// To show the character chosen in the character selection images.
				rend.sprite = spriteList[listNumber];
			}
		}
	}
	
}
