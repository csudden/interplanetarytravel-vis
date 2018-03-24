﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowBehaviour : MonoBehaviour {

	public GameObject objectToFollow;
	public bool followObject = true;
	// Use this for initialization
	void Start () {
		
	}

	Vector3 mousePressedPosition;
	Vector3 mouseReleasedPosition;
	Vector3 deltaMovement;

	Vector3 undraggedCameraPosition;
	Vector3 draggedCameraPosition;
	bool dragMode = false;
	// Update is called once per frame
	void Update () {
		if (followObject) {
			gameObject.transform.position = new Vector3 (draggedCameraPosition.x, draggedCameraPosition.y, gameObject.transform.position.z);
			//draggedCameraPosition =  new Vector3 (undraggedCameraPosition.x + deltaMovement.x / 10f, undraggedCameraPosition.y + deltaMovement.y / 10f, gameObject.transform.position.z);
		}
			
		Camera.main.orthographicSize += Input.mouseScrollDelta.y;

		if (Input.GetMouseButton(0)) {
			if (dragMode == false) {
				mousePressedPosition = Input.mousePosition;
				dragMode = true;
				undraggedCameraPosition = draggedCameraPosition;
			} else {
				deltaMovement = Input.mousePosition - mousePressedPosition;
				Debug.Log (deltaMovement);
				draggedCameraPosition = undraggedCameraPosition + deltaMovement/10f;
			}
		}

		if (Input.GetMouseButtonUp(0)) {
			//mouseReleasedPosition = Input.mousePosition;
			//deltaMovement = mouseReleasedPosition - mousePressedPosition;

			dragMode = false;
		}
	}
}
