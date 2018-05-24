using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Platforms : MonoBehaviour 
{
	public BoolReactiveProperty done = new BoolReactiveProperty(false);
	private Animator anim;
	private BoxCollider2D box;
	private GameObject tempObject;
	private bool time = false;

	public void SetTrue()
	 {
		done.Value = true;
	}
	public void SetFalse() { done.Value = false; }

	void Awake()
	{
		anim = GetComponent<Animator>();
		box = GetComponent<BoxCollider2D>();
	}

	void OnCollisionEnter2D(Collision2D collision)
    {
		tempObject = collision.gameObject;

        if (tempObject.GetComponent<PlayerManager>().dead && !done.Value && time == false)
        {
			anim.SetTrigger("IsDead");
            time = true;

        }
    }

	void Start()
	{
		done
        .Where(d => d)
        .Subscribe(_ =>
        {
            GoIdle();

        }).AddTo(this);

	}


	void GoIdle()
	{
		tempObject.GetComponent<PlayerManager>().SetDeath(false);
		time = false;
		anim.SetTrigger("IsIdle");
		anim.SetTrigger("StandStill");

	}

	private void Update() {



    }


}
