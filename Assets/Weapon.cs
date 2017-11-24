using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public Projectile projectilePrefab;
	public float currentAmmunition;
	public float maxAmmunition;

	public float standartRechargeTime;
	public float currentRechargeTime;
	public float countdownToRecharge;

	public int chargeLevel = 0;

	void Update(){
		if (currentAmmunition < maxAmmunition && !GetComponent<Movement>().isDashing) {
			countdownToRecharge += Time.deltaTime;
			if (countdownToRecharge >= currentRechargeTime) {
				currentAmmunition++;
				countdownToRecharge = 0;
			}
		}
	}

	public void Shoot (){
		if(currentAmmunition > 0){
			Projectile projectile = Instantiate (projectilePrefab,transform.position,Quaternion.identity);
			Vector2 projectileDirection = -transform.up;
			projectile.direction = projectileDirection.normalized;
			projectile.gunFiredMe = GetComponent<Gun>();
			currentAmmunition -= 1;
		}
	}
}
