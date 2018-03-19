using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeOrbs : MonoBehaviour {

	Avatar avatar;
	LifeOrb[] lifeOrbs;
	int nextOrb=0;

	void Start () {
		avatar = GetComponentInParent<Avatar> ();
		lifeOrbs = GetComponentsInChildren<LifeOrb> ();
		foreach (LifeOrb lo in lifeOrbs){
			lo.Initialize (avatar.spriteRenderer.color,this);
		}
	}

	public void StockEffect (Glossary.Effect e, Color c){
		lifeOrbs[nextOrb%lifeOrbs.Length].ReceiveEffect(e,c);
		nextOrb++;
	}

	public void TakeDamage(float damage){
		foreach (LifeOrb lo in lifeOrbs){
			if(!lo.isDestroyed){
				lo.TakeDamage (damage);
				return;
			}
		}
		avatar.Die ();
	}

//	public void StockEffect (Glossary.Effect e, Color c){
//		ActivateEffect (e);
//		for (int i = lifeOrbs.Length-1;i>=0;i--){
//			if (i == lifeOrbs.Length - 1) {
//				if (lifeOrbs [i].isDestroyed)
//					return;
//				if (lifeOrbs [i].GetEffect () != Glossary.Effect.Nothing) {
//					Glossary.Effect tempE = lifeOrbs [i].GetEffect ();
//					lifeOrbs [i].ReceiveEffect (Glossary.Effect.Nothing, Color.white);
//					CheckToDeactivate (tempE);
//				}
//			} else {
//				if(lifeOrbs[i].GetEffect() != Glossary.Effect.Nothing){
//					lifeOrbs [i + 1].ReceiveEffect (lifeOrbs[i].GetEffect(),lifeOrbs[i].standartColor);
//				}
//			}
//			if (lifeOrbs [i].isDestroyed) {
//				lifeOrbs [i+1].ReceiveEffect (e,c);
//				break;
//			}
//			if(i==0){
//				lifeOrbs [i].ReceiveEffect (e,c);
//			}
//		}
//	}

//	public void CheckToDeactivate(Glossary.Effect e){
//		bool i = false;
//		foreach(LifeOrb lo in lifeOrbs){
//			if(!lo.isDestroyed && lo.GetEffect() == e){
//				i = true;
//			}
//		}
//		if(i==false){
//			DeactivateEffect (e);
//		}
//	}
//
//	void ActivateEffect(Glossary.Effect e){
//		switch (e){
//		case Glossary.Effect.Poison:
//			avatar.myGun.isPoisonous = true;
//			break;
//		case Glossary.Effect.Piercing:
//			avatar.myGun.piercingZone = true;
//			break;
//		case Glossary.Effect.Cooldown:
//			avatar.myGun.currentCoolDownTime = 0.15f;
//			break;
//		case Glossary.Effect.Triple:
//			avatar.myGun.hasTripleShot = true;
//			break;
//		case Glossary.Effect.Bounce:
//			avatar.myGun.doesBounce = true;
//			break;
//		case Glossary.Effect.Explosive:
//			avatar.myGun.isExplosive = true;
//			break;
//		case Glossary.Effect.Shield:
//			avatar.ShieldUp ();
//			break;
//		default:
//			break;
//		}
//	}
//
//	void DeactivateEffect(Glossary.Effect e){
//		switch (e){
//		case Glossary.Effect.Poison:
//			avatar.myGun.isPoisonous = false;
//			break;
//		case Glossary.Effect.Piercing:
//			avatar.myGun.piercingZone = false;
//			break;
//		case Glossary.Effect.Cooldown:
//			avatar.myGun.currentCoolDownTime = avatar.myGun.coolDownTime;
//			break;
//		case Glossary.Effect.Triple:
//			avatar.myGun.hasTripleShot = false;
//			break;
//		case Glossary.Effect.Bounce:
//			avatar.myGun.doesBounce = false;
//			break;
//		case Glossary.Effect.Explosive:
//			avatar.myGun.isExplosive = false;
//			break;
//		case Glossary.Effect.Shield:
//			avatar.ShieldDown();
//			break;
//		default:
//			break;
//		}
//	}
}
