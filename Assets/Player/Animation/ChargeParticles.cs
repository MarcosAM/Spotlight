using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeParticles : MonoBehaviour {

	public ParticleSystem particles;

	void Start () {
		particles = GetComponent<ParticleSystem>();
	}

	public void SustainCharge ()
	{
		transform.localScale = new Vector3(0.8f,0.8f,0.8f);
	}

	public void Charge ()
	{
		transform.localScale = new Vector3(0.2f,0.2f,0.2f);
		particles.Play();
	}

	public void StopCharge(){
		particles.Stop ();
	}
}
