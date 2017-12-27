using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeZone : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D c){
		if (c.GetComponent<Avatar> ()) {
//			c.GetComponentInChildren<Gun> ().isSizeZone = true;
		}
	}

	void OnTriggerExit2D(Collider2D c){
		if (c.GetComponent<Avatar> ()) {
//			c.GetComponentInChildren<Gun> ().isSizeZone = false;
		}
	}
}
