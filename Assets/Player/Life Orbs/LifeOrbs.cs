using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeOrbs : MonoBehaviour {

	Avatar avatar;
	SpriteRenderer[] spriteRenderers;

	void Start () {
		avatar = GetComponentInParent<Avatar> ();
		spriteRenderers = GetComponentsInChildren<SpriteRenderer> ();
		foreach (SpriteRenderer sr in spriteRenderers){
			sr.color = avatar.spriteRenderer.color;
		}
	}
	
	public void RefreshLifeOrbs () {
		for(int i = spriteRenderers.Length; i > 0;i--){
			if (i > avatar.currentLife && avatar.currentLife != 0) {
				spriteRenderers [i - 1].enabled = false;
			} else {
				spriteRenderers [i - 1].enabled = true;
			}
		}
	}
}
