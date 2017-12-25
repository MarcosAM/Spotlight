using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveActions : MonoBehaviour {

	public bool hasAim = false;
	
	public float walkingSpeed = 6;
	public float runningSpeed = 9;
	[HideInInspector]public float currentSpeed;
	[HideInInspector]public Vector2 lookDirection;

	[HideInInspector]public bool canDash = true;
	public float dashDuration = 0.3f;
	public float rechargeTime;
	[HideInInspector]public float timeToRecharge;

	Avatar avatar;
	[HideInInspector]public Rigidbody2D rigidBody2D;
	[HideInInspector]public Vector2 velocity;
	[HideInInspector]public DashParticles dashParticles;
	[HideInInspector]public AssaultParticles assaultParticles;
	[HideInInspector] BoxCollider2D boxCollider2D;

	void Start(){
		timeToRecharge = rechargeTime;

		rigidBody2D = GetComponent<Rigidbody2D>();
		avatar = GetComponent<Avatar>();
		dashParticles = GetComponentInChildren<DashParticles>();
		assaultParticles = GetComponentInChildren<AssaultParticles>();
		boxCollider2D = GetComponent<BoxCollider2D>();
	}

	void Update ()
	{
		if ((avatar.state == Glossary.AvatarStates.Normal || avatar.state == Glossary.AvatarStates.Charging) && !canDash) {
			timeToRecharge -= Time.deltaTime;
			if (timeToRecharge <= 0) {
				canDash = true;
				timeToRecharge = rechargeTime;
			}
		}
	}

	void FixedUpdate (){
		if(avatar.state != Glossary.AvatarStates.Stunned)
			rigidBody2D.velocity = velocity;
	}

	public void RunOrAim (Vector2 lStick, Vector2 rStick){
		if (avatar.state == Glossary.AvatarStates.Normal || avatar.state == Glossary.AvatarStates.Charging) {
			currentSpeed = walkingSpeed;
			lookDirection = new Vector2(rStick.x, rStick.y);
			if (hasAim && (rStick.x == 0f && rStick.y == 0f)){
				currentSpeed = runningSpeed;
				lookDirection = new Vector2(lStick.x, -lStick.y);
			} 
			LookAt(lookDirection.x,lookDirection.y);
			velocity = lStick.normalized*currentSpeed;
			dashParticles.LookAt(lStick);
			assaultParticles.LookAt(lStick);
		}
	}

	public void DashBtnUp (){
		StopDash();
		StopAssaulting ();
	}

	public IEnumerator DashBtnDown(){
		if (avatar.state == Glossary.AvatarStates.Normal && canDash) {
			Dash();
			avatar.state = Glossary.AvatarStates.Dashing;
			boxCollider2D.isTrigger = true;
			dashParticles.StartDash();
		}
		if (avatar.state == Glossary.AvatarStates.Charging){
			avatar.state = Glossary.AvatarStates.Assaulting;
			avatar.myGun.chargeParticles.ChargeDown ();
			Dash();
			assaultParticles.StartAssault();
		}
		yield return new WaitForSecondsRealtime (dashDuration);
		DashBtnUp();
	}

	public void StopDash (){
		if(avatar.state == Glossary.AvatarStates.Dashing){
			avatar.state = Glossary.AvatarStates.Normal;
			boxCollider2D.isTrigger = false;
			dashParticles.StopDash();
			StopCoroutine("DashBtnDown");
		}
	}

	void StopAssaulting(){
		if(avatar.state == Glossary.AvatarStates.Assaulting){
			assaultParticles.StopAssault();
			avatar.StopCharging ();
			StopCoroutine("DashBtnDown");
		}
	}

	public void Dash ()
	{
		if (canDash) {
			velocity = velocity*3f;
			canDash = false;
		}
	}

	public void LookAt (float x, float y){
		float angle = Mathf.Atan2 (x, y) * Mathf.Rad2Deg;
		if (x != 0f || y != 0f) {
			transform.rotation = Quaternion.Euler(new Vector3(0,0,angle));
		}
	}
}
