using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catchable : MonoBehaviour {

	public Orb orb;

	void Start () {
		orb = GetComponentInParent<Orb> ();
	}
}
