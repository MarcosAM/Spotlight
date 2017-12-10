using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDLife : MonoBehaviour {

	Vector3 maxSize;
	Vector3 currentSize;

	Avatar avatar;

	void Start () {
		maxSize = transform.localScale;
		avatar = GetComponentInParent<Avatar>();
	}
	
	void Update () {
		transform.localScale = new Vector3((avatar.currentLife/avatar.maxLife)*maxSize.x,maxSize.y,maxSize.z);
	}
}
