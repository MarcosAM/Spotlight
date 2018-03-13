using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeOrb : MonoBehaviour {

	float maxHP=4;
	float currentHP;
	float regenerateTime=1.5f;

	[HideInInspector] public bool isDestroyed = false;

	Glossary.Effect effect = Glossary.Effect.Nothing;

	SpriteRenderer spriteRenderer;

	void Start () {
		currentHP = maxHP;
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	public void TakeDamage (float damage){
		StopCoroutine ("Regenerate");
		ChangeHPBy (-damage);
		StartCoroutine ("Regenerate");
	}

	void ChangeHPBy (float value){
		currentHP += value;
		if(currentHP > maxHP){
			currentHP = maxHP;
		}
		if (currentHP <= 0) {
			DestroyItself ();
		}
	}

	IEnumerator Regenerate (){
		yield return new WaitForSecondsRealtime (regenerateTime);
		while (currentHP < maxHP){
			ChangeHPBy(1);
			yield return new WaitForSecondsRealtime (regenerateTime);
		}
	}

//	IEnumerator DamagedAnimation (){
//		while(currentHP<maxHP && !isDestroyed){
//			
//		}
//	}
//	public IEnumerator FlashColor(Color newColor, float time){
//		float i = 0;
//		float t = time / 10f;
//		while (1>0){
//			spriteRenderer.color = new Color (newColor.r + (originalColor.r - newColor.r) * ((Mathf.Cos (i) + 1) / 2),
//				newColor.g + (originalColor.g - newColor.g) * ((Mathf.Cos (i) + 1) / 2),
//				newColor.b + (originalColor.b - newColor.b) * ((Mathf.Cos (i) + 1) / 2),1F);
//			i += 360F / 10F;
//			yield return new WaitForSecondsRealtime (t);
//		}
//	}

	void DestroyItself(){
		isDestroyed = true;
		spriteRenderer.enabled = false;
	}
}