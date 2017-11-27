using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catchable : MonoBehaviour {

	Orb orb;

	void Start () {
		orb = GetComponentInParent<Orb> ();
	}

	void OnTriggerEnter2D(Collider2D c){
		if(c.GetComponent<Avatar>() && c.GetComponent<Avatar>().myAvatarState == Glossary.AvatarStates.Dashing){
			orb.Follow (c.GetComponent<Avatar> ());
		}
	}
}
