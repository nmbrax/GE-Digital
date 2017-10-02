using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HotSpot {
	public Vector3 cameraPosition;
	public Vector3 cameraViewAngles;
	public HotSpotUIData hotspotUIData;


	public HotSpot(Vector3 cameraPosition,Vector3 cameraViewAngles, HotSpotUIData uiData) {
		this.cameraPosition = cameraPosition;
		this.cameraViewAngles = cameraViewAngles;
		this.hotspotUIData = uiData;
	}

}

[System.Serializable]
public class HotSpotUIData {
	public string title;
	public string body;

	public HotSpotUIData(string label,string body) {
		this.title = label;
		this.body = body;
	}
}
