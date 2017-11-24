using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float speed;
	public float damage;
	[HideInInspector]public Vector2 direction;
	public Gun gunFiredMe;
	
	void Update () {
		transform.Translate(direction * speed * Time.deltaTime);
		if(!GetComponent<MeshRenderer>().isVisible){
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D (Collider2D c){
		if(c.GetComponent<Avatar>() && c.GetComponentInChildren<Gun>() != gunFiredMe && c.GetComponent<Avatar>().myAvatarState != Glossary.AvatarStates.Dashing && c.GetComponent<Avatar>().myAvatarState != Glossary.AvatarStates.Assaulting){
			c.GetComponent<Avatar> ().ReduceLifeBy (damage);
			Destroy(gameObject);
			return;
		}
		if(c.GetComponentInChildren<Gun>() == gunFiredMe){
			return;
		}
		Destroy(gameObject);
	}
}
