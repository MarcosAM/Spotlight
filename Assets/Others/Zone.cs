using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour {

	public enum Zones : short {Damage, Piercing, Cooldown, Speed, Size};

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
			zone = (Zones)Random.Range (0, 5);
			RefreshColor ();
			StartCoroutine("ChangeZoneWithTime");
		} else {
			RefreshColor();
		}
		transform.localScale = new Vector3(size,size,size);
	}
	
	void OnTriggerEnter2D(Collider2D c){
		if(c.GetComponentInChildren<Gun>()){
			if (zone == Zones.Damage) {
				c.GetComponentInChildren<Gun>().damageZone = true;
			}
			if (zone == Zones.Piercing) {
				c.GetComponentInChildren<Gun>().piercingZone = true;
			}
			if (zone == Zones.Cooldown) {
				c.GetComponentInChildren<Gun>().currentCoolDownTime = coolDownTime;
			}
			if (zone == Zones.Size){
				c.GetComponentInChildren<Gun>().sizeZone = bulletSize;
			}
			if(zone == Zones.Speed){
				c.GetComponentInChildren<Gun>().speedZone = bulletSpeed;
			}
		}
	}

	void OnTriggerExit2D(Collider2D c){
		if(c.GetComponentInChildren<Gun>()){
			if (zone == Zones.Damage) {
				c.GetComponentInChildren<Gun>().damageZone = false;
			}
			if (zone == Zones.Piercing) {
				c.GetComponentInChildren<Gun>().piercingZone = false;
			}
			if (zone == Zones.Cooldown) {
				c.GetComponentInChildren<Gun>().currentCoolDownTime = c.GetComponentInChildren<Gun>().coolDownTime;
			}
			if (zone == Zones.Size){
				c.GetComponentInChildren<Gun>().sizeZone = 0;
			}
			if(zone == Zones.Speed){
				c.GetComponentInChildren<Gun>().speedZone = 0;
			}
		}
	}

	IEnumerator ChangeZoneWithTime(){
		while(doesChange){
			yield return new WaitForSecondsRealtime(timeToChange);

			newZone = (Zones)Random.Range(0,4);

			if (newZone == Zones.Damage)
				newColor = new Color (253f/255f, 253f/255f, 253f/255f, 147f/255f);
			if (newZone == Zones.Piercing) 
				newColor = new Color (84f/255f, 253f/255f, 84f/255f, 147f/255f);
			if (newZone == Zones.Cooldown)
				newColor = new Color (84f/255f, 84f/255f, 253f/255f, 147f/255f);
			if (newZone == Zones.Size)
				newColor = new Color (253f/255f, 84f/255f, 84f/255f, 147f/255f);
			if(newZone == Zones.Speed)
				newColor = new Color (253f/255f, 253f/255f, 84f/255f, 147f/255f);

			float plusR = (newColor.r-color.r)*0.1f;
			float plusG = (newColor.g-color.g)*0.1f;
			float plusB = (newColor.b-color.b)*0.1f;
			float plusA = (newColor.a-color.a)*0.1f;
			for(int i = 1; i<=10;i++){
				spriteRenderer.color = new Color(spriteRenderer.color.r+plusR,spriteRenderer.color.g+plusG,spriteRenderer.color.b+plusB,spriteRenderer.color.a+plusA);
				yield return new WaitForSecondsRealtime(0.1f);
			}

			zone = newZone;
			RefreshColor();
		}
	}
	void RefreshColor (){
		if (zone == Zones.Damage) {
			spriteRenderer.color = new Color (253f/255f, 253f/255f, 253f/255f, 147f/255f);
			color = GetComponent<SpriteRenderer> ().color;
		}
		if (zone == Zones.Piercing) {
			spriteRenderer.color = new Color (84f/255f, 253f/255f, 84f/255f, 147f/255f);
			color = GetComponent<SpriteRenderer> ().color;
		}
		if (zone == Zones.Cooldown) {
			spriteRenderer.color = new Color (84f/255f, 84f/255f, 253f/255f, 147f/255f);
			color = GetComponent<SpriteRenderer> ().color;
		}
		if (zone == Zones.Size){
			spriteRenderer.color = new Color (253f/255f, 84f/255f, 84f/255f, 147f/255f);
			color = GetComponent<SpriteRenderer> ().color;
		}
		if(zone == Zones.Speed){
			spriteRenderer.color = new Color (253f/255f, 253f/255f, 84f/255f, 147f/255f);
			color = GetComponent<SpriteRenderer> ().color;
		}
	}
}
