using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveActions : MonoBehaviour {

	public float walkingSpeed = 6;

	public int dashesAvailable = 1;
	public float dashDuration = 0.3f;
	public float rechargeTime;
	public float timeToRecharge;

	Avatar myAvatar;
	Rigidbody2D myRigidbody2D;
	Vector2 velocity;

	void Start(){
		myRigidbody2D = GetComponent<Rigidbody2D>();
		myAvatar = GetComponent<Avatar>();

		timeToRecharge = rechargeTime;
	}

	void Update ()
	{
		if (myAvatar.myAvatarState == Glossary.AvatarStates.Normal && dashesAvailable <= 0) {
			timeToRecharge -= Time.deltaTime;
			if (timeToRecharge <= 0) {
				dashesAvailable++;
				timeToRecharge = rechargeTime;
			}
		}
	}

	void FixedUpdate (){
		myRigidbody2D.velocity = velocity;
	}

	public void Walk (Vector2 direction){
		velocity = direction*walkingSpeed;
	}

	public void Dash (){
		velocity = velocity*2f;
		dashesAvailable --;
	}
}
