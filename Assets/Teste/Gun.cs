using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

	Avatar myAvatar;
	public Projectile projectilePrefab;

	public int ammunition = 10;
	int maxAmmunition = 10;
	public float rechargeTime;
	public float timeToRecharge;

	public float timeToCharge;
	public float ChargeTime;

	public int chargeLevel = 0;

	void Start (){
		myAvatar = GetComponentInParent<Avatar>();
	}

	void Update ()
	{
		if (myAvatar.myAvatarState == Glossary.AvatarStates.Normal && ammunition < maxAmmunition) {
			timeToRecharge -= Time.deltaTime;
			if (timeToRecharge <= 0) {
				ammunition++;
				timeToRecharge = rechargeTime;
			}
		}

		if (myAvatar.myAvatarState == Glossary.AvatarStates.Charging && ammunition > 1+ chargeLevel && chargeLevel < 2) {
			timeToCharge -= Time.deltaTime;
			if(timeToCharge <= 0){
				timeToCharge = ChargeTime;
				chargeLevel++;
				ammunition -= 2;
			}
		}
	}

	public void Shoot(){
		if(ammunition > 0){
			Projectile projectile = Instantiate (projectilePrefab,transform.position,Quaternion.identity);
			Vector2 projectileDirection = -transform.up;
			projectile.direction = projectileDirection.normalized;
			projectile.weaponFiredMe = GetComponent<Weapon>();
			if(chargeLevel==1){
				projectile.transform.localScale = new Vector3(projectile.transform.localScale.x*3f,projectile.transform.localScale.y*3f,projectile.transform.localScale.z*3f);
			}
			if(chargeLevel==2){
				projectile.transform.localScale = new Vector3(projectile.transform.localScale.x*5f,projectile.transform.localScale.y*5f,projectile.transform.localScale.z*5f);
			}
			if(chargeLevel==0)
				ammunition --;
		}
	}
}
