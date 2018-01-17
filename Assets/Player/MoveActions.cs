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

	Avatar avatar;
	[HideInInspector]public Rigidbody2D rigidBody2D;
	[HideInInspector]public Vector2 velocity;
	[HideInInspector]public DashParticles dashParticles;
	[HideInInspector]public AssaultParticles assaultParticles;
	[HideInInspector] BoxCollider2D boxCollider2D;

	void Start(){
		rigidBody2D = GetComponent<Rigidbody2D>();
		avatar = GetComponent<Avatar>();
		dashParticles = GetComponentInChildren<DashParticles>();
		assaultParticles = GetComponentInChildren<AssaultParticles>();
		boxCollider2D = GetComponent<BoxCollider2D>();
	}

	void FixedUpdate (){
		if(avatar.state != Glossary.AvatarStates.Stunned)
			rigidBody2D.velocity = velocity;
//			rigidBody2D.velocity = Vector2.Lerp(rigidBody2D.velocity,velocity,0.1f);
	}

	public void RunOrAim (Vector2 lStick, Vector2 rStick)
	{
		if (avatar.state == Glossary.AvatarStates.Normal || avatar.state == Glossary.AvatarStates.Charging) {
			currentSpeed = walkingSpeed;
			lookDirection = new Vector2 (rStick.x, rStick.y);
			if (hasAim && (rStick.x == 0f && rStick.y == 0f)) {
				currentSpeed = runningSpeed;
				lookDirection = new Vector2 (lStick.x, -lStick.y);
			}
			LookAt(lookDirection.x,lookDirection.y);
			velocity = lStick.normalized*currentSpeed;
			dashParticles.LookAt(lStick);
			assaultParticles.LookAt(lStick);
		}
	}

	public void DashBtnUp (){
		if(avatar.state == Glossary.AvatarStates.Assaulting){
			assaultParticles.StopAssault();
			avatar.myGun.Overheat();
			avatar.myGun.FireBtnUp();
			StartCoroutine("RechargeDash");
		}
		if(avatar.state == Glossary.AvatarStates.Dashing){
			avatar.state = Glossary.AvatarStates.Normal;
			boxCollider2D.isTrigger = false;
			boxCollider2D.size = new Vector2(1,1);
			dashParticles.StopDash();
			StartCoroutine("RechargeDash");
			avatar.didStole = false;
		}
		StopCoroutine("DashBtnDown");
	}

	public IEnumerator DashBtnDown(){
		if (avatar.state == Glossary.AvatarStates.Normal && canDash) {
			Dash();
			avatar.state = Glossary.AvatarStates.Dashing;
			boxCollider2D.isTrigger = true;
			boxCollider2D.size = new Vector2(2,2);
			dashParticles.StartDash();
			yield return new WaitForSecondsRealtime (dashDuration);
		}
		if (avatar.state == Glossary.AvatarStates.Charging && avatar.myGun.hasCharged){
			StopCoroutine ("RechargeDash");
			avatar.state = Glossary.AvatarStates.Assaulting;
			avatar.myGun.chargeParticles.StopCharge ();
			boxCollider2D.size = new Vector2(2,2);
			Dash();
			assaultParticles.StartAssault();
			yield return new WaitForSecondsRealtime (dashDuration);
		}
		DashBtnUp();
	}

	public IEnumerator RechargeDash (){
		yield return new WaitForSecondsRealtime(rechargeTime);
		if(!canDash){
			canDash = true;
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
