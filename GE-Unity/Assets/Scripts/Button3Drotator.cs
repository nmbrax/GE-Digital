using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Button3Drotator : MonoBehaviour {


	public void onButtonPressed(RaycastHit hit) {
		Vector3 displacement = transform.position - Camera.main.transform.position;

		Vector3 perp = new Vector3 (-displacement.z, 0, displacement.x);
		Quaternion rot = Quaternion.LookRotation (-displacement, new Vector3 (0, 1, 0));
		//Quaternion rot = Quaternion.FromToRotation(new Vector3(0,1,0),-displacement);
		transform.DOLocalRotateQuaternion (transform.rotation * rot, 2);
	}


	// Use this for initialization
	void Start () {
		Vector3 displacement = transform.position - Camera.main.transform.position;
		Vector3 perp = new Vector3 (-displacement.z, 0, displacement.x);
		Quaternion rot = Quaternion.LookRotation (perp, new Vector3 (0, 1, 0));
		transform.rotation = rot * transform.rotation;
		//transform.LookAt (Camera.main.transform, new Vector3 (0, 1, 0));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
