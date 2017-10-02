using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotspotUIController : MonoBehaviour {

	public GameObject hotspotUIPrefab;
	private GameObject currentUIObject;

	public void showHotspotUI(HotSpot hotspot) {
		if (currentUIObject != null) {
			GameObject.Destroy (currentUIObject);
		}
		currentUIObject = Instantiate (hotspotUIPrefab);
		currentUIObject.transform.SetParent (transform);
		RectTransform rt = currentUIObject.GetComponent<RectTransform> ();
		rt.anchorMin = new Vector2 (1, 0);
		rt.anchorMax = new Vector2 (0, 1);
		rt.offsetMin = new Vector2 (0, 0);
		rt.offsetMax = new Vector2 (0, 0);

		Text title = currentUIObject.transform.FindChild ("title").GetComponent<Text> ();
		Text body = currentUIObject.transform.FindChild ("body").GetComponent<Text> ();

		title.text = hotspot.hotspotUIData.title;
		body.text = hotspot.hotspotUIData.body;
	}

	public void hideHotspotUI() {
		if (currentUIObject != null) {
			GameObject.Destroy (currentUIObject);
		}
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
