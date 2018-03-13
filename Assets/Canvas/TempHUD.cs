using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempHUD : MonoBehaviour {

	Avatar avatar;
	Image image;
	RectTransform rectTransform;

	void Start () {
		image = GetComponent<Image> ();
		rectTransform = GetComponent<RectTransform> ();
	}

	//TODO
	//Tirar isso do update e virar uma função

	void Update () {
		image.fillAmount = avatar.myGun.temperature / avatar.myGun.maxTemperature;
		rectTransform.position = avatar.transform.position;
	}

	public void SetAvatar(Avatar a){
		avatar = a;
	}
}