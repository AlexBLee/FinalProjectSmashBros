using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ImageDisplay : MonoBehaviour 
{
	public List<GameObject> managerObjects;	
	public Cursor cursor;
	private GameObject character;
	private SpriteRenderer rend;
	public Sprite[] spriteList;
	private int listNumber = 0;

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
				if(cursor.overlap[1].name == spriteList[i].name)
				{
					Debug.Log(cursor.overlap[1].name);
					listNumber = i;
					Debug.Log(listNumber);
				}
			}
			
			if(cursor.overlap[1].gameObject != null && !managerObjects.Contains(cursor.overlap[1].gameObject))
			{
				managerObjects.Add(cursor.overlap[1].gameObject);
				character = cursor.overlap[1].gameObject;
			}
			rend.sprite = spriteList[listNumber];
        }).AddTo(this);

		Observable.EveryUpdate()
        .Where(_ => cursor.overlap.Length == 1)
        .Subscribe(_ =>
        {
			if(managerObjects.Contains(character))
			{
				managerObjects.Remove(character);
			}
			listNumber = 0;
			rend.sprite = spriteList[listNumber];

			

			
        }).AddTo(this);
		
	}
	
	
}
