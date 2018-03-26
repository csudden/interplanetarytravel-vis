using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailCoordinatorBehaviour : MonoBehaviour {

	float startScale;
	public float scaleIntensity = 1000;
	TrailRenderer trailRenderer;
	// Use this for initialization
	void Start () {
		trailRenderer = GetComponent<TrailRenderer>();
		startScale = (float)trailRenderer.widthMultiplier;
		Debug.Log (trailRenderer.name);
	}

	public void SetColorGradient(Gradient gradient){
		trailRenderer.colorGradient = gradient;
	}

	public void ResetTrail(){
		if (trailRenderer != null) {
			trailRenderer.Clear ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<TrailRenderer>().widthMultiplier = startScale + Camera.main.orthographicSize/scaleIntensity;
	}
}
