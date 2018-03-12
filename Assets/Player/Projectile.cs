using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float speed;
	public float normalDamage;
	public float chargedDamage;
	public float zoneDamage;

	[HideInInspector]public bool isCharged;
	[HideInInspector]public float timeToWear;
	[HideInInspector]public bool isPiercing = false;
	[HideInInspector]public bool isPoisonous = false;
	[HideInInspector]public float size = 2;
	public Vector3 minimumSize;

	public int bouncesLeft = 4;
	public PhysicsMaterial2D physicsMaterial2D;
	Collider2D c2D;
	public Explosion explosionPrefab;
	public bool isExplosive = false;
	[HideInInspector]public bool isBouncing;
	[HideInInspector]public float currentDamage;
	[HideInInspector]public Vector2 direction;
	[HideInInspector]public SpriteRenderer spriteRenderer;
	[HideInInspector]public Gun gunFiredMe;

	void Awake (){
		currentDamage = normalDamage;
	}

	void Start ()
	{
		spriteRenderer = GetComponent<SpriteRenderer> ();
		c2D = GetComponent<Collider2D> ();
		if (isBouncing) {
			c2D.isTrigger = false;
			c2D.sharedMaterial = physicsMaterial2D;
			c2D.enabled = false;
			c2D.enabled = true;
			Rigidbody2D rgbd2D = gameObject.AddComponent<Rigidbody2D> () as Rigidbody2D;
			rgbd2D.mass = 0.01F;
			rgbd2D.drag = 0F;
			rgbd2D.angularDrag = 0F;
			rgbd2D.gravityScale = 0F;
			rgbd2D.AddForce (direction * speed * 0.3f);
		}
		if (isPiercing && isBouncing) {
			Obstacle[] obstacles = FindObjectsOfType<Obstacle>();
			foreach (Obstacle o in obstacles){
				Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(),o.gameObject.GetComponent<Collider2D>());
			}
		}
	}
	
	void Update () {
		if(!isBouncing){
			transform.Translate(direction * speed * Time.deltaTime);
		}
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

		if(c.GetComponent<Avatar>() && c.GetComponent<Avatar>().isShielded){
			if(c2D.IsTouching(c.GetComponent<Avatar>().shield.boxCollider)){
				Destroy (gameObject);
				return;
			}
		}

		if (c.GetComponent<Avatar> () && c.GetComponent<Avatar> ().state != Glossary.AvatarStates.Dashing && c.GetComponent<Avatar> ().state != Glossary.AvatarStates.Assaulting) {
			if (c.GetComponent<Avatar> ().state == Glossary.AvatarStates.Charging || c.GetComponent<Avatar>().myGun.hasOverheated) {
				c.GetComponent<Avatar> ().ReduceLifeBy (currentDamage * 2, gunFiredMe.avatar);
			} else {
				c.GetComponent<Avatar> ().ReduceLifeBy (currentDamage,gunFiredMe.avatar);
			}
			if(isPoisonous && !c.GetComponent<Avatar>().myGun.hasOverheated){
				c.GetComponent<Avatar>().myGun.GetPoisoned(gunFiredMe.avatar);
			}
		}

		if(isPiercing){
			return;
		}
		if(isExplosive && !c.GetComponent<Shield>())
			Explode ();
		Destroy(gameObject);
	}

	void OnCollisionEnter2D (Collision2D c)
	{
//		if(c.gameObject.GetComponent<Shield>() && c.gameObject.GetComponentInParent<Avatar>().state != Glossary.AvatarStates.Dashing){
//			bouncesLeft --;
//			if(bouncesLeft <=0){
//				Destroy(gameObject);
//			}
//		}
		if (c.gameObject.GetComponent<Avatar> () && c.gameObject.GetComponent<Avatar> ().state != Glossary.AvatarStates.Dashing && c.gameObject.GetComponent<Avatar>().myGun != gunFiredMe) {
			if (c.gameObject.GetComponent<Avatar> ().myGun.hasOverheated) {
				c.gameObject.GetComponent<Avatar> ().ReduceLifeBy (currentDamage * 2, gunFiredMe.avatar);
			} else {
				c.gameObject.GetComponent<Avatar>().ReduceLifeBy(currentDamage,gunFiredMe.avatar);
			}
			if(isPoisonous){
				c.gameObject.GetComponent<Avatar>().myGun.GetPoisoned(gunFiredMe.avatar);
			}
		}
		bouncesLeft --;
		if(bouncesLeft <=0){
			if(isExplosive)
				Explode ();
			Destroy(gameObject);
		}
	}

	public void changeSize (float newSize)
	{
		size = newSize;
		transform.localScale = minimumSize*size;
	}

	public void Explode (){
		Explosion explosion = Instantiate (explosionPrefab, transform.position, Quaternion.identity);
	}
}
