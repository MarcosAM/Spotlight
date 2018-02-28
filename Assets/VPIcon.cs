using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VPIcon : MonoBehaviour {

	float duration;
	float speed;
	SpriteRenderer spriteRenderer;
	TextMesh text;
	
	void Update () {
		transform.Translate (transform.up*speed*Time.deltaTime);
		duration -= Time.deltaTime;
		if(duration <= 0){
			Destroy (gameObject);
		}
	}

	public void Initialize (float d, float s, Color c, int v){
		spriteRenderer = GetComponent<SpriteRenderer> ();
		text = GetComponentInChildren<TextMesh> ();
		duration = d;
		speed = s;
		spriteRenderer.color = c;
		text.text = "+" + v;
	}
}
