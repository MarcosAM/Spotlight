using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDDash : MonoBehaviour {

	Vector3 size;

	Avatar avatar;

	void Start () {
		avatar = GetComponentInParent<Avatar>();
		size = transform.localScale;
	}
	
	void Update ()
	{
		if (avatar.moveActions.dashesAvailable > 0) {
			transform.localScale = size;
		} else {
			transform.localScale = Vector3.zero;
		}
	}
}
