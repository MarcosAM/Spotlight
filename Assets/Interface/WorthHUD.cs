using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorthHUD : MonoBehaviour {

	public SpriteRenderer leftBall;
	public SpriteRenderer middleBall;
	public SpriteRenderer rightBall;
	public SpriteRenderer upBall;

	public void RefreshWorthHUD (int worth){
		if(worth == 1){
			leftBall.enabled = false;
			middleBall.enabled = true;
			rightBall.enabled = false;
			upBall.enabled = false;
			return;
		}
		if(worth == 2){
			leftBall.enabled = true;
			middleBall.enabled = false;
			rightBall.enabled = true;
			upBall.enabled = false;
			return;
		}
		if(worth == 3){
			leftBall.enabled = true;
			middleBall.enabled = true;
			rightBall.enabled = true;
			upBall.enabled = false;
			return;
		}
		if(worth == 4){
			leftBall.enabled = true;
			middleBall.enabled = true;
			rightBall.enabled = true;
			upBall.enabled = true;
			return;
		}
	}
}
