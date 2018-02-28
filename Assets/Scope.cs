using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour {

	Avatar avatar;
	public SpriteRenderer spriteRenderer1;
	public SpriteRenderer spriteRenderer2;

	void Start () {
		Initialize ();
	}
	
//	void Update () {
//		
//	}

	public void Initialize(){
		avatar = GetComponentInParent<Avatar> ();
//		spriteRenderer = GetComponent<SpriteRenderer> ();
//		spriteRenderer1.color = avatar.spriteRenderer.color;
		Deactivate ();
	}

	public void Activate (){
		spriteRenderer1.enabled = true;
		spriteRenderer2.enabled = true;
	}

	public void Deactivate (){
		spriteRenderer1.enabled = false;
		spriteRenderer2.enabled = false;
	}
}
