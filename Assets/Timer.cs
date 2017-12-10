﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	Text text;
	int time=100;

	void Start () {
		text = GetComponentInChildren<Text>();
		StartCoroutine("Countdown");
	}
	
	IEnumerator Countdown(){
		for(int i = time; i>0;i--){
			time --;
			text.text = time.ToString();
			yield return new WaitForSecondsRealtime(1f);
		}
		print ("Acabou");
	}
}
