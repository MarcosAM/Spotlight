using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour {

	void Start () {
		PlayerController[] playerControllers = FindObjectsOfType<PlayerController>();
		SpawnPoint[] spawnPoints = FindObjectsOfType<SpawnPoint>();

		for(int i = 0; i < 4 ;i++){
			if(playerControllers[i].isActive){
				playerControllers[i].avatar.transform.position = spawnPoints[i].transform.position;
				playerControllers[i].avatar.SetActive(true);
			}
		}
	}
	
	void Update () {
		
	}
}
