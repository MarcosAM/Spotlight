using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour {

	Vector3 pointA;
	public Transform pointB;
	int direction = -1;

	void Start(){
		pointA = transform.position;
	}

	void Update (){
		if(direction == -1){
			if(transform.position.x < pointB.position.x){
				direction = 1;
			}
		}
		if(direction == 1){
			if(transform.position.x > pointA.x){
				direction = -1;
			}
		}
		transform.Translate (direction*transform.right*3*Time.deltaTime);
	}

//	void Start () {
//		StartCoroutine (PointAToPointB(transform,pointB));
//	}

//	IEnumerator PointAToPointB(Transform a, Transform b){
//		Transform destination;
//		while (1>0){
//			if (transform.position == a.position) {
//				destination = b;
//			} else {
//				destination = a;
//			}
//			while(transform.position != destination.position){
//				
////				transform.position = Vector2.MoveTowards (transform.position,destination.position,5);
//				yield return new WaitForSecondsRealtime (0.1F);
//			}
//		}
//	}
}