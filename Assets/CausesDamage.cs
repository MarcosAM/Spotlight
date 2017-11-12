using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CausesDamage : MonoBehaviour {

	public float damage;

	void OnTriggerEnter2D (Collider2D c)
	{
		if (c.GetComponent<Life> ()) {
			c.GetComponent<Life>().ReduceLifeBy(damage);
		}
	}
}
