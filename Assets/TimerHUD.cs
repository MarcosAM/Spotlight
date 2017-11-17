using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerHUD : MonoBehaviour {

	FightManager fightManager;
	Text myText;

	void Start () {
		fightManager = FindObjectOfType<FightManager> ();
		myText = GetComponent<Text> ();
	}
	
	void Update () {
		myText.text = fightManager.timer.ToString("0");
	}
}
