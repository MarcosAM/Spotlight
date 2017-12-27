using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float speed;
	public float normalDamage;
	public float chargedDamage;
	public float zoneDamage;

	[HideInInspector]public bool isCharged;
	public float timeToWear;
	public bool isPiercing = false;
	public float size = 2;
	public Vector3 minimumSize;

	[HideInInspector]public float currentDamage;
	[HideInInspector]public Vector2 direction;
	[HideInInspector]public SpriteRenderer spriteRenderer;
	[HideInInspector]public Gun gunFiredMe;

	void Awake (){
		currentDamage = normalDamage;
	}

	void Start(){
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	void Update () {
		transform.Translate(direction * speed * Time.deltaTime);
		spriteRenderer.color = gunFiredMe.avatar.spriteRenderer.color;
		if(!spriteRenderer.isVisible){
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D (Collider2D c)
	{
		if (c.GetComponentInChildren<Gun> () == gunFiredMe || c.GetComponent<Zone>() || c.GetComponent<Catchable>()) {
			return;
		}

		if (c.GetComponent<Avatar> () && c.GetComponent<Avatar> ().state != Glossary.AvatarStates.Dashing && c.GetComponent<Avatar> ().state != Glossary.AvatarStates.Assaulting) {
			if (c.GetComponent<Avatar> ().state == Glossary.AvatarStates.Charging || c.GetComponent<Avatar>().myGun.hasOverheated) {
				c.GetComponent<Avatar> ().ReduceLifeBy (currentDamage * 2, gunFiredMe.avatar);
			} else {
				c.GetComponent<Avatar> ().ReduceLifeBy (currentDamage,gunFiredMe.avatar);
			}
		}

		if(isPiercing){
			return;
		}
		Destroy(gameObject);
	}

	public void changeSize (float newSize)
	{
		size = newSize;
		transform.localScale = minimumSize*size;
	}
}
