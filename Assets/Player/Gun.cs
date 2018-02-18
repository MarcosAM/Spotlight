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

	[SerializeField]bool isPoisoned= false;
	public float poisonSpeed=0.4f;
	public bool isPoisonous = false;
	[HideInInspector]public Avatar poisonousEnemy;

	[HideInInspector]public bool doesBounce = false;
	[HideInInspector]public bool hasTripleShot = false;
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

	public void Shoot ()
	{
		StopCoroutine ("CoolDown");
		int projectiles = 1;
		if(hasTripleShot)
			projectiles = 3;
		if (!hasOverheated) {
			for (int i=1; i<=projectiles;i++){
				Projectile projectile = Instantiate (projectilePrefab, transform.position - transform.up*0.5f, Quaternion.identity);
				Vector2 projectileDirection = -transform.up;
				projectile.direction = projectileDirection.normalized;
				if(i==2)
					projectile.direction = Quaternion.Euler(0,0,10f)*projectile.direction;
				if(i==3)
					projectile.direction = Quaternion.Euler(0,0,-10f)*projectile.direction;
				projectile.gunFiredMe = this;
				if(doesBounce){
					projectile.isBouncing = true;
				}
				if(isPoisonous){
					projectile.isPoisonous = true;
				}
				projectile.changeSize (currentBulletSize + sizeZone);
				if (speedZone != 0) {
					projectile.speed += speedZone;
				}
				if(piercingZone)
					projectile.isPiercing = true;
				if (damageZone) {
					projectile.currentDamage = projectile.zoneDamage;
				}
			}
			temperature++;
			hudTemperature.AjustSize(temperature,maxTemperature);
			if (temperature >= maxTemperature) {
				Overheat();
			} else {
				StartCoroutine("CoolDown");
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
		StopPoison();
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
		isPoisoned = false;
	}

	public void GetPoisoned (Avatar enemy){
		if(!isPoisoned){
			isPoisoned = true;
			poisonousEnemy = enemy;
			StartCoroutine("DamageByPoison");
		}
	}

	IEnumerator DamageByPoison (){
		Avatar enemy = poisonousEnemy;
		while (isPoisoned){
			yield return new WaitForSecondsRealtime (poisonSpeed);
			avatar.ReduceLifeBy (1F,enemy);
		}
	}

	public void StopPoison (){
		isPoisoned = false;
		StopCoroutine("DamageByPoison");
	}
}