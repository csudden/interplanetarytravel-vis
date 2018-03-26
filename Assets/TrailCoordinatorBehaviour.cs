using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailCoordinatorBehaviour : MonoBehaviour {

	float startScale;
	public float scaleIntensity = 1000;
	TrailRenderer trailRenderer;
	LineRenderer lineRenderer;
	// Use this for initialization
	void Start () {
		trailRenderer = GetComponent<TrailRenderer>();
		lineRenderer = GetComponent<LineRenderer> ();

		if (trailRenderer != null) {
			startScale = (float)trailRenderer.widthMultiplier;
			Debug.Log (trailRenderer.name);
		} else if (lineRenderer != null) {
			startScale = (float)lineRenderer.widthMultiplier;
			Debug.Log (lineRenderer.name);
		}

	}

	public void SetColorGradient(Gradient gradient){
		if (trailRenderer != null) {
			trailRenderer.colorGradient = gradient;
		} else if (lineRenderer != null) {
			lineRenderer.colorGradient = gradient;
		}
	}

	public void SetToFront(){
		if (lineRenderer != null) {
			for (int i = 0; i < lineRenderer.positionCount; ++i) {
				Debug.Log ("huiHUI" +i);
				Vector3 position = lineRenderer.GetPosition (i);
				lineRenderer.SetPosition(i, new Vector3(position.x,position.y,-5));
			}
		}
	}

	public void SetToBack(){
		if (lineRenderer != null) {
			for (int i = 0; i < lineRenderer.positionCount; ++i) {
				Vector3 position = lineRenderer.GetPosition (0);
				lineRenderer.SetPosition(i, new Vector3(position.x,position.y,-0.65f));
			}
		}
	}

	public void ResetTrail(){
		if (trailRenderer != null) {
			trailRenderer.Clear ();
		} else if (lineRenderer != null) {
			Vector3[] positions;
			for (int i = 0; i < lineRenderer.positionCount; ++i) {
				lineRenderer.SetPosition(i, Vector3.zero);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (trailRenderer != null) {
			trailRenderer.widthMultiplier = startScale + Camera.main.orthographicSize / scaleIntensity;
		} else if (lineRenderer != null) {
			lineRenderer.widthMultiplier = startScale + Camera.main.orthographicSize / scaleIntensity;
		}
	}
}
