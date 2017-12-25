using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour {

	public Avatar[] players = new Avatar[4];


	public void RefreshPlayersWorth(){
		for(int i = 0;i<4;i++){
			players[i].position = 4;
			for(int j = 0; j<4;j++){
				if(players[i].victoryPoints > players[j].victoryPoints){
					players[i].position --;
				}
			}
		}
		for(int l= 0;l<4;l++){
			players[l].myWorth = 5-players[l].position;
		}
	}
}
