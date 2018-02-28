using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temperatures : MonoBehaviour {

	public Temperature temperaturePrefab;
	Canvas canvas;

	void Start(){
		canvas = FindObjectOfType<Canvas> ();
		StartTemperatureHUD ();
	}

	public void StartTemperatureHUD(){
		Avatar[] avatars = FindObjectsOfType<Avatar> ();
		for (int i = 0;i<avatars.Length;i++){
			Temperature temperature = Instantiate (temperaturePrefab, canvas.transform);
			temperature.SetAvatar (avatars[i]);
//			temperature.avatar = avatars [i];
		}
	}
}
