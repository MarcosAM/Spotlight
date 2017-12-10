using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDAmmunition : MonoBehaviour {

	Vector3 maxSize;
	Vector3 currentSize;

	void Start () {
		maxSize = transform.localScale;
	}
	
	void Update () {
		transform.localScale = new Vector3((GetComponentInParent<Avatar>().myGun.ammunition/GetComponentInParent<Avatar>().myGun.maxAmmunition)*maxSize.x,maxSize.y,maxSize.z);
	}
}
