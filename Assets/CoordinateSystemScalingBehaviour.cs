using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinateSystemScalingBehaviour : MonoBehaviour {

	float startScale;
	Vector3 startScaleBoxCollider;
	public float scaleIntensity = 1000;
	LineRenderer lr;
	BoxCollider bc;
	// Use this for initialization
	void Start () {

		lr = GetComponent<LineRenderer> ();
		if (lr == null) {
			startScale = gameObject.transform.localScale.x;
		} else {
			startScale = lr.widthMultiplier;
		}
		bc = GetComponent<BoxCollider> ();
		if(bc != null){
			startScaleBoxCollider = bc.size;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (lr == null) {
			gameObject.transform.localScale = new Vector3 (startScale + Camera.main.orthographicSize / scaleIntensity, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
		} else {
			lr.widthMultiplier = startScale + Camera.main.orthographicSize / scaleIntensity;
			GetComponent<BoxCollider> ().size = new Vector3 (startScaleBoxCollider.x + Camera.main.orthographicSize / scaleIntensity, startScaleBoxCollider.y, startScaleBoxCollider.z);
		}

	}
}
