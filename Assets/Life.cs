using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour {

	public int points = 0;
	public int position;

	public float maxLife;
	public float currentLife;
	public bool isAlive = true;

	public float currentTimeToHeal;
	public float countdownToHeal;

	public float timeToRessurect;

	void Update (){

		if(!isAlive){
			timeToRessurect += Time.deltaTime;
			if(timeToRessurect > 1f){
				Ressurect();
			}
		}
	
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
		if (currentLife < 0 && isAlive) {
			Die();
			GivePointsTo (attacker);
		}
	}

	public void Die (){
		isAlive = false;
		gameObject.transform.position = new Vector2 (-300,-300);
		timeToRessurect = 0;
	}

	public void GivePointsTo(GameObject o){
		if(position == 4)
			o.GetComponent<Life> ().points++;
		if(position == 3)
			o.GetComponent<Life> ().points +=2;
		if(position == 2)
			o.GetComponent<Life> ().points +=3;
		if(position == 1)
			o.GetComponent<Life> ().points +=4;

		FindObjectOfType<FightManager>().RefreshAvatarPositions();
	}

	public void Ressurect (){
		currentLife = maxLife;
		GetComponent<Weapon>().currentAmmunition = GetComponent<Weapon>().maxAmmunition;
		GetComponent<Movement>().dashesAvailable = 1;
		SpawnPoint[] SpawnPoints = FindObjectsOfType<SpawnPoint>();
		gameObject.transform.position = SpawnPoints[Random.Range(0,4)].transform.position;
		isAlive = true;
	}
}
