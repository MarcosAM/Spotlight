using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHUD : MonoBehaviour {

	Text myText;
	Life myLife;
	public int number;

	void Start () {
		myText = GetComponent<Text>();
		PlayerController[] playerControllers = FindObjectsOfType<PlayerController> ();
		foreach (PlayerController controller in playerControllers) {
			if(controller.number == number){
				myLife = controller.avatar.GetComponent<Life> ();
			}
		}
	}
	
	void Update () {
		myText.text = myLife.points.ToString();
	}
}
