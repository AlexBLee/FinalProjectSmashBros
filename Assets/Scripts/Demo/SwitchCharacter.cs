using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchCharacter : MonoBehaviour 
{
	
	private Button button;

	public GameObject dog;
	public GameObject cat;

	public Transform spawnPosition;
	public Transform switchPosition;

	private DogManager dogManager;
	private CatManager catManager;

	private bool dogActive = true;
	private bool catActive = false;

	public GameObject dogButtonA;
	public GameObject dogButtonB;

	public GameObject catButtonA;
	public GameObject catButtonB;
	
	

	void Start()
	{
		dogManager = FindObjectOfType<DogManager>();
		catManager = FindObjectOfType<CatManager>();
		
		catButtonA.SetActive(false);
		catButtonB.SetActive(false);
		
		
		button = GetComponent<Button>();
		button.onClick.AddListener(SwitchChar);
	}

	public void SwitchChar()
	{	
		if(dogActive)
		{
			dogActive = false;
			catActive = true;
			dogManager.dogPosition.position = switchPosition.position;
			catManager.catPosition.position = spawnPosition.position;
			catButtonA.SetActive(true);
			catButtonB.SetActive(true);
			dogButtonA.SetActive(false);
			dogButtonB.SetActive(false);
			

		}
		else if(catActive)
		{	
			catActive = false;
			dogActive = true;
			catManager.catPosition.position = switchPosition.position;
			dogManager.dogPosition.position = spawnPosition.position;
			dogButtonA.SetActive(true);
			dogButtonB.SetActive(true);
			catButtonA.SetActive(false);
			catButtonB.SetActive(false);
			
		}
		
		
		
	}
}
