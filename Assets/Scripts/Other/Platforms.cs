using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Platforms : MonoBehaviour 
{
	public bool done = false;
	private Animator anim;
	private BoxCollider2D box;

	public void SetTrue()
	 {
		done = true;
		box.isTrigger = true;
		 
	}
	public void SetFalse() { done = false; }

	void Awake()
	{
		anim = GetComponent<Animator>();
		box = GetComponent<BoxCollider2D>();
	}

	void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject)
        {
			if(!done)
			{
				Debug.Log("!");
            	anim.SetTrigger("IsDead");
			}
        }
    }

	void Start()
	{
		Observable.EveryUpdate()
        .Where(_ => done)
        .Subscribe(_ =>
        {
            GoIdle();
        });

	}


	void GoIdle()
	{
		anim.SetTrigger("IsIdle");
	}

	private void Update() {
		Debug.Log(done);
	}
	
	
}
