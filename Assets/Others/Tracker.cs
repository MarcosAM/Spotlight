using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tracker : MonoBehaviour {

	Slider slider;
	public Avatar avatar;
	ScoreKeeper scoreKeeper;
	public Image fillImage;
	public Image handleImage;
	
	void Start () {
		scoreKeeper = FindObjectOfType<ScoreKeeper>();
		slider = GetComponent<Slider>();
		slider.value = avatar.victoryPoints/scoreKeeper.vpointsToWin;
		fillImage.color = avatar.spriteRenderer.color;
		handleImage.color = avatar.spriteRenderer.color;
		StartCoroutine("RefreshValue");
	}

	IEnumerator RefreshValue ()
	{
		while (1!=0) {
			slider.value = avatar.victoryPoints/scoreKeeper.vpointsToWin;
			yield return new WaitForSecondsRealtime(0.5f);
		}
	}
}
