using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VPIcon : MonoBehaviour {

	float duration;
	float speed;
	SpriteRenderer spriteRenderer;
	
	void Update () {
		transform.Translate (transform.up*speed*Time.deltaTime);
		duration -= Time.deltaTime;
		if(duration <= 0){
			Destroy (gameObject);
		}
	}

	public void Initialize (float d, float s, Color c){
		spriteRenderer = GetComponent<SpriteRenderer> ();
		duration = d;
		speed = s;
		spriteRenderer.color = c;
	}
}
