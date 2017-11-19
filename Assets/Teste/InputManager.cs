using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	public bool BattleOn;
	public GameObject gameObjectAvatar;
	[HideInInspector]public Avatar myAvatar;

//	DEFINIÇÂO DO CONTROLE - pt.1
	public int number;
	#region
	string LHorizontal;
	string LVertical;
	string RHorizontal;
	string RVertical;
	string Fire;
	string Dash;
	#endregion

	void Start () {
//	DEFINIÇÃO DO CONTROLE - pt.2
		#region
		LHorizontal = "LHorizontal_P"+number;
		LVertical = "LVertical_P"+number;
		RHorizontal = "RHorizontal_P"+number;
		RVertical = "RVertical_P"+number;
		Fire = "Fire_P"+number;
		Dash = "Dash_P"+number;
		#endregion
		myAvatar = gameObjectAvatar.GetComponent<Avatar>();
	}
	
	void Update () {

		if(BattleOn){
			myAvatar.LeftStick(new Vector2 (Input.GetAxis (LHorizontal), Input.GetAxis (LVertical)));
			myAvatar.RightStick (new Vector2 (Input.GetAxis (RHorizontal), Input.GetAxis (RVertical)));

			if(Input.GetButtonDown(Fire))
				myAvatar.FireBtnDown();
			if(Input.GetButtonUp(Fire))
				myAvatar.FireBtnUp();
			if(Input.GetButtonDown(Dash))
				myAvatar.DashBtnDown();
			if(Input.GetButtonUp(Dash))
				myAvatar.DashBtnUp();
		}
	}
}
