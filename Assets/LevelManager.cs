using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public PlayerController[] Controllers;

	void Start () {
		DontDestroyOnLoad(gameObject);
	}
	
	void Update () {
		
	}

	public void NextLevel (){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void BackGame (){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
	}
}
