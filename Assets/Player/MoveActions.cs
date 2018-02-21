using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveActions : MonoBehaviour {

	public float runningSpeed = 9;
	[HideInInspector]public float currentSpeed;

	[HideInInspector]public bool canDash = true;
	public float dashDuration = 0.3f;
	public float rechargeTime;

	Avatar avatar;
	[HideInInspector]public Rigidbody2D rigidBody2D;
	[HideInInspector]public Vector2 velocity;
	[HideInInspector]public DashParticles dashParticles;
	[HideInInspector]public BoxCollider2D boxCollider2D;
	Transform movementTransform;

	void Start(){
		rigidBody2D = GetComponent<Rigidbody2D>();
		avatar = GetComponent<Avatar>();
		dashParticles = GetComponentInChildren<DashParticles>();
		boxCollider2D = GetComponent<BoxCollider2D>();
		movementTransform = new GameObject ().transform;
		movementTransform.rotation = transform.rotation;
		currentSpeed = runningSpeed;
	}

	void FixedUpdate (){
		if(avatar.state != Glossary.AvatarStates.Stunned)
			rigidBody2D.velocity = velocity;
	}

	public void RunOrAim (Vector2 lStick, Vector2 rStick)
	{
		if (avatar.state == Glossary.AvatarStates.Normal) {
			if (rStick != Vector2.zero)
				LookAt (rStick.x, rStick.y);
			else 
				LookAt (lStick.x,-lStick.y);

			MoveTo (lStick.x,-lStick.y);
			velocity = movementTransform.up* -1 * currentSpeed;

			float intensity;
			if (Mathf.Abs (lStick.x) > Mathf.Abs (lStick.y)) {
				intensity = Mathf.Abs (lStick.x);
			} else {
				intensity = Mathf.Abs (lStick.y);
			}
			velocity = velocity * intensity;
		}
	}

	public void LookAt (float x, float y){
		float angle = Vector2.Angle(Vector2.up,new Vector2(x,y));
		if (x < 0) {
			angle *= -1;
		}
		if (x != 0f || y != 0f) {
			transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(new Vector3(0,0,angle)),Time.deltaTime*10);
		}
	}

	public void MoveTo (float x, float y){
		float angle = Vector2.Angle(Vector2.up,new Vector2(x,y));
		if (x < 0) {
			angle *= -1;
		}
		if (x != 0f || y != 0f) {
			movementTransform.rotation = Quaternion.Lerp(movementTransform.rotation,Quaternion.Euler(new Vector3(0,0,angle)),Time.deltaTime*10);
		}
	}

	public void DashBtnUp (){

		if(avatar.state == Glossary.AvatarStates.Dashing){
			avatar.state = Glossary.AvatarStates.Normal;
			boxCollider2D.isTrigger = false;
			boxCollider2D.size = avatar.currentColliderSize;
			dashParticles.StopDash();
			StartCoroutine("RechargeDash");
			avatar.didStole = false;
		}
		StopCoroutine("DashBtnDown");
	}

	public IEnumerator DashBtnDown(){
		if (avatar.state == Glossary.AvatarStates.Normal && canDash) {
			Dash();
			avatar.state = Glossary.AvatarStates.Dashing;
			boxCollider2D.isTrigger = true;
			boxCollider2D.size = new Vector2(2,2);
			dashParticles.StartDash();
			yield return new WaitForSecondsRealtime (dashDuration);
		}
		DashBtnUp();
	}

	public IEnumerator RechargeDash (){
		yield return new WaitForSecondsRealtime(rechargeTime);
		if(!canDash){
			canDash = true;
		}
	}

	public void Dash ()
	{
		if (canDash) {
			velocity = velocity*3f;
			canDash = false;
		}
	}
}
