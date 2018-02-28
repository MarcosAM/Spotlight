using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetHUDTest : MonoBehaviour {

	public GameObject Obj;
	public Avatar avatar;

	public Camera mCamera;
	private RectTransform rt;
	Image image;

	void Start ()
	{
		rt = GetComponent<RectTransform>();
		mCamera = Camera.main;
		avatar = Obj.GetComponentInParent<Avatar> ();
		image = GetComponent<Image> ();
		image.color = avatar.spriteRenderer.color;
	}

	void Update ()
	{
		if (Obj != null)
		{
			image.fillAmount = avatar.myGun.temperature / avatar.myGun.maxTemperature;
			rt.position = Obj.transform.position;
		}
	}
}
