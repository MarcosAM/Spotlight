using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbManager : MonoBehaviour {

	public Orb orbPrefab;
	public float timeToSpawnNew;
	public int orbMax;
	public Orb[] orbsInGame;

	void Start () {
		orbsInGame = new Orb[orbMax];
		StartCoroutine (SpawnOrbWithTime(timeToSpawnNew));
	}

	IEnumerator SpawnOrbWithTime (float time){
		while (1>0){
			yield return new WaitForSecondsRealtime (time);
			orbsInGame = FindObjectsOfType<Orb> ();
			if(FindObjectsOfType<Orb>().Length < orbMax){
				Orb orb = Instantiate (orbPrefab, transform.position,Quaternion.identity,transform);
				orb.GetComponentInChildren<Zone> ().RandomizeAndActive ();
			}
		}
	}
}