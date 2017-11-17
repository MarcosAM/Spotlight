using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float speed;
	public float damage;
	[HideInInspector]public Vector2 direction;
	public Weapon weaponFiredMe;
	
	void Update () {
		transform.Translate(direction * speed * Time.deltaTime);
		if(!GetComponent<MeshRenderer>().isVisible){
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D (Collider2D c){
		if(c.GetComponent<Life>() && c.GetComponent<Weapon>() != weaponFiredMe && !c.GetComponent<Movement>().isDashing){
			c.GetComponent<Life>().ReduceLifeBy(damage,weaponFiredMe.gameObject);
			Destroy(gameObject);
			return;
		}
		if(c.GetComponent<Weapon>() == weaponFiredMe){
			return;
		}
		Destroy(gameObject);
	}
}
