﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class CharacterSelect : MonoBehaviour 
{
	private List<GameObject> managerObjects;
	public Cursor cursor;
	private SpriteRenderer rend;
	public List<GameObject> characterList;
	public Sprite[] spriteList;
	public int listNumber = 1;

	private static bool p1Chosen;
	private static bool p2Chosen;

	private void Start() 
	{
		managerObjects = GameManager.instance.players;
		
		rend = GetComponent<SpriteRenderer>();

		Observable.EveryUpdate()
        .Where(_ => cursor.overlap.Length > 1 && cursor.overlap[1] != null)
        .Subscribe(_ =>
        {
			for(int i = 0; i < spriteList.Length; i++)
			{
				if(cursor.overlap[1].tag == spriteList[i].name)
				{
					listNumber = i;

					if(i == 0)
					{
						GameManager.instance.p1Number = i;
					}

					if(i == 1)
					{
						GameManager.instance.p2Number = i;
					}
				}
			}

			if(cursor.name == "P1Cursor")
			{
				managerObjects[0] = characterList[listNumber - 1];
				p1Chosen = true;
			}
			if(cursor.name == "P2Cursor")
			{
				managerObjects[1] = characterList[listNumber - 1];
				p2Chosen = true;
				
			}
			rend.sprite = spriteList[listNumber];
        }).AddTo(this);

		Observable.EveryUpdate()
        .Where(_ => cursor.overlap.Length == 1)
        .Subscribe(_ =>
        {
			if(managerObjects[0] != null && p1Chosen == true)
			{
				managerObjects[0] = null;
				p1Chosen = false;
				GameManager.instance.ready = false;				
				
			}

			if(managerObjects[1] != null && p2Chosen == true)
			{
				managerObjects[1] = null;
				p2Chosen = false;
				GameManager.instance.ready = false;	
			}
			

			

			listNumber = 0;
			rend.sprite = spriteList[listNumber];

        }).AddTo(this);

		Observable.EveryUpdate()
		.Where(_ => p1Chosen && p2Chosen)
		.Subscribe(_ => GameManager.instance.ready = true);
		
		
	}

	
}