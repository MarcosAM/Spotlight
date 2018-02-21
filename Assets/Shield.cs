using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

	public Collider2D boxCollider;

	void Start(){
		boxCollider = GetComponent<Collider2D> ();
	}
}
