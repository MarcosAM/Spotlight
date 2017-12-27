using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashParticles : MonoBehaviour {

	ParticleSystem particle;

	void Start(){
		particle = GetComponent<ParticleSystem>();
	}

	public void StartDash (){
		particle.Play();
	}

	public void StopDash(){
		particle.Stop();
	}

	public void LookAt (Vector2 direction){
		float angle2 = Mathf.Atan2(direction.x, -direction.y) * Mathf.Rad2Deg;
		if(direction.x != 0f || direction.y != 0f)
			transform.rotation = Quaternion.Euler(new Vector3(0,0,angle2));
	}
}