using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDTemperature : MonoBehaviour {

	float maxSize;
	Color OverheatColor;
	public SpriteRenderer spriteRenderer;

	void Awake (){
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Start (){
		maxSize = transform.localScale.x;
//		OverheatColor = spriteRenderer.color;
		OverheatColor = Color.red;
	}

	public void AjustSize (float currentTemperature, float maxTemperature){
		float temperaturePercentage = (currentTemperature/maxTemperature);
		transform.localScale = new Vector3(temperaturePercentage*maxSize,transform.localScale.y,transform.localScale.z);
		spriteRenderer.color = new Color(OverheatColor.r,OverheatColor.g,OverheatColor.b,temperaturePercentage*OverheatColor.a);
	}
}
