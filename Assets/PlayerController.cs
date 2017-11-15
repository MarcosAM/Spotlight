using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public int number;
	public GameObject avatar;
	public Movement avatarMovement;
	public Weapon avatarWeapon;

	string LHorizontal;
	string LVertical;
	string RHorizontal;
	string RVertical;
	string Fire;
	string Dash;

	public bool isActive = true;

	Vector2 LStick;

	void Start ()
	{
		DontDestroyOnLoad(gameObject);
		LHorizontal = "LHorizontal_P"+number;
		LVertical = "LVertical_P"+number;
		RHorizontal = "RHorizontal_P"+number;
		RVertical = "RVertical_P"+number;
		Fire = "Fire_P"+number;
		Dash = "Dash_P"+number;

		avatarMovement = avatar.GetComponent<Movement>();
		avatarWeapon = avatar.GetComponent<Weapon>();

		avatar.SetActive(false);
	}
	
	void Update (){
		
		LStick = new Vector2 (Input.GetAxis (LHorizontal), Input.GetAxis (LVertical));

		if (Input.GetButtonDown (Dash) && LStick != Vector2.zero && avatarMovement.dashesAvailable > 0) {
			avatarMovement.isDashing = true;
			avatarMovement.dashingDirection = LStick;
			avatarMovement.currentDashingDuration = 0;
			avatarMovement.dashesAvailable--;
		}

		if (Input.GetButtonUp (Dash)) {
			avatarMovement.isDashing = false;
		}

		float angle = Mathf.Atan2(Input.GetAxisRaw(RHorizontal), Input.GetAxisRaw(RVertical)) * Mathf.Rad2Deg;
		if(Input.GetAxis(RHorizontal) != 0f || Input.GetAxis(RVertical) != 0f)
			avatarMovement.RotateBy(angle);

		avatarMovement.LStick = LStick;
		if(!avatarMovement.isDashing && Input.GetButtonDown(Fire)){
			avatarWeapon.Shoot();
		}
	}

	public void ActiveDeactive (){
		if(isActive)
			isActive = false;
		else
			isActive = true;
	}
}