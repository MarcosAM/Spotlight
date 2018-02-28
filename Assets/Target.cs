using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void MoveAim (Vector2 direction){
		transform.Translate (direction.normalized*12*Time.deltaTime);
	}
}
