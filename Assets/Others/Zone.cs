using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour {

	public enum Zones : short {Damage, Piercing, Cooldown, Size, Speed};

	public Zones zone;
	public float size;

	public float coolDownTime = 0.1f;
	public float bulletSize;
	public float bulletSpeed;

	void Start ()
	{
		if (zone == Zones.Damage) {
			GetComponent<SpriteRenderer> ().color = new Color (253f/255f, 253f/255f, 253f/255f, 147f/255f);
		}
		if (zone == Zones.Piercing) {
			GetComponent<SpriteRenderer> ().color = new Color (84f/255f, 253f/255f, 84f/255f, 147f/255f);
		}
		if (zone == Zones.Cooldown) {
			GetComponent<SpriteRenderer> ().color = new Color (84f/255f, 84f/255f, 253f/255f, 147f/255f);
		}
		if (zone == Zones.Size){
			GetComponent<SpriteRenderer> ().color = new Color (253f/255f, 84f/255f, 84f/255f, 147f/255f);
		}
		if(zone == Zones.Speed){
			GetComponent<SpriteRenderer> ().color = new Color (253f/255f, 253f/255f, 84f/255f, 147f/255f);
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
}
