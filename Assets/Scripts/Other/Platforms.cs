using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Platforms : MonoBehaviour 
{
	public bool done = false;
	private Animator anim;

	public void SetTrue() { done = true; }
	public void SetFalse() { done = false; }

	void Awake()
	{
		anim = GetComponent<Animator>();
	}

	void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject)
        {
            anim.SetTrigger("IsDead");
        }
    }

	void Start()
	{
		Observable.EveryUpdate()
        .Where(_ => done)
        .Subscribe(_ =>
        {
            anim.SetTrigger("IsIdle");
            SetFalse();
        });

	}



	// void Update()
	// {
	// 	if(done)
	// 	{
	// 		anim.SetTrigger("IsIdle");
	// 		SetFalse();
	// 	}
	// }

	
	
}
