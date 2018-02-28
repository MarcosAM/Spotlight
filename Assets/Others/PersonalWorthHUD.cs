using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonalWorthHUD : MonoBehaviour {

	Text text;
	public Avatar avatar;
	RectTransform rectTransform;

	void Awake(){
		text = GetComponent<Text> ();
		rectTransform = GetComponent<RectTransform> ();
	}

	void Update(){
		rectTransform.position = avatar.transform.position;
	}

	public void Refresh (string value){
		text.text = value;
	}
}
