using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit 
 {
	 public float damage;
	 public Vector2 knockback;
	
	public Hit(int dmg, Vector2 kback)
	{
		damage = dmg;
		knockback = kback;
	}

	float GetDamage(float dmg)
    {
        return damage;
    }

    Vector2 GetKnockback(Vector2 knockback)
    {
        return knockback;
    }

	void SetDamage(float dmg)
	{
		damage = dmg;
	}

	void SetKnockback(Vector2 kback)
	{
		knockback = kback;
	}

}
