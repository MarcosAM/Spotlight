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
	public Rigidbody2D myRigidbody2D;
	public Vector2 velocity;

	void Start(){
		myRigidbody2D = GetComponent<Rigidbody2D>();
		myAvatar = GetComponent<Avatar>();

		timeToRecharge = rechargeTime;
	}

	void Update ()
	{
		if ((myAvatar.myAvatarState == Glossary.AvatarStates.Normal || myAvatar.myAvatarState == Glossary.AvatarStates.Charging) && dashesAvailable <= 0) {
			timeToRecharge -= Time.deltaTime;
			if (timeToRecharge <= 0) {
				dashesAvailable++;
				timeToRecharge = rechargeTime;
			}
		}
	}

	void FixedUpdate (){
		if(myAvatar.myAvatarState != Glossary.AvatarStates.Stunned)
			myRigidbody2D.velocity = velocity;
	}

	public void Walk (Vector2 direction){
		velocity = direction.normalized*walkingSpeed;
	}

	public void Dash ()
	{
		if (dashesAvailable > 0) {
			velocity = velocity*2f;
			dashesAvailable --;
		}
	}
}
