using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	public float walkingSpeed;
	public float dashingSpeed;
	public float dashingDuration;
	public float currentDashingDuration;
	public bool isDashing;
	public Vector2 dashingDirection;

	private Rigidbody2D myRigidbody2D;

	public Vector2 LStick;
	public Vector2 moveVelocity;

	public bool isUsingController;

	void Start () {
		myRigidbody2D = GetComponent<Rigidbody2D>();
	}
	
	void Update ()
	{
		if (isDashing) {
			currentDashingDuration += Time.deltaTime;
			if (currentDashingDuration >= dashingDuration)
				isDashing = false;
		}

		if (!isDashing) {
			moveVelocity = LStick * walkingSpeed;
		} else{
			moveVelocity = dashingDirection * dashingSpeed;
		}
	}

	void FixedUpdate (){
		myRigidbody2D.velocity = moveVelocity;
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