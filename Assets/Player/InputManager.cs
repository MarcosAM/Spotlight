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

	Vector2 lastAxis;

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

			float LAxisX = Input.GetAxisRaw (LHorizontal);
//			LAxisX = (float)(((int)(LAxisX * 1000)) / 1000);

			float LAxisY = Input.GetAxisRaw (LVertical);
//			LAxisY = (float)(((int)(LAxisY * 1000)) / 1000);

			Vector2 LAxis = new Vector2 (LAxisX, LAxisY).normalized;

			avatar.moveActions.RunOrAim(LAxis,new Vector2 (Input.GetAxisRaw (RHorizontal), Input.GetAxisRaw (RVertical)));
//			avatar.moveActions.RunOrAim(Vector2.Lerp(lastAxis,new Vector2 (Input.GetAxis (LHorizontal), Input.GetAxis (LVertical)),0.01f) ,new Vector2 (Input.GetAxis (RHorizontal), Input.GetAxis (RVertical)));
//			lastAxis = new Vector2(Input.GetAxis (LHorizontal), Input.GetAxis (LVertical));
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
