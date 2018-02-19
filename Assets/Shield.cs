using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

	[HideInInspector]public BoxCollider2D boxCollider2D;
	[HideInInspector]public Avatar avatar;

	void Start () {
		boxCollider2D = GetComponent<BoxCollider2D> ();
		avatar = GetComponentInParent<Avatar> ();
	}
}
