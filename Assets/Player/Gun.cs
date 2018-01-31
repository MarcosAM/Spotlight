using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

	[HideInInspector]public float temperature = 0;
	public float maxTemperature = 6;

	public float coolDownTime = 0.2f;
	[HideInInspector]public float currentCoolDownTime = 0.2f;

	bool isHoldingBtn = false;

	[HideInInspector]public bool hasOverheated = false;
	public float overheatTime = 1f;

	public float bulletSize = 2f;
	[HideInInspector]public float currentBulletSize;

	[HideInInspector]public bool damageZone = false;
	[HideInInspector]public bool piercingZone = false;
	[HideInInspector]public float speedZone = 0;
	[HideInInspector]public float sizeZone = 0;

	[HideInInspector]public Avatar avatar;
	public Projectile projectilePrefab;
	HUDTemperature hudTemperature;

	void Start (){
		avatar = GetComponentInParent<Avatar>();
		hudTemperature = GetComponentInChildren<HUDTemperature>();
		hudTemperature.AjustSize(temperature,maxTemperature);
		currentBulletSize = bulletSize;
		currentCoolDownTime = coolDownTime;
	}

	public void FireBtnDown (){
		if(avatar.state == Glossary.AvatarStates.Normal && !hasOverheated){
			Shoot();
		}
	}

	public void FireBtnUp (){
		isHoldingBtn = false;
	}

	public void StopCharging (){
		if (avatar.state == Glossary.AvatarStates.Charging || avatar.state == Glossary.AvatarStates.Assaulting) {
			avatar.state=Glossary.AvatarStates.Normal;
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
			temperature++;
			hudTemperature.AjustSize(temperature,maxTemperature);
			if (temperature >= maxTemperature) {
				Overheat();
			} else {
				StartCoroutine("CoolDown");
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
		isHoldingBtn = true;
		while((temperature>0 && !hasOverheated) || isHoldingBtn == true){
			yield return new WaitForSecondsRealtime(currentCoolDownTime);
			if(temperature>0)
				temperature --;
			hudTemperature.AjustSize(temperature,maxTemperature);
			if(isHoldingBtn && avatar.state == Glossary.AvatarStates.Normal){
				Shoot ();
			}
		}
	}

	public void ResetGun ()
	{
		temperature = 0;
		hudTemperature.AjustSize(temperature,maxTemperature);
		hasOverheated = false;
		StopAllCoroutines();
		currentBulletSize = bulletSize;
	}
}