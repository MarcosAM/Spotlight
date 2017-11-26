using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashParticles : MonoBehaviour {

	ParticleSystem particle;
	Avatar avatar;

	void Start(){
		particle = GetComponent<ParticleSystem>();
		avatar = GetComponentInParent<Avatar>();
	}

	public void StartDash (){
		particle.Play();
	}

	public void StopDash(){
		particle.Stop();
	}
}
