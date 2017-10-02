using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button3dCollider : MonoBehaviour {
	[SerializeField]
	public string id;
	public ViewController vc;

	private Collider coll;
	// Use this for initialization
	void Start () {
		coll = GetComponent<Collider> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (coll.Raycast(ray, out hit, 1000.0F)) {
				vc.hotspotIdSelectedByUser (id);

			}
		}
	}
}
