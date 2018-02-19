using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	ParticleSystem ps;

	void Start(){
		ps = GetComponent<ParticleSystem> ();
	}

	void Update(){
		if (!ps.isPlaying) {
			Destroy (gameObject);
		}
	}

	void OnParticleCollision (GameObject g){
		if (g.GetComponent<Avatar> ()) {
			g.GetComponent<Avatar> ().ReduceLifeBy (0.05F,FindObjectOfType<Avatar>());
		}
	}
}
