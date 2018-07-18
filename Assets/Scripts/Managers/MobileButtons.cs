using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileButtons : MonoBehaviour 
{
	public Button[] buttons;

	public bool A;
    public bool B;
    public bool C;
    public bool D;

	// Use this for initialization
	void Start () 
	{
		#if UNITY_STANDALONE || UNITY_EDITOR
			gameObject.SetActive(false);
		#endif

		#if UNITY_ANDROID
			gameObject.SetActive(true);
		#endif

		Button btn1 = buttons[0].GetComponent<Button>();
		Button btn2 = buttons[1].GetComponent<Button>();
		Button btn3 = buttons[2].GetComponent<Button>();
		Button btn4 = buttons[3].GetComponent<Button>();

		btn1.onClick.AddListener(MobileButtonA);
		btn2.onClick.AddListener(MobileButtonB);
		btn3.onClick.AddListener(MobileButtonC);
		btn4.onClick.AddListener(MobileButtonD);

	}

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
