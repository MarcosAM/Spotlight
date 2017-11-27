using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour {

	Rigidbody2D rb2d;

	public float speed = 5;
	public Avatar following;
	public bool isFollowing = false;

	void Update(){
		if(isFollowing){
			transform.position = following.transform.position;
		}
	}

//	void Start () {
//		rb2d = GetComponent<Rigidbody2D> ();
//		Vector2 direction = new Vector2 (Random.Range (-2, 2), Random.Range (-2, 2));
//		direction.Normalize ();
//		rb2d.AddForce (direction*speed);
//	}
//
//	void OnCollisionEnter2D(Collision2D c){
//		Vector2 newVelocity = rb2d.velocity.normalized; 
//		rb2d.velocity = newVelocity * speed*2;
//	}

	public void Follow (Avatar f){
		following = f;
		isFollowing = true;
	}
}