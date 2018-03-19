using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

	public Avatar[] players;
	int[] ranking;
	Color[] colors;
	int amountOfPlayers;

	public PanelKeeper panelKeepers;

	void Start (){
		DontDestroyOnLoad(gameObject);
		players = FindObjectsOfType<Avatar>();
		amountOfPlayers = players.Length;
		ranking = new int[players.Length];
		colors = new Color[players.Length];
	}

	public void CheckIfEnded (Avatar a)
	{
		for (int i = players.Length - 1; i >= 0; i--) {
			if (ranking [i] == 0) {
				ranking [i] = a.inputManager.number;
				colors [i] = a.spriteRenderer.color;
				if (i == 1) {
					foreach (Avatar p in players) {
						if (!p.isDead) {
							ranking [0] = p.inputManager.number;
							colors [0] = p.spriteRenderer.color;
						}
					}
				} else {
				return;
				}
			}
		}
		TempHUD[] tempHUDs = FindObjectsOfType<TempHUD>();
		foreach (TempHUD tHUD in tempHUDs){
			Destroy(tHUD.gameObject);
		}
		PanelKeeper panelKeeper = Instantiate(panelKeepers,transform);
		panelKeeper.RefreshPanels (ranking,colors);
		SceneManager.LoadScene("End Game");
	}
}
