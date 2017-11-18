using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour {

	public float timer;
	public bool BattleOn;
	public PlayerController[] activePlayerControllers;

	int playersInTheFight;

	void Start () {
		PlayerController[] playerControllers = FindObjectsOfType<PlayerController>();
		SpawnPoint[] spawnPoints = FindObjectsOfType<SpawnPoint>();

		int ii= 0;
		for(int i = 0; i < 4 ;i++){
			if(playerControllers[i].isActive){
				playerControllers[i].avatar.transform.position = spawnPoints[i].transform.position;
				playerControllers[i].avatar.SetActive(true);
				ii++;
			}
		}

		activePlayerControllers = new PlayerController[ii];

		ii = 0;
		for(int i = 0; i < 4 ;i++){
			if(playerControllers[i].isActive){
				activePlayerControllers[ii] = playerControllers[i];
				ii++;
			}
		}

		foreach(PlayerController playerController in activePlayerControllers){
			playerController.avatarLife.position = activePlayerControllers.Length;
		}
		timer = 100;
	}
	
	void Update () {
		timer -= Time.deltaTime;
	}

	public void RefreshAvatarPositions(){

		int l = activePlayerControllers.Length;
		for (int i = 0;i <l;i++){
			activePlayerControllers[i].avatarLife.position = l;
			for(int j = 0; j < l; j++){
				if(i != j && activePlayerControllers[i].avatarLife.points > activePlayerControllers[j].avatarLife.points){
					activePlayerControllers[i].avatarLife.position--;
				}
			}
		}
	}
}
