using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class rotateController : MonoBehaviour {

	// Use this for initialization
	void Start () {
//		doRotation ();
	}

	public void doRotation(){
		Vector3 d = Camera.main.transform.position - transform.position;
		Quaternion q = Quaternion.LookRotation (d, new Vector3 (0,1,0));
		transform.DOLocalRotateQuaternion (q, 2);
	}


	public void onButtonPressed(RaycastHit hit) {
		doRotation ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
