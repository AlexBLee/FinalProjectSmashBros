using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDestroy : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start () 
	{
		yield return new WaitForSeconds(0.6f);
		Destroy(gameObject);
	}
	
	
}
