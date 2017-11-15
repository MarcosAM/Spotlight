using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Btn_Players : MonoBehaviour {

	public Button button;
	public PlayerController playerController;

	public Color white = new Color (0.9f, 0.9f, 0.9f);
	public Color black = new Color (0.1f, 0.1f, 0.1f);
	public Color myColor;

	void Start () {
		button = GetComponent<Button>();
		if(playerController.number==1)
			myColor = new Color (1,0.35f,0.35f);
		if(playerController.number==2)
			myColor = new Color (0.35f,0.35f,1);
		if(playerController.number==3)
			myColor = new Color (0.35f,1,0.35f);
		if(playerController.number==4)
			myColor = new Color (1,1,0.35f);
	}
	
	void Update ()
	{
		if (playerController.isActive) {
			GetComponentInChildren<Text> ().color = white;
			GetComponent<Image>().color = myColor;
		} else {
			GetComponentInChildren<Text> ().color = myColor;
			GetComponent<Image>().color = black;
		}
		GetComponentInChildren<Text>().text = "P"+playerController.number;
//		if(playerController.isActive){
//			button.transit
//		}
	}
}
