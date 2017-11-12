using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float speed;
	[HideInInspector]public Vector2 direction;
	
	void Update () {
		transform.Translate(direction * speed * Time.deltaTime);
	}

	void OnTriggerEnter2D (Collider2D c){
		Destroy(gameObject);
	}
}
