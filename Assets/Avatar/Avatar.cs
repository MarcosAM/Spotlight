using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : MonoBehaviour {

	MoveActions moveActions;
	public Gun myGun;
	BoxCollider2D myBoxCollider2D;
	public Glossary.AvatarStates myAvatarState = Glossary.AvatarStates.Normal;
	DashParticles dashParticles;

	public float currentLife=5;
	float maxLife=5;
	public float stunDuration = 0.5f;

	void Start(){
		moveActions = GetComponent<MoveActions>();
		myBoxCollider2D = GetComponent<BoxCollider2D>();
		myGun = GetComponentInChildren<Gun>();
		dashParticles = GetComponentInChildren<DashParticles>();
	}

	void OnTriggerEnter2D (Collider2D c)
	{
		if (c.GetComponent<Avatar> ()) {
			if(c.GetComponent<Avatar>().myAvatarState == Glossary.AvatarStates.Normal){
				Catch(c.GetComponentInChildren<Gun>(), c.GetComponent<MoveActions>());
			}
			if(c.GetComponent<Avatar>().myAvatarState == Glossary.AvatarStates.Charging){
				Catch(c.GetComponentInChildren<Gun>(), c.GetComponent<MoveActions>());
				c.GetComponent<Avatar> ().StopCharging ();
			}
		}
	}

	void OnCollisionEnter2D(Collision2D c){
		if(c.gameObject.GetComponent<Avatar>() && myAvatarState == Glossary.AvatarStates.Assaulting){
			c.gameObject.GetComponent<Avatar> ().ReduceLifeBy (myGun.chargeLevel*2);
			c.gameObject.GetComponent<Avatar> ().StartCoroutine("StartStunned");
			c.gameObject.GetComponent<MoveActions>().myRigidbody2D.AddForce(moveActions.myRigidbody2D.velocity*60);
		}
	}

	public void LeftStick (Vector2 LStick){
		if(myAvatarState == Glossary.AvatarStates.Normal || myAvatarState == Glossary.AvatarStates.Charging)
			moveActions.Walk(LStick);
		if(myAvatarState != Glossary.AvatarStates.Dashing){
			float angle2 = Mathf.Atan2(LStick.x, -LStick.y) * Mathf.Rad2Deg;
			if(LStick.x != 0f || LStick.y != 0f)
				dashParticles.transform.rotation = Quaternion.Euler(new Vector3(0,0,angle2));
		}
	}

	public void RightStick (Vector2 RStick){
		float angle = Mathf.Atan2(RStick.x, RStick.y) * Mathf.Rad2Deg;
		if(RStick.x != 0f || RStick.y != 0f)
			transform.rotation = Quaternion.Euler(new Vector3(0,0,angle));
	}

	public void FireBtnDown (){
		if(myAvatarState==Glossary.AvatarStates.Normal){
			StartCharging ();
		}
	}

	public void FireBtnUp ()
	{
		if(myAvatarState == Glossary.AvatarStates.Normal){
			StopCharging ();
		}

		if(myAvatarState == Glossary.AvatarStates.Charging){
			myGun.Shoot();
			StopCharging ();
		}
	}

	public void DashBtnDown ()
	{
		StartCoroutine("StartDash");
		StartCoroutine ("StartAssaulting");
	}

	public void DashBtnUp (){
		StopDash();
		StopAssaulting ();
	}

	IEnumerator StartDash ()
	{
		if (myAvatarState == Glossary.AvatarStates.Normal && moveActions.dashesAvailable > 0) {
			moveActions.Dash();
			myAvatarState = Glossary.AvatarStates.Dashing;
			myBoxCollider2D.isTrigger = true;
			dashParticles.StartDash();
			yield return new WaitForSeconds(moveActions.dashDuration);
		}
		if(myAvatarState == Glossary.AvatarStates.Dashing){
			StopDash();
		}
	}

	void StopDash (){
		if(myAvatarState == Glossary.AvatarStates.Dashing){
			myAvatarState = Glossary.AvatarStates.Normal;
			myBoxCollider2D.isTrigger = false;
			dashParticles.StopDash();
			StopCoroutine("StartDash");
		}
	}

	void StartCharging(){
		myAvatarState=Glossary.AvatarStates.Charging;
	}

	public void StopCharging(){
		if (myAvatarState == Glossary.AvatarStates.Charging || myAvatarState == Glossary.AvatarStates.Assaulting) {
			myGun.chargeLevel=0;
			myGun.timeToCharge=myGun.ChargeTime;
			myAvatarState=Glossary.AvatarStates.Normal;
		}
	}

	IEnumerator StartAssaulting(){
		if(myAvatarState == Glossary.AvatarStates.Charging){
			myAvatarState = Glossary.AvatarStates.Assaulting;
			myGun.chargeParticles.ChargeDown ();
			moveActions.Dash();
			yield return new WaitForSecondsRealtime (moveActions.dashDuration);
		}

		if(myAvatarState == Glossary.AvatarStates.Assaulting)
			StopAssaulting();
	}

	void StopAssaulting(){
		if(myAvatarState == Glossary.AvatarStates.Assaulting){
			StopCharging ();
			StopCoroutine("StartAssaulting");
		}
	}

	public IEnumerator StartStunned(){
		if(myAvatarState != Glossary.AvatarStates.Stunned){
			StopCharging ();
			myAvatarState = Glossary.AvatarStates.Stunned;
			yield return new WaitForSecondsRealtime(stunDuration);
		}
		if(myAvatarState == Glossary.AvatarStates.Stunned)
			myAvatarState = Glossary.AvatarStates.Normal;
	}

	void Catch (Gun theirGun, MoveActions theirMoveAction){
		int myAmmunition = myGun.ammunition;
		int theirAmmunition = theirGun.ammunition;
		myGun.ammunition = theirAmmunition;
		theirGun.ammunition = myAmmunition;
		moveActions.dashesAvailable++;
		if (theirMoveAction.dashesAvailable > 0) {
			theirMoveAction.dashesAvailable--;
			theirMoveAction.timeToRecharge = theirMoveAction.rechargeTime;
		}
	}

	public void ReduceLifeBy(float damage){
		currentLife -= damage;
		StopCharging ();
		if (currentLife <= 0)
			Die ();
	}

	public void Die(){
		gameObject.SetActive (false);
	}

	public void Refresh(){
		currentLife = maxLife;
		myGun.ammunition = myGun.maxAmmunition;
		moveActions.dashesAvailable = 1;
		myAvatarState = Glossary.AvatarStates.Normal;
	}
}