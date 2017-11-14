using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmunitionDisplay : MonoBehaviour {

	public Weapon weapon;
	public float maxAmmunitionScale;
	
	void Start () {
		weapon = GetComponentInParent<Weapon>();
		maxAmmunitionScale = transform.localScale.x;
	}

	void Update () {
		Vector3 currentScale = new Vector3 ((weapon.currentAmmunition/weapon.maxAmmunition)*maxAmmunitionScale,transform.localScale.y, transform.localScale.z);
		transform.localScale = currentScale;
	}
}
