using System.Collections;
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

	public float offsetY = -3;

	float newOrthographicSize;
	// Update is called once per frame
	void Update () {
		if (followObject) {
			gameObject.transform.position = new Vector3 (draggedCameraPosition.x+objectToFollow.transform.position.x, objectToFollow.transform.position.y - newOrthographicSize/5 + offsetY, gameObject.transform.position.z);
		}

		if (Input.GetKey(KeyCode.F)) {
			draggedCameraPosition = Vector3.zero;
		}

		newOrthographicSize = (Camera.main.orthographicSize + Input.mouseScrollDelta.y/4);
		if (newOrthographicSize > 0) {
			Debug.Log ("newOrtho" + newOrthographicSize);
			Camera.main.orthographicSize = newOrthographicSize;
		}

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
