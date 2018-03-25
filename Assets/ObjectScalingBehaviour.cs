using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScalingBehaviour : MonoBehaviour {

	float startScale;
	Vector3 startPosition;
	public float scaleIntensity = 1000;
	//public float displacementIntensity = 1000;
	// Use this for initialization
	void Start () {
		startScale = gameObject.transform.localScale.x;
		startPosition = gameObject.transform.position;
	}

	// Update is called once per frame
	void Update () {
		gameObject.transform.localScale = new Vector3 (startScale + Camera.main.orthographicSize / scaleIntensity, startScale + Camera.main.orthographicSize / scaleIntensity, startScale + Camera.main.orthographicSize / scaleIntensity);
		//gameObject.transform.position = new Vector3 (startPosition.x, startPosition.y + Camera.main.orthographicSize * displacementIntensity, startPosition.z);
	}
}
