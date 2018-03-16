using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour {

	public Glossary.Effect effect;
	Glossary.Effect newEffect;
	public float size;

	Color color;
	Color newColor;

	[SerializeField]float HP = 18f;

	public float coolDownTime = 0.1f;
	public float bulletSize;
	public float bulletSpeed;

	public bool doesChange;
	public float timeToChange = 8f;

	Collider2D[] avatarsCollider;
	float[] avatarsCountdown;

	Color poison = new Color (84f/255f, 253f/255f, 84f/255f, 100f/255f);
	Color piercing = new Color (253f / 255f, 13f / 255f, 253f / 255f, 100f / 255f);
	Color cooldown = new Color (84f/255f, 84f/255f, 253f/255f, 100f/255f);
	Color triple = new Color (253f/255f, 84f/255f, 84f/255f, 100f/255f);
	Color bounce = new Color (253f/255f, 253f/255f, 13f/255f, 100f/255f);
	Color explosive = new Color (253f/255f, 253f/255f, 84f/255f, 100f/255f);
	Color shield = new Color (84f/255f, 225/255f, 253f/255f, 100f/255f);

	Orb orb;
	SpriteRenderer spriteRenderer;
	public Collider2D c2D;

	void Awake(){
		spriteRenderer = GetComponent<SpriteRenderer>();
		orb = GetComponentInParent<Orb> ();
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
		avatarsCollider = new Collider2D[4];
		avatarsCountdown = new float[4];
		Avatar[] allAvatars = FindObjectsOfType<Avatar> ();
		for(int i=0;i<allAvatars.Length;i++){
			avatarsCollider [i] = allAvatars [i].GetComponent<Collider2D> ();
			avatarsCountdown [i] = 0;
		}
	}

	void OnTriggerStay2D(Collider2D c){
		if(c.GetComponent<Avatar>()){
			for(int i =0;i<avatarsCollider.Length;i++){
				if(avatarsCollider[i]==c){
					avatarsCountdown [i] += Time.deltaTime;
					if(avatarsCountdown[i]>2f){
						c.GetComponent<Avatar> ().lifeOrbs.StockEffect (effect,new Color(color.r,color.g,color.b,1f));
						avatarsCountdown [i] = 0;
					}
				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D c){
		if(c.GetComponent<Avatar>()){
			for(int i =0;i<avatarsCollider.Length;i++){
				if(avatarsCollider[i]==c){
					avatarsCountdown [i] = 0;
				}
			}
		}
	}

//	void OnTriggerEnter2D(Collider2D c){
//		if(c.GetComponent<Avatar>()){
//			c.GetComponent<Avatar> ().lifeOrbs.StockEffect (effect,color);
//		}
//	}

	IEnumerator ChangeZoneWithTime(){
		while(doesChange){
			yield return new WaitForSecondsRealtime(timeToChange);

			newEffect = (Glossary.Effect)Random.Range(0,7);

			if (newEffect == Glossary.Effect.Poison)
				newColor = poison;
			if (newEffect == Glossary.Effect.Piercing) 
				newColor = piercing;
			if (newEffect == Glossary.Effect.Cooldown)
				newColor = cooldown;
			if (newEffect == Glossary.Effect.Triple)
				newColor = triple;
			if(newEffect == Glossary.Effect.Bounce)
				newColor = bounce;
			if(newEffect == Glossary.Effect.Explosive)
				newColor = explosive;
			if (newEffect == Glossary.Effect.Shield)
				newColor = shield;


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
			effect = newEffect;
			GetComponent<CircleCollider2D> ().offset = new Vector2 (0,0);
			RefreshColor();
		}
	}
	void RefreshColor (){
		if (effect == Glossary.Effect.Poison) {
			spriteRenderer.color = poison;
			color = GetComponent<SpriteRenderer> ().color;
			orb.text.text = "VENENOSO";
		}
		if (effect == Glossary.Effect.Piercing) {
			spriteRenderer.color = piercing;
			color = GetComponent<SpriteRenderer> ().color;
			orb.text.text = "PERFURANTE";
		}
		if (effect == Glossary.Effect.Cooldown) {
			spriteRenderer.color = cooldown;
			color = GetComponent<SpriteRenderer> ().color;
			orb.text.text = "METRALHADORA";
		}
		if (effect == Glossary.Effect.Triple){
			spriteRenderer.color = triple;
			color = GetComponent<SpriteRenderer> ().color;
			orb.text.text = "TIRO TRIPLO";
		}
		if(effect == Glossary.Effect.Bounce){
			spriteRenderer.color = bounce;
			color = GetComponent<SpriteRenderer> ().color;
			orb.text.text = "RICOCHETEIA";
		}
		if (effect == Glossary.Effect.Explosive) {
			spriteRenderer.color = explosive;
			color = GetComponent<SpriteRenderer> ().color;
			orb.text.text = "EXPLOSIVA";
		}
		if(effect == Glossary.Effect.Shield){
			spriteRenderer.color = shield;
			color = GetComponent<SpriteRenderer> ().color;
			orb.text.text = "ESCUDO";
		}
	}

	public void RandomizeAndActive(){
		int i=0;
		Zone[] zones = FindObjectsOfType<Zone> ();
		while (i < zones.Length) {
			i = 0;
			effect = (Glossary.Effect)Random.Range (0, 7);
			foreach (Zone z in zones){
				if(effect != z.effect || this == z){
					i++;
				}
			}
		}
		RefreshColor ();
		GetComponentInParent<Orb>().transform.position = new Vector3 (Random.Range(-16.0F,16.0F),Random.Range(-4.0F,4.0F),GetComponentInParent<Orb>().transform.position.z);
	}

//	IEnumerator GiveVPs(){
//		int i;
//		Avatar[] avatars = FindObjectsOfType<Avatar> ();
//		while(1>0){
//			RaycastHit2D[] hits = new RaycastHit2D[10];
//			i = c2D.Cast(Vector2.zero, hits);
//			if(i != 0){
//				foreach (RaycastHit2D hit in hits){
//					if(hit.collider != null){
//						if(hit.collider.gameObject.GetComponent<Avatar>()){
//							foreach (Avatar a in avatars){
//								if(hit.collider.gameObject.GetComponent<Avatar>() == a){
//									a.victoryPoints++;
//								}
//							}
//						}
//					}
//				}
//				if(HP <=0){
//					Zone[] zones = FindObjectsOfType<Zone> ();
//					foreach (Avatar a in avatars) {
//						if(c2D.IsTouching(a.GetComponent<Collider2D>())){
//							bool hasSameZone = false;
//							foreach (Zone z in zones){
//								if(z.c2D.IsTouching(a.GetComponent<Collider2D>()) && z.zone == zone && z != this){
//									hasSameZone = true;
//								}
//							}
//							if(!hasSameZone){
//								TakeEffectAway (a.GetComponent<Collider2D>());
//							}
//						}
//					}
//					Destroy (GetComponentInParent<Orb>().gameObject);
//				}
//			}
//			yield return new WaitForSecondsRealtime (1.5f);
//		}
//	}
}
