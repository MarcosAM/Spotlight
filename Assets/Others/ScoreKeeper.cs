using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreKeeper : MonoBehaviour {

	public float vpointsToWin;
	public Avatar[] players;
	int amountOfPlayers;

	public PanelKeeper panelKeepers;

	void Start (){
		DontDestroyOnLoad(gameObject);
		players = FindObjectsOfType<Avatar>();
		amountOfPlayers = players.Length;
	}

	public void RefreshGameState(){
		for(int i = 0;i<amountOfPlayers;i++){
			players[i].position = amountOfPlayers;
			for(int j = 0; j<amountOfPlayers;j++){
				if(players[i].victoryPoints > players[j].victoryPoints){
					players[i].position --;
				}
			}
		}
		for(int l= 0;l<amountOfPlayers;l++){
			players[l].myWorth = (amountOfPlayers+1)-players[l].position;
		}
		for(int m= 0;m<amountOfPlayers;m++){
			if(players[m].victoryPoints >= vpointsToWin){
				EndGame(players[m].victoryPoints,players[m].spriteRenderer.color, m);
				return;
			}
		}
	}

	void EndGame (int number, Color color, int n){

		int[] numbers = new int[4];
		Color[] colors = new Color[4];

		numbers[0] = number;
		colors[0] = color;

		int m = 1;
		for (int i = 0; i<4;i++){
			if(n!=i){
				numbers[m] = players[i].victoryPoints;
				colors[m] = players[i].spriteRenderer.color;
				m++;
			}
		}
		SceneManager.LoadScene("End Game");
		PanelKeeper panelKeeper = Instantiate(panelKeepers);
		panelKeeper.RefreshPanels(numbers[0],numbers[1],numbers[2],numbers[3],colors[0],colors[1],colors[2],colors[3]);
	}
}
