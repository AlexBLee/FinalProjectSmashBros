using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Platforms : MonoBehaviour 
{
	// For animations going up and down.
	private BoolReactiveProperty done = new BoolReactiveProperty(false);
	private Animator anim;

	// The object that will be on the platform.
	private GameObject tempObject;

	// CALLED IN ANIMATIONS
	public void SetTrue() { done.Value = true; }
	public void SetFalse() { done.Value = false; }

	private bool ready = false;

	void Awake()
	{
		anim = GetComponent<Animator>();
	}

	// When the platform has finished going down, go up and idle.
	void Start()
	{
		done
        .Where(d => d)
        .Subscribe(_ =>
        {
            GoIdle();

        }).AddTo(this);

	}


	// The platform has two checks: a done bool and a ready bool.
	// Where: the "done" is changed after the animation is done, and the ready is used to check when the platform can go up.
	void OnCollisionEnter2D(Collision2D collision)
    {
		tempObject = collision.gameObject;

        if (tempObject.GetComponent<PlayerManager>().dead && !done.Value && ready == false)
        {
			anim.SetTrigger("IsDead");
            ready = true;

        }
    }

	
	// Set the platform back to it's idle position.
	void GoIdle()
	{
		tempObject.GetComponent<PlayerManager>().SetDeath(false);
		ready = false;

		anim.SetTrigger("IsIdle");
		anim.SetTrigger("StandStill");

	}



}
