using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButtons : MonoBehaviour 
{


	public void MobileButtonA()
	{
		if(gameObject.name == "DogFighter")
		{
			GetComponent<DogControls>().AButton();
		}

		if(gameObject.name == "CatFighter")
		{
			GetComponent<CatControls>().AButton();
		}

			
	}

	public void MobileButtonB()
	{
		if(gameObject.name == "DogFighter")
		{
			GetComponent<DogControls>().BButton();
		}

		if(gameObject.name == "CatFighter")
		{
			GetComponent<CatControls>().BButton();
		}
	}

	public void MobileButtonC()
	{
		if(gameObject.name == "DogFighter")
		{
			GetComponent<DogControls>().CButton();
		}

		if(gameObject.name == "CatFighter")
		{
			GetComponent<CatControls>().CButton();
		}
	}

	public void MobileButtonD()
	{
		if(gameObject.name == "DogFighter")
		{
			GetComponent<DogControls>().DButton();
		}

		if(gameObject.name == "CatFighter")
		{
			GetComponent<CatControls>().DButton();
		}
	}
}
