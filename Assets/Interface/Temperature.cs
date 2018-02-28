using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Temperature : MonoBehaviour {

	public Avatar avatar;
	public Image image;
	RectTransform rectTransform;

	void Start () {
		image = GetComponent<Image> ();
		rectTransform = GetComponent<RectTransform> ();
	}
	
	void Update () {
		image.fillAmount = avatar.myGun.temperature / avatar.myGun.maxTemperature;
		rectTransform.position = avatar.transform.position + avatar.transform.up*1.5f;
	}

	public void SetAvatar(Avatar a){
		avatar = a;
	}
}