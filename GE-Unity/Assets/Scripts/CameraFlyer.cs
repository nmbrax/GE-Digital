using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CameraFlyer : MonoBehaviour {

	private Quaternion initialRotation;
	private Vector3 initialPosition;


	// Use this for initialization
	void Start () {
		initialRotation = transform.localRotation;
		initialPosition = transform.position;
	}

	public void zoomToTarget(Transform target,float duration) {

		Vector3 displacement = target.position - transform.position;
		Vector3 endPoint = transform.position + displacement;


		transform.DOLocalMove (endPoint, duration);
		transform.DOLocalRotateQuaternion (target.rotation, duration);
	}


	public void returnToInitialValue() {
		transform.DOLocalRotateQuaternion (initialRotation, 2);
		transform.DOLocalMove (initialPosition, 2);
	}




	public Quaternion getLookAtQuat(Transform target) {
		Vector3 displacement = target.position - transform.position;
		return Quaternion.LookRotation (displacement, new Vector3 (0, 1, 0));
	}

//	public void onButtonPressed (RaycastHit hit) {
//		zoomToTarget (hit.transform,Quaternion.identity,2);
//	}



}
