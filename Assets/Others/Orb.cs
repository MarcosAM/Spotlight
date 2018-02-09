using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour {

	Rigidbody2D rb2d;

	public float speed = 5;
	[HideInInspector]public Avatar following;
	[HideInInspector]public bool isFollowing = false;

	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		Release ();
	}

	void Update(){
		if(isFollowing){
			transform.position = following.transform.position;
		}
	}

	void OnCollisionEnter2D(Collision2D c){
		Vector2 newVelocity = rb2d.velocity.normalized;
		float randomAngle = Random.Range (-10,11);
		rb2d.velocity = newVelocity * speed*2;
	}

	public void Follow (Avatar f){
		following = f;
		isFollowing = true;
		GetComponent<CircleCollider2D> ().enabled = false;
	}

	public void Release (){
		following = null;
		isFollowing = false;
		GetComponent<CircleCollider2D> ().enabled = true;

		Vector2 direction = new Vector2 (Random.Range (-2, 2), Random.Range (-2, 2));
		direction.Normalize ();
		rb2d.AddForce (direction*speed);
	}
}