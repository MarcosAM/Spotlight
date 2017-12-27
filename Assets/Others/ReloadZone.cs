using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadZone : MonoBehaviour {

	public float newRechargeTime = 0.5f;

	void OnTriggerEnter2D(Collider2D c){
		if (c.GetComponent<Avatar> ()) {
//			c.GetComponentInChildren<Gun> ().currentRechargeTime = newRechargeTime;
		}
	}

	void OnTriggerExit2D(Collider2D c){
		if (c.GetComponent<Avatar> ()) {
//			c.GetComponentInChildren<Gun> ().currentRechargeTime = c.GetComponentInChildren<Gun> ().standartRechargeTime;
		}
	}

}
