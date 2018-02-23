using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

	public Vector3 teleportTo;

	void OnTriggerEnter2D(Collider2D c){
		if(c.GetComponent<Avatar>()){
			c.transform.position = teleportTo;
		}
	}
}
