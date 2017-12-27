using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	[HideInInspector]public bool isControllingAvatar=true;
	public GameObject gameObjectAvatar;
	[HideInInspector]public Avatar avatar;

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
		avatar = gameObjectAvatar.GetComponent<Avatar>();
		avatar.inputManager = this;
	}
	
	void Update () {
		if(isControllingAvatar){
			if (avatar.state == Glossary.AvatarStates.Stunned)
				return;
			avatar.moveActions.RunOrAim(new Vector2 (Input.GetAxis (LHorizontal), Input.GetAxis (LVertical)),new Vector2 (Input.GetAxis (RHorizontal), Input.GetAxis (RVertical)));
			if(Input.GetButtonDown(Fire))
				avatar.myGun.StartCoroutine("FireBtnDown");
			if(Input.GetButtonUp(Fire))
				avatar.myGun.FireBtnUp();
			if(Input.GetButtonDown(Dash))
				avatar.moveActions.StartCoroutine("DashBtnDown");
			if(Input.GetButtonUp(Dash))
				avatar.moveActions.DashBtnUp();
		}
	}
}
