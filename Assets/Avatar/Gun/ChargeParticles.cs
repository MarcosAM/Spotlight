using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeParticles : MonoBehaviour {

	Gun gun;
	ParticleSystem particles;

	void Start () {
		gun = GetComponentInParent<Gun>();
		particles = GetComponent<ParticleSystem>();
	}
	
	void Update ()
	{
		if (particles.isPlaying) {
			if (gun.chargeLevel == 1) {
				transform.localScale = new Vector3(0.5f,0.5f,0.5f);
			}
			if (gun.chargeLevel == 2){
				transform.localScale = new Vector3(0.8f,0.8f,0.8f);
			}
		}
	}
}
