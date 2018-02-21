using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour {

	public enum Zones : short {Poison, Piercing, Cooldown, Triple, Bounce, Explosive, Shield};

	public Zones zone;
	Zones newZone;
	public float size;

	Color color;
	Color newColor;

	public float coolDownTime = 0.1f;
	public float bulletSize;
	public float bulletSpeed;

	public bool doesChange;
	public float timeToChange = 8f;

	SpriteRenderer spriteRenderer;

	void Start ()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		if (doesChange) {
			zone = (Zones)Random.Range (0, 7);
			RefreshColor ();
			StartCoroutine("ChangeZoneWithTime");
		} else {
			RefreshColor();
		}
		transform.localScale = new Vector3(size,size,size);
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
				newColor = new Color (84f/255f, 253f/255f, 84f/255f, 147f/255f);
			if (newZone == Zones.Piercing) 
				newColor = new Color (253f/255f, 13f/255f, 253f/255f, 147f/255f);
			if (newZone == Zones.Cooldown)
				newColor = new Color (84f/255f, 84f/255f, 253f/255f, 147f/255f);
			if (newZone == Zones.Triple)
				newColor = new Color (253f/255f, 84f/255f, 84f/255f, 147f/255f);
			if(newZone == Zones.Bounce)
				newColor = new Color (253f/255f, 253f/255f, 13f/255f, 147f/255f);
			if(newZone == Zones.Explosive)
				newColor = new Color (253f/255f, 253f/255f, 84f/255f, 147f/255f);
			if (newZone == Zones.Shield)
				newColor = new Color (84f/255f, 225/255f, 253f/255f, 147f/255f);


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
			spriteRenderer.color = new Color (84f/255f, 253f/255f, 84f/255f, 147f/255f);
			color = GetComponent<SpriteRenderer> ().color;
		}
		if (zone == Zones.Piercing) {
			spriteRenderer.color = new Color (253f/255f, 13f/255f, 253f/255f, 147f/255f);
			color = GetComponent<SpriteRenderer> ().color;
		}
		if (zone == Zones.Cooldown) {
			spriteRenderer.color = new Color (84f/255f, 84f/255f, 253f/255f, 147f/255f);
			color = GetComponent<SpriteRenderer> ().color;
		}
		if (zone == Zones.Triple){
			spriteRenderer.color = new Color (253f/255f, 84f/255f, 84f/255f, 147f/255f);
			color = GetComponent<SpriteRenderer> ().color;
		}
		if(zone == Zones.Bounce){
			spriteRenderer.color = new Color (253f/255f, 84f/255f, 13f/255f, 147f/255f);
			color = GetComponent<SpriteRenderer> ().color;
		}
		if (zone == Zones.Explosive) {
			spriteRenderer.color = new Color (253f/255f, 253f/255f, 84f/255f, 147f/255f);
			color = GetComponent<SpriteRenderer> ().color;
		}
		if(zone == Zones.Shield){
			spriteRenderer.color = new Color (84f/255f, 225/255f, 253f/255f, 147f/255f);
			color = GetComponent<SpriteRenderer> ().color;
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
}
