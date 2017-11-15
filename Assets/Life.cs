using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour {

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

	public void ReduceLifeBy (float damage){
		currentLife -= damage;
		if(currentLife < 0)
			Die();
	}

	public void Die (){
		isAlive = false;
	}
}
