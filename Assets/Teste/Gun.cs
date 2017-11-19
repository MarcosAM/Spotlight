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
	}

	public void Shoot(){
		if(ammunition > 0){
			Projectile projectile = Instantiate (projectilePrefab,transform.position,Quaternion.identity);
			Vector2 projectileDirection = -transform.up;
			projectile.direction = projectileDirection.normalized;
			projectile.weaponFiredMe = GetComponent<Weapon>();
			ammunition --;
		}
	}
}
