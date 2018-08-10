using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileButtons : MonoBehaviour 
{
	// Buttons
	public Button[] buttons;

	// Checking inputs for buttons
	public bool A;
    public bool B;
    public bool C;
    public bool D;

	// -----------------------------------------------------------------------------------------------------------------------//

	// Use this for initialization
	void Start () 
	{
		// If you're on PC/Mac/Linux, set buttons to inactive
		#if UNITY_STANDALONE || UNITY_EDITOR
			foreach(Button btn in buttons)
			{
				btn.gameObject.SetActive(false);
				gameObject.SetActive(false);
			}
		#endif

		// If you're on mobile, set buttons to active.
		#if UNITY_ANDROID
			gameObject.SetActive(true);
		#endif

		// Get button properties
		Button btn1 = buttons[0].GetComponent<Button>();
		Button btn2 = buttons[1].GetComponent<Button>();
		Button btn3 = buttons[2].GetComponent<Button>();
		Button btn4 = buttons[3].GetComponent<Button>();

		// Add functions to buttons
		btn1.onClick.AddListener(MobileButtonA);
		btn2.onClick.AddListener(MobileButtonB);
		btn3.onClick.AddListener(MobileButtonC);
		btn4.onClick.AddListener(MobileButtonD);

	}

	// Check input for buttons when they're hit.
	public void MobileButtonA()
	{
		A = true;
	}

	public void MobileButtonB()
	{
		B = true;
	}

	public void MobileButtonC()
	{
		C = true;
	}

	public void MobileButtonD()
	{
		D = true;
	}
		


	

}
