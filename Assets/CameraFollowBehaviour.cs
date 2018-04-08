using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowBehaviour : MonoBehaviour {

	public GameObject objectToFollow;
	public bool followObject = true;



	// Use this for initialization
	void Start () {

	}

	public void ResetFocus(bool resetScale){
		draggedCameraPosition = Vector3.zero;
		if (resetScale) {
			Camera.main.orthographicSize = 0.001f;
		}
	}

	public void SetInitialFocus(){
		draggedCameraPosition = Vector3.zero;
		Camera.main.orthographicSize = 0.006f;

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

	Vector3 mousePositionOld;
	Vector3 mousePositionNew;
	float scaleDelta;
	void Update () {
		if (followObject && objectToFollow != null) {
			gameObject.transform.position = new Vector3 (draggedCameraPosition.x+objectToFollow.transform.position.x, draggedCameraPosition.y/7.5f + (objectToFollow.transform.position.y - Camera.main.orthographicSize/5) + offsetY, gameObject.transform.position.z);
		}

		if (Input.GetKey(KeyCode.F)) {
			ResetFocus(false);
		}

		if (Input.GetKey(KeyCode.R)) {
			ResetFocus(true);
		}

		mousePositionOld = mousePositionNew;
		mousePositionNew = Input.mousePosition;
		if (Input.GetMouseButton (1)) {
			scaleDelta = Camera.main.ScreenToViewportPoint(mousePositionNew).magnitude-Camera.main.ScreenToViewportPoint(mousePositionOld).magnitude;
			Debug.Log (scaleDelta);
			newOrthographicSize += scaleDelta;
		} else {
			float formulaUltraWidefield = (Camera.main.orthographicSize + Input.mouseScrollDelta.y * 50f);
			float formulaWidefield = (Camera.main.orthographicSize + Input.mouseScrollDelta.y);
			float formulaMiddlefield = (Camera.main.orthographicSize + Input.mouseScrollDelta.y / 100f);
			float formulaSmallfield = (Camera.main.orthographicSize + Input.mouseScrollDelta.y / 3000f);


			if (formulaUltraWidefield >= 30f && formulaUltraWidefield <= 3000f && formulaWidefield >= 30f) {
				newOrthographicSize = formulaUltraWidefield;
			} else if (formulaWidefield >= 0.3f && formulaWidefield <= 300f && formulaMiddlefield >= 0.3f) {
				newOrthographicSize = formulaWidefield;
			} else if (formulaMiddlefield > 0.001f && formulaMiddlefield <= 300f) {
				newOrthographicSize = formulaMiddlefield;
			} else {
				if (formulaSmallfield >= 0.0001f && formulaSmallfield <=  300f) {
					newOrthographicSize = formulaSmallfield;
				}
			}
		}
		if (newOrthographicSize > 0) {
			Camera.main.orthographicSize = newOrthographicSize;
		}



		if (Input.GetMouseButton(0)) {
			//Debug.Log (deltaMovement);
			deltaMovement = Input.mousePosition - mousePressedPosition;
			if (dragMode == false) {
				mousePressedPosition = Input.mousePosition;
				dragMode = true;
				undraggedCameraPosition = draggedCameraPosition;
			} else {
				draggedCameraPosition = undraggedCameraPosition + deltaMovement/10f*newOrthographicSize/10;
			}
		}

		if (Input.GetMouseButtonUp(0)) {
			dragMode = false;
		}
	}

	public void SetObjectToFollow(GameObject obj){
		if (objectToFollow != obj) {
			objectToFollow = obj;
			undraggedCameraPosition = Vector3.zero;
			draggedCameraPosition = Vector3.zero;
		}
	}
}
