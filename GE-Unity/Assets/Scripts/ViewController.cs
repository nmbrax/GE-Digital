using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ViewController : MonoBehaviour {

	/* DATA */
	private Vector3 localSceneCameraRotation;
	private Vector3 localSceneCameraPosition;

	[SerializeField]
	private Dictionary<string,HotSpot> hotspots = new Dictionary<string,HotSpot>();

	//[SerializeField]
	//public HotSpot[] hotSpots;

	/* Things to control */
	public CameraFlyer cameraFlyer;
	public HotspotUIController hotspotUIController;

	public GameObject enterpriseSceneUIContainer;


	/* State */
	string currentHotspotId = null;

	/* Lifecycle */
	void Start () {
	//Hardcode these here for now, later read from config file

		localSceneCameraPosition = new Vector3 (24.7f, 66.8f, 80f);
		localSceneCameraRotation = new Vector3 (32.1f, 188.5f, 0f);

		HotSpot l1 = new HotSpot(new Vector3(-4.1f,8.53f,14.5f),new Vector3(15.86f,191.8f,0f), new HotSpotUIData ("LEVER STORY 1","This is some information!!"));
		hotspots.Add ("l1", l1);

		HotSpot l2 = new HotSpot (new Vector3 (25.1f, 10.1f, 16.9f), new Vector3 (11.227f, 170.4f, 0f), new HotSpotUIData ("LEVER STORY 2","This is some information!!"));
		hotspots.Add ("l2", l2);

		HotSpot l3 = new HotSpot (new Vector3 (62.28f, 2.8f, -20.36f), new Vector3(11.96f,231f,2f), new HotSpotUIData ("LEVER STORY 3","This is some information!!"));
		hotspots.Add ("l3", l3);



		// Set intial Conditions
		//moveCameraToLocalScene();
	}

	public void moveCameraToLocalScene() {
		//Camera.main.transform.DOLocalMove()
		Camera.main.transform.localPosition = localSceneCameraPosition;
		Camera.main.transform.eulerAngles = localSceneCameraRotation;
		enterpriseSceneUIContainer.SetActive (false);
	}

	void Update () {

	}

	private void showHotspotUI(HotSpot hotspot) {
		hotspotUIController.showHotspotUI (hotspot);
	}

	private void hideHotspotUI() {
		hotspotUIController.hideHotspotUI ();
	}

	public void hotspotIdSelectedByUser(string id) {
		selectHotspotId (id);
	}


	private void selectHotspotId(string id) {
		HotSpot hs = findHotSpotWithId (id);
		if (hs == null) {
			return;
		}
		HotSpot oldHotspot = findHotSpotWithId (currentHotspotId);
		currentHotspotId = id;
		if (oldHotspot == null) {
			Debug.Log ("going to hotspot");
			goToHotspot (hs);
		} else {
			 moveToNewHotspot (hs);
			Debug.Log ("transitioning to hotspot");
		}
	}

	public void returnCameraToInitialLocation() {
		hideHotspotUI ();
		Camera.main.transform.DOLocalMove (localSceneCameraPosition, 2);
		Camera.main.transform.DORotate (localSceneCameraRotation, 2);

//		cameraFlyer.returnToInitialValue ();
		currentHotspotId = null;
	}

	private void moveToNewHotspot(HotSpot hs2) {
		hideHotspotUI ();
		Sequence seq = goToHotspotScenic (hs2);
		seq.AppendCallback (() => showHotspotUI (hs2));
	}
		
	private void goToHotspot(HotSpot hotspot) {
		
		Sequence mySequence = DOTween.Sequence ();

		Tween t1 = Camera.main.transform.DOLocalMove (hotspot.cameraPosition, 2);
		//transform.DOLocalRotateQuaternion (hotspot.transform.rotation, 2);
		Tween t2 = Camera.main.transform.DORotate(hotspot.cameraViewAngles,2);

		mySequence.Insert (0,t1);
		mySequence.Insert (0,t2);
		mySequence.AppendCallback (() => showHotspotUI (hotspot));
	}

	private Sequence goToHotspotScenic(HotSpot hotspot) {
		Vector3 waypointPos = localSceneCameraPosition;
		Vector3 targetPosition = hotspot.cameraPosition;
		Vector3 currentPosition = Camera.main.transform.position;
		Vector3 midpoint = 0.5f * currentPosition + 0.5f * targetPosition;
		Vector3 displacement = waypointPos - midpoint;

		float d1 = (targetPosition - currentPosition).sqrMagnitude;
		float d2 = displacement.sqrMagnitude;
		float t = 0.75f * d1/d2;


		Vector3 midway = midpoint + t * displacement;

		float d3 = Mathf.Sqrt( (midway - currentPosition).sqrMagnitude);
		float d4 = Mathf.Sqrt((midway - targetPosition).sqrMagnitude);

		float duration = 2.5f;

		float dur1 = d3 * duration / (d3 + d4);
		float dur2 = d4 * duration / (d3 + d4);

		Tween t1  = Camera.main.transform.DOLocalMove (midway, dur1).SetEase(Ease.OutQuad);
		Tween t2 = Camera.main.transform.DOLocalMove (targetPosition, dur2).SetEase(Ease.InOutQuad);
		Tween t3 = Camera.main.transform.DORotate(hotspot.cameraViewAngles,duration);

		Sequence seq1 = DOTween.Sequence ();
		seq1.Append (t1);
		seq1.Append (t2);

		Sequence seq = DOTween.Sequence ();
		seq.Insert (0,seq1);
		seq.Insert (0, t3);
		return seq;
	}


	private HotSpot findHotSpotWithId(string id) {
		if (id == null) {
			return null;
		}
		return hotspots [id];

	}
}
