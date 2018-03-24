using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinateSystemScalingBehaviour : MonoBehaviour {

	float startScale;
	public float scaleIntensity = 1000;
	// Use this for initialization
	void Start () {
		startScale = gameObject.transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.localScale = new Vector3 (startScale + Camera.main.orthographicSize / scaleIntensity, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
	}
}
