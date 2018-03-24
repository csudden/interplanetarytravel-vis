using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailCoordinatorBehaviour : MonoBehaviour {

	float startScale;
	public float scaleIntensity = 1000;
	// Use this for initialization
	void Start () {
		startScale = (float)GetComponent<TrailRenderer>().widthMultiplier;
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<TrailRenderer>().widthMultiplier = startScale + Camera.main.orthographicSize/scaleIntensity;
	}
}
