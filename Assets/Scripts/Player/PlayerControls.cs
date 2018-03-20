using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    int clickCounter = 0;
    float comboCounter = 1.0f;

    private float timeBetweenClicks;
    bool buttonPressed = false;

    public float tapSpeed = 1.0f;
    private Animator anim;

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            timeBetweenClicks = Time.time;
            StartCoroutine(comboTimer());
            OneTwoCombo();
            ++clickCounter;
            StopCoroutine(comboTimer());

        }




        
    }

    void OneTwoCombo()
    {
        if(clickCounter == 0)
        {
            Debug.Log(clickCounter);
            anim.SetBool("OneTwoCombo1", true);
        }

        if (clickCounter == 1)
        {
            Debug.Log(clickCounter);
            anim.SetBool("OneTwoCombo2", true);
            StopCoroutine(comboTimer());

        }

    }

    IEnumerator comboTimer()
    {
        yield return new WaitForSeconds(0.5f);
        clickCounter = 0;
        anim.SetBool("OneTwoCombo1", false);
        anim.SetBool("OneTwoCombo2", false);


    }
}
