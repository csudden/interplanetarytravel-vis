using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustScaleOnStartBehaviour : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		// Unity Plane Scale compared to Cube is 10 meters, so divide by 10
		transform.localScale /= 1f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
