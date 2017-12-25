using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHUD : MonoBehaviour {

	public int position;

	public Text text;
	public Image image;

	public Avatar avatar;
	
	void Start () {
		text = GetComponentInChildren<Text>();
		image = GetComponentInChildren<Image>();
	}

	void Update (){
		Refresh();
	}

	public void Refresh ()
	{
		text.text = avatar.victoryPoints.ToString ();
		image.color = avatar.spriteRenderer.color;
		if (avatar.position == 1)
			text.fontSize = 50;
		if(avatar.position == 2)
			text.fontSize = 40;
		if(avatar.position == 3)
			text.fontSize = 30;
		if(avatar.position == 4)
			text.fontSize = 20;
	}
}
