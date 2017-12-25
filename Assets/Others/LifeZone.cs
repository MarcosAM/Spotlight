using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeZone : MonoBehaviour {
			
	void OnTriggerEnter2D(Collider2D c){
		c.GetComponent<Avatar>().StartCoroutine("Heal");
	}

	void OnTriggerExit2D(Collider2D c){
		c.GetComponent<Avatar>().StopCoroutine("Heal");
	}
}
