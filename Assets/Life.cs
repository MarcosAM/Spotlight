using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour {

	public int points = 0;

	public float maxLife;
	public float currentLife;
	public bool isAlive = true;

	public float currentTimeToHeal;
	public float countdownToHeal;

	void Update (){
		if (currentLife < maxLife) {
			countdownToHeal += Time.deltaTime;
			if (countdownToHeal >= currentTimeToHeal) {
				currentLife++;
				countdownToHeal = 0;
			}
		}
	}

	public void ReduceLifeBy (float damage, GameObject attacker){
		currentLife -= damage;
		if (currentLife < 0) {
			Die();
			GivePointsTo (attacker);
		}
	}

	public void Die (){
		isAlive = false;
	}

	public void GivePointsTo(GameObject o){
		o.GetComponent<Life> ().points++;
	}
}
