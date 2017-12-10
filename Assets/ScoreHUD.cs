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

	public void Refresh (){
		text.text = avatar.victoryPoints.ToString();
		image.color = avatar.spriteRenderer.color;
	}
}
