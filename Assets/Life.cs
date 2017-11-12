using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour {

	public float maxLife;
	public float currentLife;
	public bool isAlive = true;

	public void ReduceLifeBy (float damage){
		currentLife -= damage;
		if(currentLife < 0)
			isAlive = false;
	}
}
