﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour {

	public enum Zones : short {Poison, Piercing, Cooldown, Triple, Bounce, Explosive, Shield};

	public Zones zone;
	Zones newZone;
	public float size;

	Color color;
	Color newColor;

	[SerializeField]float HP = 18f;

	public float coolDownTime = 0.1f;
	public float bulletSize;
	public float bulletSpeed;

	public bool doesChange;
	public float timeToChange = 8f;

	Orb orb;
	public VPIcon vpIconPrefab;
	SpriteRenderer spriteRenderer;
	public Collider2D c2D;
	ScoreKeeper scoreKeeper;

	void Awake(){
		spriteRenderer = GetComponent<SpriteRenderer>();
		orb = GetComponentInParent<Orb> ();
		scoreKeeper = FindObjectOfType<ScoreKeeper> ();
	}

	void Start ()
	{
		if (doesChange) {
			RandomizeAndActive ();
			StartCoroutine("ChangeZoneWithTime");
		} else {
			RefreshColor();
		}
		transform.localScale = new Vector3(size,size,size);
		c2D = GetComponent<Collider2D> ();
		StartCoroutine (GiveVPs());
	}
	
	void OnTriggerEnter2D(Collider2D c){
		GiveEffect (c);
	}

	void OnTriggerExit2D(Collider2D c){
		if(c.GetComponentInChildren<Gun>()){
			if (zone == Zones.Poison) {
				c.GetComponentInChildren<Gun>().isPoisonous = false;
			}
			if (zone == Zones.Piercing) {
				c.GetComponentInChildren<Gun>().piercingZone = false;
			}
			if (zone == Zones.Cooldown) {
				c.GetComponentInChildren<Gun>().currentCoolDownTime = c.GetComponentInChildren<Gun>().coolDownTime;
			}
			if (zone == Zones.Triple){
				c.GetComponentInChildren<Gun>().hasTripleShot = false;
			}
			if(zone == Zones.Bounce){
				c.GetComponentInChildren<Gun>().doesBounce = false;
			}
			if(zone == Zones.Explosive){
				c.GetComponentInChildren<Gun>().isExplosive = false;
			}
			if(zone == Zones.Shield){
				c.GetComponent<Avatar> ().ShieldDown ();
			}
		}
	}

	IEnumerator ChangeZoneWithTime(){
		while(doesChange){
			yield return new WaitForSecondsRealtime(timeToChange);

			newZone = (Zones)Random.Range(0,7);

			if (newZone == Zones.Poison)
				newColor = new Color (84f/255f, 253f/255f, 84f/255f, 100f/255f);
			if (newZone == Zones.Piercing) 
				newColor = new Color (253f/255f, 13f/255f, 253f/255f, 100f/255f);
			if (newZone == Zones.Cooldown)
				newColor = new Color (84f/255f, 84f/255f, 253f/255f, 100f/255f);
			if (newZone == Zones.Triple)
				newColor = new Color (253f/255f, 84f/255f, 84f/255f, 100f/255f);
			if(newZone == Zones.Bounce)
				newColor = new Color (253f/255f, 253f/255f, 13f/255f, 100f/255f);
			if(newZone == Zones.Explosive)
				newColor = new Color (253f/255f, 253f/255f, 84f/255f, 100f/255f);
			if (newZone == Zones.Shield)
				newColor = new Color (84f/255f, 225/255f, 253f/255f, 100f/255f);


			float plusR = (newColor.r-color.r)*0.1f;
			float plusG = (newColor.g-color.g)*0.1f;
			float plusB = (newColor.b-color.b)*0.1f;
			float plusA = (newColor.a-color.a)*0.1f;
			for(int i = 1; i<=10;i++){
				yield return new WaitForSecondsRealtime(0.1f);
				spriteRenderer.color = new Color(spriteRenderer.color.r+plusR,spriteRenderer.color.g+plusG,spriteRenderer.color.b+plusB,spriteRenderer.color.a+plusA);
			}
			GetComponent<CircleCollider2D> ().offset = new Vector2 (100,0);
			yield return 0;
			zone = newZone;
			GetComponent<CircleCollider2D> ().offset = new Vector2 (0,0);
			RefreshColor();
		}
	}
	void RefreshColor (){
		if (zone == Zones.Poison) {
			spriteRenderer.color = new Color (84f/255f, 253f/255f, 84f/255f, 100f/255f);
			color = GetComponent<SpriteRenderer> ().color;
			orb.text.text = "VENENOSO";
		}
		if (zone == Zones.Piercing) {
			spriteRenderer.color = new Color (253f/255f, 13f/255f, 253f/255f, 100f/255f);
			color = GetComponent<SpriteRenderer> ().color;
			orb.text.text = "PERFURANTE";
		}
		if (zone == Zones.Cooldown) {
			spriteRenderer.color = new Color (84f/255f, 84f/255f, 253f/255f, 100f/255f);
			color = GetComponent<SpriteRenderer> ().color;
			orb.text.text = "METRALHADORA";
		}
		if (zone == Zones.Triple){
			spriteRenderer.color = new Color (253f/255f, 84f/255f, 84f/255f, 100f/255f);
			color = GetComponent<SpriteRenderer> ().color;
			orb.text.text = "TIRO TRIPLO";
		}
		if(zone == Zones.Bounce){
			spriteRenderer.color = new Color (253f/255f, 84f/255f, 13f/255f, 100f/255f);
			color = GetComponent<SpriteRenderer> ().color;
			orb.text.text = "RICOCHETEIA";
		}
		if (zone == Zones.Explosive) {
			spriteRenderer.color = new Color (253f/255f, 253f/255f, 84f/255f, 100f/255f);
			color = GetComponent<SpriteRenderer> ().color;
			orb.text.text = "EXPLOSIVA";
		}
		if(zone == Zones.Shield){
			spriteRenderer.color = new Color (84f/255f, 225/255f, 253f/255f, 100f/255f);
			color = GetComponent<SpriteRenderer> ().color;
			orb.text.text = "ESCUDO";
		}
	}

	void GiveEffect (Collider2D c){
		if(c.GetComponentInChildren<Gun>()){
			if (zone == Zones.Poison) {
				c.GetComponentInChildren<Gun>().isPoisonous = true;
			}
			if (zone == Zones.Piercing) {
				c.GetComponentInChildren<Gun>().piercingZone = true;
			}
			if (zone == Zones.Cooldown) {
				c.GetComponentInChildren<Gun>().currentCoolDownTime = coolDownTime;
			}
			if (zone == Zones.Triple){
				c.GetComponentInChildren<Gun>().hasTripleShot = true;
			}
			if(zone == Zones.Bounce){
				c.GetComponentInChildren<Gun>().doesBounce = true;
			}
			if(zone == Zones.Explosive){
				c.GetComponentInChildren<Gun>().isExplosive = true;
			}
			if(zone == Zones.Shield){
				c.GetComponent<Avatar> ().ShieldUp ();
			}
		}
	}

	void TakeEffectAway (Collider2D c){
		if(c.GetComponentInChildren<Gun>()){
			if (zone == Zones.Poison) {
				c.GetComponentInChildren<Gun>().isPoisonous = false;
			}
			if (zone == Zones.Piercing) {
				c.GetComponentInChildren<Gun>().piercingZone = false;
			}
			if (zone == Zones.Cooldown) {
				c.GetComponentInChildren<Gun>().currentCoolDownTime = c.GetComponentInChildren<Gun>().coolDownTime;
			}
			if (zone == Zones.Triple){
				c.GetComponentInChildren<Gun>().hasTripleShot = false;
			}
			if(zone == Zones.Bounce){
				c.GetComponentInChildren<Gun>().doesBounce = false;
			}
			if(zone == Zones.Explosive){
				c.GetComponentInChildren<Gun>().isExplosive = false;
			}
			if(zone == Zones.Shield){
				c.GetComponent<Avatar> ().ShieldDown ();
			}
		}
	}

	public void RandomizeAndActive(){
		int i=0;
		Zone[] zones = FindObjectsOfType<Zone> ();
		while (i < zones.Length) {
			i = 0;
			zone = (Zones)Random.Range (0, 7);
			foreach (Zone z in zones){
				if(zone != z.zone || this == z){
					i++;
				}
			}
		}
		RefreshColor ();
		GetComponentInParent<Orb>().transform.position = new Vector3 (Random.Range(-16.0F,16.0F),Random.Range(-4.0F,4.0F),GetComponentInParent<Orb>().transform.position.z);
	}

	IEnumerator GiveVPs(){
		int i;
		Avatar[] avatars = FindObjectsOfType<Avatar> ();
		while(1>0){
			RaycastHit2D[] hits = new RaycastHit2D[10];
			i = c2D.Cast(Vector2.zero, hits);
			if(i != 0){
				foreach (RaycastHit2D hit in hits){
					if(hit.collider != null){
						if(hit.collider.gameObject.GetComponent<Avatar>()){
							foreach (Avatar a in avatars){
								if(hit.collider.gameObject.GetComponent<Avatar>() == a){
									a.victoryPoints++;
									VPIcon vp = Instantiate (vpIconPrefab,a.transform.position,Quaternion.identity);
									vp.Initialize (0.3f,8f,a.spriteRenderer.color,1);
									scoreKeeper.RefreshGameState ();
//									HP--;
								}
							}
						}
					}
				}
				if(HP <=0){
					Zone[] zones = FindObjectsOfType<Zone> ();
					foreach (Avatar a in avatars) {
						if(c2D.IsTouching(a.GetComponent<Collider2D>())){
							bool hasSameZone = false;
							foreach (Zone z in zones){
								if(z.c2D.IsTouching(a.GetComponent<Collider2D>()) && z.zone == zone && z != this){
									hasSameZone = true;
								}
							}
							if(!hasSameZone){
								TakeEffectAway (a.GetComponent<Collider2D>());
							}
						}
					}
					Destroy (GetComponentInParent<Orb>().gameObject);
				}
			}
			yield return new WaitForSecondsRealtime (1.5f);
		}
	}
}
