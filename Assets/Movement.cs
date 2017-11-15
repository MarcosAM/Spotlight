using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	public float walkingSpeed;
	public float dashingSpeed;
	public float dashingDuration;
	public float currentDashingDuration;
	public float dashesAvailable = 1;
	public float rechargeDashTime = 0.3f;
	public float countdownToRechargeDash = 0;
	public bool isDashing;
	public Vector2 dashingDirection;

	private Rigidbody2D myRigidbody2D;

	public Vector2 LStick;
	public Vector2 moveVelocity;

	public bool isUsingController;

	float myAmmunition;


	void Start () {
		DontDestroyOnLoad(gameObject);
		myRigidbody2D = GetComponent<Rigidbody2D>();
	}
	
	void Update ()
	{
		if (isDashing) {
			GetComponent<BoxCollider2D>().isTrigger = true;
			currentDashingDuration += Time.deltaTime;
			if (currentDashingDuration >= dashingDuration)
				isDashing = false;
		}

		if (!isDashing) {
			GetComponent<BoxCollider2D>().isTrigger = false;
			moveVelocity = LStick * walkingSpeed;
			if (dashesAvailable <= 0) {
				countdownToRechargeDash += Time.deltaTime;
				if (countdownToRechargeDash >= rechargeDashTime) {
					dashesAvailable++;
					countdownToRechargeDash = 0;
				}
			}
		} else{
			moveVelocity = dashingDirection * dashingSpeed;
		}
	}

	void FixedUpdate (){
		myRigidbody2D.velocity = moveVelocity;
	}

	void OnTriggerEnter2D(Collider2D c){
		if (c.GetComponent<Movement> () == null)
			isDashing = false;
		else {
			if(isDashing){
				float myAmmunition = GetComponent<Weapon> ().currentAmmunition;
				float theirAmmunition = c.GetComponent<Weapon> ().currentAmmunition;
				GetComponent<Weapon> ().currentAmmunition = theirAmmunition;
				c.GetComponent<Weapon> ().currentAmmunition = myAmmunition;
				dashesAvailable++;
				if (c.GetComponent<Movement> ().dashesAvailable <= 0) {
					countdownToRechargeDash = -rechargeDashTime * 2;
				} else {
					c.GetComponent<Movement> ().dashesAvailable--;
				}
			}
		}
	}

	public void RotateBy (float angle){
		transform.rotation = Quaternion.Euler(new Vector3(0,0,angle));
	}
}

////		ROTATE WITH MOUSE
//		#region
//		if(!isUsingController){
//			Vector2 lookAtVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
//			float angle = Mathf.Rad2Deg * Mathf.Atan2(lookAtVector.y, lookAtVector.x);
//			Quaternion newRotation = Quaternion.AngleAxis(angle, Vector3.forward);
//			transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 300);
//		}
//		#endregion