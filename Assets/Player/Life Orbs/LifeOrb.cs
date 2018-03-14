using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeOrb : MonoBehaviour {

	float maxHP=4;
	float currentHP;
	float regenerateTime=3f;
	bool damagedAnimation=false;

	[HideInInspector] public Color standartColor;

	[HideInInspector] public bool isDestroyed = false;

	Glossary.Effect effect = Glossary.Effect.Nothing;
	LifeOrbs lifeOrbsManager;
	SpriteRenderer spriteRenderer;

	public void TakeDamage (float damage){
		StopCoroutine ("Regenerate");
		ChangeHPBy (-damage);
		if (!damagedAnimation)
			StartCoroutine ("DamagedAnimation");
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

	IEnumerator DamagedAnimation (){
		float a=0;
		float t=2f/30;
		damagedAnimation = true;
		while(currentHP<maxHP && !isDestroyed){
			spriteRenderer.color = new Color (Color.white.r + (standartColor.r - Color.white.r) * ((Mathf.Cos (a) + 1) / 2),
				Color.white.g + (standartColor.g - Color.white.g) * ((Mathf.Cos (a) + 1) / 2),
				Color.white.b + (standartColor.b - Color.white.b) * ((Mathf.Cos (a) + 1) / 2),1F);
			a += 360F/30;
			if (currentHP == 3)
				t = 2f/30;
			if (currentHP == 2)
				t = 1f/30;
			if (currentHP == 1)
				t = 0.5f/30;
			yield return new WaitForSecondsRealtime (t);
		}
		damagedAnimation = false;
		spriteRenderer.color = standartColor;
	}

	void DestroyItself(){
		isDestroyed = true;
		spriteRenderer.enabled = false;
		lifeOrbsManager.CheckToDeactivate (effect);
	}

	public void ReceiveEffect (Glossary.Effect e, Color c){
		effect = e;
		ChangeColor (c);
	}

	public Glossary.Effect GetEffect (){
		return effect;
	}

	public void Initialize(Color newColor, LifeOrbs lom){
		currentHP = maxHP;
		spriteRenderer = GetComponent<SpriteRenderer> ();
		ChangeColor (newColor);
		lifeOrbsManager = lom;
	}

	public void ChangeColor (Color newColor){
		spriteRenderer.color = newColor;
		standartColor = newColor;
	}
}