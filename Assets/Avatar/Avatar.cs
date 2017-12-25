using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : MonoBehaviour {

	[HideInInspector]public InputManager inputManager;
	[HideInInspector]public MoveActions moveActions;
	[HideInInspector]public Gun myGun;
	BoxCollider2D myBoxCollider2D;
	[HideInInspector]public Glossary.AvatarStates state = Glossary.AvatarStates.Normal;
	DashParticles dashParticles;
	AssaultParticles assaultParticles;
	[HideInInspector]public SpriteRenderer spriteRenderer;

	[HideInInspector]public Orb orb;

	public float currentLife=5;
	public float maxLife=5;
	public float stunDuration = 0.5f;

	[HideInInspector]public int victoryPoints=0;
	[HideInInspector]public int myWorth=1;
	[HideInInspector]public int position=4;

	void Start(){
		moveActions = GetComponent<MoveActions>();
		myBoxCollider2D = GetComponent<BoxCollider2D>();
		myGun = GetComponentInChildren<Gun>();
		dashParticles = GetComponentInChildren<DashParticles>();
		assaultParticles = GetComponentInChildren<AssaultParticles>();
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
	}

	void OnTriggerEnter2D (Collider2D c)
	{
		if(c.GetComponent<Wall>()){
			moveActions.StopDash();
		}
		if (c.GetComponent<Avatar> ()) {
			if(c.GetComponent<Avatar>().state == Glossary.AvatarStates.Normal){
				StealAndSwitch(c.GetComponentInChildren<Avatar>());
			}
			if(c.GetComponent<Avatar>().state == Glossary.AvatarStates.Charging){
				StealAndSwitch(c.GetComponentInChildren<Avatar>());
				c.GetComponent<Avatar> ().StopCharging ();
			}
		}

		if(c.GetComponent<Catchable>() && !c.GetComponent<Catchable>().orb.isFollowing && state == Glossary.AvatarStates.Dashing){
			CatchOrSwitch(c.GetComponent<Catchable>().orb);
		}
	}

	void OnCollisionEnter2D(Collision2D c){
		if(c.gameObject.GetComponent<Avatar>() && state == Glossary.AvatarStates.Assaulting){
			c.gameObject.GetComponent<Avatar> ().ReduceLifeBy (6,GetComponent<Avatar>());
			c.gameObject.GetComponent<Avatar> ().StartCoroutine("StartStunned");
			c.gameObject.GetComponent<MoveActions>().rigidBody2D.AddForce(moveActions.rigidBody2D.velocity*60);
		}
	}

	public void FireBtnDown (){
		if(state==Glossary.AvatarStates.Normal){
			StartCharging ();
		}
	}

	public void FireBtnUp ()
	{
		if(state == Glossary.AvatarStates.Normal){
			StopCharging ();
		}

		if(state == Glossary.AvatarStates.Charging){
			myGun.Shoot();
			StopCharging ();
		}
	}

	void StartCharging(){
		state=Glossary.AvatarStates.Charging;
	}

	public void StopCharging(){
		if (state == Glossary.AvatarStates.Charging || state == Glossary.AvatarStates.Assaulting) {
			myGun.chargeLevel=0;
			myGun.timeToCharge=myGun.ChargeTime;
			state=Glossary.AvatarStates.Normal;
		}
	}

	public IEnumerator StartStunned(){
		if(state != Glossary.AvatarStates.Stunned){
			StopCharging ();
			state = Glossary.AvatarStates.Stunned;
			yield return new WaitForSecondsRealtime(stunDuration);
		}
		if(state == Glossary.AvatarStates.Stunned)
			state = Glossary.AvatarStates.Normal;
	}

	public IEnumerator StartStunned(float duration){
		if(state != Glossary.AvatarStates.Stunned){
			StopCharging ();
			state = Glossary.AvatarStates.Stunned;
			yield return new WaitForSecondsRealtime(duration);
		}
		if(state == Glossary.AvatarStates.Stunned)
			state = Glossary.AvatarStates.Normal;
	}

	void StealAndSwitch (Avatar theirAvatar)
	{
		myGun.ammunition += 5;
		if (myGun.ammunition > myGun.maxAmmunition) {
			myGun.ammunition = myGun.maxAmmunition;
		}
		theirAvatar.myGun.ammunition -= 5;
		if (theirAvatar.myGun.ammunition < 0) {
			theirAvatar.myGun.ammunition = 0;
		}
		moveActions.canDash = true;
		if (theirAvatar.moveActions.canDash) {
			theirAvatar.moveActions.canDash = false;
			theirAvatar.moveActions.timeToRecharge = theirAvatar.moveActions.rechargeTime;
		}
		theirAvatar.StartStunned (0.3f);

		Orb theirOrb;
		Orb myOrb;
		theirOrb = theirAvatar.orb;
		myOrb = orb;
		if (theirOrb != null || myOrb != null) {
			orb = theirOrb;
			theirAvatar.orb = myOrb;
			if (orb != null) {
				orb.Follow (GetComponent<Avatar> ());
			}
			if (theirAvatar.orb != null) {
				theirAvatar.orb.Follow(theirAvatar);
			}
		}
	}

	void CatchOrSwitch (Orb otherOrb)
	{
		if(orb != null){
			orb.Release();
			orb = otherOrb;
			orb.Follow(GetComponent<Avatar>());
		}
		else {
			orb = otherOrb;
			otherOrb.Follow(GetComponent<Avatar>());
		}
	}

	public IEnumerator Heal (){
		while(currentLife < maxLife){
			currentLife ++;
			yield return new WaitForSecondsRealtime(1f);
		}
	}

	public void ReduceLifeBy (float damage, Avatar enemy)
	{
		currentLife -= damage;
		StopCharging ();
		if (currentLife <= 0) {
			enemy.victoryPoints += myWorth;
			StartCoroutine("Die");
		}
	}

	public IEnumerator Die ()
	{
		if (orb != null) {
			orb.Release();
			orb = null;
		}
		FindObjectOfType<ScoreKeeper>().RefreshPlayersWorth();
		Refresh();
		transform.position = new Vector3(300,300,transform.position.z);
		inputManager.isControllingAvatar = false;
		yield return new WaitForSecondsRealtime(5);
		inputManager.isControllingAvatar = true;
		SpawnPoint[] spawnPoints = FindObjectsOfType<SpawnPoint>();
		int n = spawnPoints.Length;
		int r = Random.Range(0,n);
		transform.position = spawnPoints[r].gameObject.transform.position;
	}

	public void Refresh(){
		currentLife = maxLife;
		myGun.ammunition = myGun.maxAmmunition;
		myGun.timeToCharge = myGun.ChargeTime;
		myGun.timeToRecharge = myGun.standartRechargeTime;
		myGun.chargeLevel = 0;
		myGun.isSizeZone = false;
		moveActions.canDash = true;
		moveActions.timeToRecharge = moveActions.rechargeTime;
		state = Glossary.AvatarStates.Normal;
	}
}