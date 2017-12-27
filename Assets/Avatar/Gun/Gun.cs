using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

	[HideInInspector]public float temperature = 0;
	public float maxTemperature = 6;
	[HideInInspector]public bool hasOverheated = false;
	public float coolDownTime = 0.2f;
	[HideInInspector]public float currentCoolDownTime = 0.2f;
	public float overheatTime = 1f;

	public float ChargeTime;
	[HideInInspector]public bool hasCharged = false;

	public float bulletSize = 2f;
	public float chargedBulletSize = 5f;
	[HideInInspector]public float currentBulletSize;

	[HideInInspector]public bool damageZone = false;
	[HideInInspector]public bool piercingZone = false;
	[HideInInspector]public float speedZone = 0;
	[HideInInspector]public float sizeZone = 0;

	[HideInInspector]public Avatar avatar;
	public Projectile projectilePrefab;
	[HideInInspector]public ChargeParticles chargeParticles;
	HUDTemperature hudTemperature;

	void Start (){
		avatar = GetComponentInParent<Avatar>();
		chargeParticles = GetComponentInChildren<ChargeParticles> ();
		hudTemperature = GetComponentInChildren<HUDTemperature>();
		hudTemperature.AjustSize(temperature,maxTemperature);
		currentBulletSize = bulletSize;
	}

	public IEnumerator FireBtnDown (){
		if(avatar.state == Glossary.AvatarStates.Normal && !hasOverheated){
			Shoot();
			yield return new WaitForSecondsRealtime(0.1f);
			avatar.state = Glossary.AvatarStates.Charging;
			chargeParticles.Charge();
			yield return new WaitForSecondsRealtime(ChargeTime);
			hasCharged = true;
			chargeParticles.SustainCharge();
			currentBulletSize = chargedBulletSize;
		}
	}

	public void FireBtnUp (){
		if(avatar.state == Glossary.AvatarStates.Charging){
			Shoot();
		}
		StopCharging();
	}

	public void StopCharging (){
		if (avatar.state == Glossary.AvatarStates.Charging || avatar.state == Glossary.AvatarStates.Assaulting) {
			hasCharged = false;
			avatar.state=Glossary.AvatarStates.Normal;
			chargeParticles.StopCharge ();
			currentBulletSize = bulletSize;
		}
		StopCoroutine("FireBtnDown");
	}

	public void Shoot ()
	{
		StopCoroutine ("CoolDown");
		if (!hasOverheated) {
			Projectile projectile = Instantiate (projectilePrefab, transform.position, Quaternion.identity);
			Vector2 projectileDirection = -transform.up;
			projectile.direction = projectileDirection.normalized;
			projectile.gunFiredMe = this;
			projectile.changeSize (currentBulletSize + sizeZone);
			if (speedZone != 0) {
				projectile.speed += speedZone;
			}
			if(piercingZone)
				projectile.isPiercing = true;
			if (hasCharged) {
				Overheat ();
				projectile.currentDamage = projectile.chargedDamage;
			} else {
				temperature++;
				hudTemperature.AjustSize(temperature,maxTemperature);
				if (temperature >= maxTemperature) {
					Overheat();
				} else {
					StartCoroutine("CoolDown");
				}
			}
			if (damageZone) {
				projectile.currentDamage = projectile.zoneDamage;
			}
		}
	}

	public void Overheat ()
	{
		if(temperature != maxTemperature){
			temperature = maxTemperature;
			hudTemperature.AjustSize(temperature,maxTemperature);
		}
		hasOverheated = true;
		StartCoroutine("StopOverheat");
	}

	public IEnumerator StopOverheat ()
	{
		yield return new WaitForSecondsRealtime(overheatTime);
		EndOverheat();
	}

	public void EndOverheat ()
	{
		hasOverheated = false;
		temperature = 0;
		hudTemperature.AjustSize(temperature,maxTemperature);
		StopCoroutine("StopOverheat");
	}

	public IEnumerator CoolDown ()
	{
		while(temperature>0 && !hasOverheated){
			yield return new WaitForSecondsRealtime(currentCoolDownTime);
			temperature --;
			hudTemperature.AjustSize(temperature,maxTemperature);
		}
	}

	public void ResetGun ()
	{
		temperature = 0;
		hudTemperature.AjustSize(temperature,maxTemperature);
		hasOverheated = false;
		StopAllCoroutines();
		hasCharged = false;
		chargeParticles.StopCharge();
		currentBulletSize = bulletSize;
	}
}