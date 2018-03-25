using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineCoordinatorBehaviour : MonoBehaviour {

	public RectTransform timelineContainer;
	public GameObject timeStepPrefab;
	public GameObject currentPositionMarker;
	public int timeStep = 100;

	public MovementBehaviour movementBehaviour;
	// Use this for initialization
	void Start () {
		
	}

	public void CreateTimesteps(){
		foreach (RectTransform t in timelineContainer){
			Destroy (t.gameObject);
		}

		float containerSize = timelineContainer.rect.width;

		for (int i = timeStep; i < containerSize; i += timeStep) {
			GameObject timeStep = Instantiate (timeStepPrefab, timelineContainer, false);
			timeStep.GetComponent<RectTransform>().anchoredPosition = new Vector2(i,0);
		}
	}
	
	// Update is called once per frame
	void Update () {
		float containerSize = timelineContainer.rect.width;

		if (movementBehaviour.distanceToStart != 0) {
			currentPositionMarker.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (timelineContainer.rect.width * (movementBehaviour.distanceToStart/movementBehaviour.distanceComplete), 0);
		}
	}
}
