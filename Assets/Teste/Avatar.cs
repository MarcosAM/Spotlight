using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : MonoBehaviour {

	MoveActions moveActions;
	Gun myGun;
	public Glossary.AvatarStates myAvatarState = Glossary.AvatarStates.Normal;
	BoxCollider2D myBoxCollider2D;

	public float timeToChangeBack;

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
	}

	void OnTriggerEnter2D (Collider2D c)
	{
		if (c.GetComponent<Avatar> ()) {
			if(GetComponent<Avatar>().myAvatarState == Glossary.AvatarStates.Normal){
				Catch(c.GetComponentInChildren<Gun>(), c.GetComponent<MoveActions>());
				}
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
			myAvatarState=Glossary.AvatarStates.Charging;
		}
	}

	public void FireBtnUp ()
	{
		if (myAvatarState == Glossary.AvatarStates.Charging) {
			myGun.Shoot();
			myGun.chargeLevel=0;
			myGun.timeToCharge=myGun.ChargeTime;
			myAvatarState=Glossary.AvatarStates.Normal;
		}
	}

	public void DashBtnDown ()
	{
		if (myAvatarState == Glossary.AvatarStates.Normal && moveActions.dashesAvailable > 0)
			StartDash();
	}

	public void DashBtnUp (){
		if(myAvatarState == Glossary.AvatarStates.Dashing)
			StopDash();
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

	void Catch (Gun theirGun, MoveActions theirMoveAction)
	{
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
}
