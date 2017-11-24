using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : MonoBehaviour {

	MoveActions moveActions;
	Gun myGun;
	public Glossary.AvatarStates myAvatarState = Glossary.AvatarStates.Normal;
	BoxCollider2D myBoxCollider2D;

	public float timeToChangeBack;
	public float currentLife=5;
	float maxLife=5;

	void Start(){
		moveActions = GetComponent<MoveActions>();
		myBoxCollider2D = GetComponent<BoxCollider2D>();
		myGun = GetComponentInChildren<Gun>();
	}

	void Update ()
	{
		if (myAvatarState == Glossary.AvatarStates.Dashing) {
			timeToChangeBack -= Time.deltaTime;
			if(timeToChangeBack <= 0)
				StopDash();
		}

		if (myAvatarState == Glossary.AvatarStates.Assaulting) {
			timeToChangeBack -= Time.deltaTime;
			if (timeToChangeBack <= 0) {
				StopAssaulting ();
				myGun.chargeLevel=0;
				myGun.timeToCharge=myGun.ChargeTime;
			}
		}

		if(myAvatarState == Glossary.AvatarStates.Stunned){
			timeToChangeBack -= Time.deltaTime;
			if (timeToChangeBack <= 0)
				StopStunned ();
		}
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
			c.gameObject.GetComponent<Avatar> ().StartStunned ();
			c.gameObject.GetComponent<MoveActions>().myRigidbody2D.AddForce(moveActions.myRigidbody2D.velocity*60);
		}
	}

	public void LeftStick (Vector2 LStick){
		if(myAvatarState == Glossary.AvatarStates.Normal || myAvatarState == Glossary.AvatarStates.Charging)
			moveActions.Walk(LStick);
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
		if (myAvatarState == Glossary.AvatarStates.Normal && moveActions.dashesAvailable > 0)
			StartDash();
		StartAssaulting ();
	}

	public void DashBtnUp (){
		if(myAvatarState == Glossary.AvatarStates.Dashing)
			StopDash();
		StopAssaulting ();
	}

	void StartDash(){
		moveActions.Dash();
		myAvatarState = Glossary.AvatarStates.Dashing;
		timeToChangeBack = moveActions.dashDuration;
		myBoxCollider2D.isTrigger = true;
	}

	void StopDash (){
		myAvatarState = Glossary.AvatarStates.Normal;
		myBoxCollider2D.isTrigger = false;
	}

	void StartCharging(){
		myAvatarState=Glossary.AvatarStates.Charging;
	}

	public void StartStunned(){
		StopCharging ();
		myAvatarState = Glossary.AvatarStates.Stunned;
		timeToChangeBack = 0.5f;
	}

	public void StopStunned(){
		myAvatarState = Glossary.AvatarStates.Normal;
	}

	public void StopCharging(){
		if (myAvatarState == Glossary.AvatarStates.Charging) {
			myGun.chargeLevel=0;
			myGun.timeToCharge=myGun.ChargeTime;
			myAvatarState=Glossary.AvatarStates.Normal;
		}
	}

	void StartAssaulting(){
		if(myAvatarState == Glossary.AvatarStates.Charging){
			myAvatarState = Glossary.AvatarStates.Assaulting;
			moveActions.Dash();
			timeToChangeBack = moveActions.dashDuration;
		}
	}

	void StopAssaulting(){
		if(myAvatarState == Glossary.AvatarStates.Assaulting){
			myAvatarState = Glossary.AvatarStates.Normal;
			StopCharging ();
		}
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