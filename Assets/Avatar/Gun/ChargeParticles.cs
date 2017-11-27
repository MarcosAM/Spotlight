using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeParticles : MonoBehaviour {

	public ParticleSystem particles;
	public bool isLevel2 = false;

	void Start () {
		particles = GetComponent<ParticleSystem>();
	}

	public void ChargeUp(int chargeLevel){
		if (chargeLevel == 1) {
			particles.Play ();
		}
		if (chargeLevel == 2) {
			transform.localScale = new Vector3(0.8f,0.8f,0.8f);
		}
	}

	public void ChargeDown(){
		particles.Stop ();
		transform.localScale = new Vector3(0.5f,0.5f,0.5f);
	}
}
