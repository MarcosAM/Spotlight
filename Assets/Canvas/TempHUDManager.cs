using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempHUDManager : MonoBehaviour {

	public TempHUD temperaturePrefab;
	[HideInInspector] Canvas canvas;

	void Start(){
		canvas = GetComponent<Canvas>();
		StartTemperatureHUD ();
	}

	public void StartTemperatureHUD(){
		Avatar[] avatars = FindObjectsOfType<Avatar> ();
		for (int i = 0;i<avatars.Length;i++){
			TempHUD temperature = Instantiate (temperaturePrefab, canvas.transform);
			temperature.SetAvatar (avatars[i]);
		}
	}
}
