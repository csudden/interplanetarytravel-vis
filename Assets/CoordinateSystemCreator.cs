using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinateSystemCreator : MonoBehaviour {

	public GameObject planetIndicatorLine;
	public GameObject coordinateSystem;
	// Use this for initialization
	void Start () {
		foreach (Transform t in gameObject.transform) {
			GameObject obj = Instantiate(planetIndicatorLine, t.position, Quaternion.Euler(-90,0,0),coordinateSystem.transform);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
