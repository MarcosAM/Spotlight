using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultParticles : MonoBehaviour {

	ParticleSystem particle;
	Avatar avatar;

	void Start(){
		particle = GetComponent<ParticleSystem>();
		avatar = GetComponentInParent<Avatar>();
	}

	public void StartAssault (){
		particle.Play();
	}

	public void StopAssault(){
		particle.Stop();
	}
}
