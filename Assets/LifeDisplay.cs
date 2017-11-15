using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeDisplay : MonoBehaviour {

	public Life life;
	public float maxLifeScale;
	
	void Start () {
		life = GetComponentInParent<Life>();
		maxLifeScale = transform.localScale.x;
	}

	void Update () {
		Vector3 currentScale = new Vector3 ((life.currentLife/life.maxLife)*maxLifeScale,transform.localScale.y, transform.localScale.z);
		transform.localScale = currentScale;
	}
}
