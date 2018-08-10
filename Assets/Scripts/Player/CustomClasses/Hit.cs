using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
 {
	 // This class sets knockback and damage and is used by the characters.

	 public float damage;
	 public Vector2 knockback;
	
	// Initalizer/Constructor
	public Hit(int dmg, Vector2 kback)
	{
		damage = dmg;
		knockback = kback;
	}

	// Getters
	public float GetDamage()
    {
        return damage;
    }

    public Vector2 GetKnockback()
    {
        return knockback;
    }

	// Setters
	public void SetDamage(float dmg)
	{
		damage = dmg;
	}

	public void SetKnockback(Vector2 kback)
	{
		knockback = kback;
	}

}
