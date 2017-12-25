using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float speed;
	public float damage;
	[HideInInspector]public Vector2 direction;
	[HideInInspector]public SpriteRenderer spriteRenderer;
	[HideInInspector]public Gun gunFiredMe;

	void Start(){
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	void Update () {
		transform.Translate(direction * speed * Time.deltaTime);
		spriteRenderer.color = gunFiredMe.myAvatar.spriteRenderer.color;
		if(!spriteRenderer.isVisible){
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D (Collider2D c){
		if(c.GetComponent<Avatar>() && c.GetComponentInChildren<Gun>() != gunFiredMe && c.GetComponent<Avatar>().myAvatarState != Glossary.AvatarStates.Dashing && c.GetComponent<Avatar>().myAvatarState != Glossary.AvatarStates.Assaulting){
			c.GetComponent<Avatar> ().ReduceLifeBy (damage,gunFiredMe.myAvatar);
			Destroy(gameObject);
			return;
		}
		if(c.GetComponentInChildren<Gun>() == gunFiredMe){
			return;
		}
		if(c.GetComponent<Obstacle>()){
			Destroy(gameObject);
		}
	}
}
