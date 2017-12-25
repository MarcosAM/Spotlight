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

	public void LookAt (Vector2 direction){
		float angle3 = Mathf.Atan2 (direction.y, -direction.x) * Mathf.Rad2Deg;
		if (direction.x != 0f || direction.y != 0f) {
			transform.rotation = Quaternion.Euler(new Vector3(angle3,90,0));
		}
	}
}