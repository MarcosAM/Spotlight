using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public Projectile projectilePrefab;

	public void Shoot (){
		Projectile projectile = Instantiate (projectilePrefab,transform.position,Quaternion.identity);
		Vector2 projectileDirection = -transform.up;
		projectile.direction = projectileDirection.normalized;
	}
}
