using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinateSystemScalingBehaviour : MonoBehaviour {

	float startScale;
	public float scaleIntensity = 1000;
	LineRenderer lr;
	// Use this for initialization
	void Start () {

		lr = GetComponent<LineRenderer> ();
		if (lr == null) {
			startScale = gameObject.transform.localScale.x;
		} else {
			startScale = lr.widthMultiplier;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (lr == null) {
			gameObject.transform.localScale = new Vector3 (startScale + Camera.main.orthographicSize / scaleIntensity, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
		} else {
			lr.widthMultiplier = startScale + Camera.main.orthographicSize / scaleIntensity;
		}

	}
}
