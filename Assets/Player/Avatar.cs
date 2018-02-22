using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : MonoBehaviour {

	public bool isStealing = false;

	[HideInInspector]public InputManager inputManager;
	[HideInInspector]public MoveActions moveActions;
	[HideInInspector]public Gun myGun;
	[HideInInspector]public Glossary.AvatarStates state = Glossary.AvatarStates.Normal;
	[HideInInspector]public SpriteRenderer spriteRenderer;
	[HideInInspector]public Orb orb;
	[HideInInspector]public WorthHUD worthHUD;
	[HideInInspector]public Color originalColor;
	[HideInInspector]public Shield shield;
	[HideInInspector]public bool isShielded = false;
	[HideInInspector]public Vector2 currentColliderSize;

	public float currentLife=5;
	public float maxLife=5;
	public float stunDuration = 0.5f;
	[HideInInspector]public bool isDying = false;

	[HideInInspector]public bool didStole = false;

	[HideInInspector]public int victoryPoints=0;
	[HideInInspector]public int myWorth=1;
	[HideInInspector]public int position=4;

	public VPIcon vpIconPrefab;

	void Awake(){
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		shield = GetComponentInChildren<Shield> ();
		shield.gameObject.SetActive(false);
	}

	void Start(){
		moveActions = GetComponent<MoveActions>();
		myGun = GetComponentInChildren<Gun>();
//		worthHUD = GetComponentInChildren<WorthHUD>();
//		worthHUD.RefreshWorthHUD(myWorth);
		originalColor = spriteRenderer.color;
		currentColliderSize = Vector2.one;
	}

	void OnTriggerStay2D (Collider2D c){
		if(c.GetComponent<Wall>()){
			moveActions.DashBtnUp();
		}
		if(!didStole){
			if (c.GetComponent<Avatar> ()) {
				if(c.GetComponent<Avatar>().state == Glossary.AvatarStates.Normal){
					StealAndSwitch(c.GetComponentInChildren<Avatar>());
				}
				if(c.GetComponent<Avatar>().state == Glossary.AvatarStates.Charging){
					StealAndSwitch(c.GetComponentInChildren<Avatar>());
				}
			}
			if(c.GetComponent<Catchable>() && !c.GetComponent<Catchable>().orb.isFollowing && state == Glossary.AvatarStates.Dashing){
				CatchOrSwitch(c.GetComponent<Catchable>().orb);
			}
		}
	}

	void OnCollisionStay2D(Collision2D c){
		if(c.gameObject.GetComponent<Avatar>() && state == Glossary.AvatarStates.Assaulting){
			c.gameObject.GetComponent<Avatar> ().ReduceLifeBy (6,GetComponent<Avatar>());
			c.gameObject.GetComponent<Avatar> ().StartCoroutine("StartStunned");
			c.gameObject.GetComponent<MoveActions>().rigidBody2D.AddForce(moveActions.rigidBody2D.velocity*60);
		}
	}

	public IEnumerator StartStunned(){
		if(state != Glossary.AvatarStates.Stunned){
			state = Glossary.AvatarStates.Stunned;
			yield return new WaitForSecondsRealtime(stunDuration);
		}
		if(state == Glossary.AvatarStates.Stunned)
			state = Glossary.AvatarStates.Normal;
	}

	public IEnumerator StartStunned(float duration){
		if(state != Glossary.AvatarStates.Stunned){
			state = Glossary.AvatarStates.Stunned;
			yield return new WaitForSecondsRealtime(duration);
		}
		if(state == Glossary.AvatarStates.Stunned)
			state = Glossary.AvatarStates.Normal;
	}

	void StealAndSwitch (Avatar theirAvatar)
	{
		myGun.EndOverheat();
		theirAvatar.myGun.Overheat();
		moveActions.canDash = true;
		if (theirAvatar.moveActions.canDash) {
			theirAvatar.moveActions.canDash = false;
			theirAvatar.moveActions.StartCoroutine("RechargeDash");
		}
		theirAvatar.StartStunned (0.3f);

		if(isStealing){
			Orb theirOrb;
			Orb myOrb;
			theirOrb = theirAvatar.orb;
			myOrb = orb;
			if (theirOrb != null || myOrb != null) {
				orb = theirOrb;
				theirAvatar.orb = myOrb;
				if (orb != null) {
					orb.Follow (GetComponent<Avatar> ());
				}
				if (theirAvatar.orb != null) {
					theirAvatar.orb.Follow(theirAvatar);
				}
			}
			didStole = true;	
		}
	}

	void CatchOrSwitch (Orb otherOrb)
	{
		if(isStealing){
			if(orb != null){
				orb.Release();
				orb = otherOrb;
				orb.Follow(GetComponent<Avatar>());
			}
			else {
				orb = otherOrb;
				otherOrb.Follow(GetComponent<Avatar>());
			}
			didStole = true;
		}
	}

	public void ReduceLifeBy (float damage, Avatar enemy)
	{
		currentLife -= damage;
		if (currentLife <= 0) {
			if(enemy != this){
				enemy.victoryPoints += myWorth;
				VPIcon vp = Instantiate (vpIconPrefab,transform.position,Quaternion.identity);
				vp.Initialize (0.3f,8f,enemy.spriteRenderer.color);
			}
			StartCoroutine("Die");
		}
	}

	public IEnumerator Die ()
	{
		if (orb != null) {
			orb.Release();
			orb = null;
		}
		FindObjectOfType<ScoreKeeper>().RefreshGameState();
		Refresh();
		transform.position = new Vector3(300,300,transform.position.z);
		inputManager.isControllingAvatar = false;
		yield return new WaitForSecondsRealtime(5);
		inputManager.isControllingAvatar = true;
		SpawnPoint[] spawnPoints = FindObjectsOfType<SpawnPoint>();
		int n = spawnPoints.Length;
		int r = Random.Range(0,n);
		transform.position = spawnPoints[r].gameObject.transform.position;
	}

	public void Refresh(){
		currentLife = maxLife;
		myGun.ResetGun();
		moveActions.canDash = true;
		state = Glossary.AvatarStates.Normal;
		didStole = false;
		ShieldDown ();	
	}

	public IEnumerator FlashColor(Color newColor, float time){
		float i = 0;
		float t = time / 10f;
		while (1>0){
			spriteRenderer.color = new Color (newColor.r + (originalColor.r - newColor.r) * ((Mathf.Cos (i) + 1) / 2),
												newColor.g + (originalColor.g - newColor.g) * ((Mathf.Cos (i) + 1) / 2),
												newColor.b + (originalColor.b - newColor.b) * ((Mathf.Cos (i) + 1) / 2),1F);
			i += 360F / 10F;
			yield return new WaitForSecondsRealtime (t);
		}
	}

	public void StopFlashColor (){
		StopCoroutine ("FlashColor");
		spriteRenderer.color = originalColor;
	}
		
	public void ShieldUp(){
		if(!isShielded){
			isShielded = true;
			shield.gameObject.SetActive (true);
		}
	}

	public void ShieldDown(){
		if(isShielded){
			isShielded = false;
			shield.gameObject.SetActive (false);
		}
	}
}