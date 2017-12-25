using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

	[HideInInspector]public Avatar myAvatar;
	public Projectile projectilePrefab;
	public ChargeParticles chargeParticles;

	[HideInInspector]public float ammunition = 10;
	[HideInInspector]public float maxAmmunition = 10;
	[HideInInspector]public float currentRechargeTime;
	public float standartRechargeTime;
	public float timeToRecharge;

	public float timeToCharge;
	public float ChargeTime;

	public int chargeLevel = 0;

	public bool isSizeZone = false;

	void Start (){
		myAvatar = GetComponentInParent<Avatar>();
		chargeParticles = GetComponentInChildren<ChargeParticles> ();
	}

	void Update ()
	{
		if (myAvatar.myAvatarState == Glossary.AvatarStates.Normal && ammunition < maxAmmunition) {
			timeToRecharge -= Time.deltaTime;
			if (timeToRecharge <= 0) {
				ammunition++;
				timeToRecharge = currentRechargeTime;
			}
		}

		if (myAvatar.myAvatarState == Glossary.AvatarStates.Charging && ammunition > 1+ chargeLevel && chargeLevel < 2) {
			timeToCharge -= Time.deltaTime;
			if(timeToCharge <= 0){
				timeToCharge = ChargeTime;
				chargeLevel++;
				chargeParticles.ChargeUp (chargeLevel);
				ammunition -= 2;
			}
		}
	}

	public void Shoot ()
	{
		if (ammunition > 0) {
			Projectile projectile = Instantiate (projectilePrefab, transform.position, Quaternion.identity);
			Vector2 projectileDirection = -transform.up;
			projectile.direction = projectileDirection.normalized;
			projectile.gunFiredMe = GetComponent<Gun> ();
			chargeParticles.ChargeDown ();
			if (chargeLevel == 1) {
				if(isSizeZone)
					projectile.transform.localScale = new Vector3 (projectile.transform.localScale.x * 4.5f, projectile.transform.localScale.y * 4.5f, projectile.transform.localScale.z * 4.5f);
				else
					projectile.transform.localScale = new Vector3 (projectile.transform.localScale.x * 3f, projectile.transform.localScale.y * 3f, projectile.transform.localScale.z * 3f);
			}
			if (chargeLevel == 2) {
				if(isSizeZone)
					projectile.transform.localScale = new Vector3 (projectile.transform.localScale.x * 7f, projectile.transform.localScale.y * 7f, projectile.transform.localScale.z * 7f);
				else
					projectile.transform.localScale = new Vector3 (projectile.transform.localScale.x * 5f, projectile.transform.localScale.y * 5f, projectile.transform.localScale.z * 5f);
			}
			if (chargeLevel == 0) {
				ammunition --;
				if(isSizeZone)
					projectile.transform.localScale = new Vector3 (projectile.transform.localScale.x * 3f, projectile.transform.localScale.y * 3f, projectile.transform.localScale.z * 3f);
			}
		}
	}
}
