using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursorFollowBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Input.mousePosition;
		transform.Translate (new Vector3(50, 0));
	}
}
