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

	public void CreateTimestepsMinutes(){
		CreateTimesteps (0);
	}
	public void CreateTimestepsHours(){
		CreateTimesteps (1);
	}
	public void CreateTimestepsDays(){
		CreateTimesteps (2);
	}
	public void CreateTimestepsMonths(){
		CreateTimesteps (3);
	}
	public void CreateTimestepsYears(){
		CreateTimesteps (4);
	}
		
	public void CreateTimesteps(int type){
		foreach (RectTransform t in timelineContainer){
			Destroy (t.gameObject);
		}

		float containerSize = timelineContainer.rect.width;

		double minutes = ((movementBehaviour.distanceComplete / (movementBehaviour.kilometersPerSecond/1000000f * 60f)));
		double hours = ((movementBehaviour.distanceComplete / (movementBehaviour.kilometersPerSecond/1000000f * 60f * 60f)));
		double days = ((movementBehaviour.distanceComplete / (movementBehaviour.kilometersPerSecond/1000000f * 60f * 60f * 24f)));
		double months = ((movementBehaviour.distanceComplete / (movementBehaviour.kilometersPerSecond/1000000f * 60f * 60f * 24f * 30)));
		double years = ((movementBehaviour.distanceComplete / (movementBehaviour.kilometersPerSecond/1000000f * 60f * 60f * 24f * 365)));

		if (type == 0) {
			timeStep = (int)(containerSize / minutes);
		} else if (type == 1) {
			timeStep = (int)(containerSize / hours);
		} else if (type == 2) {
			timeStep = (int)(containerSize / days);
		} else if (type == 3) {
			timeStep = (int)(containerSize / months);
		} else if (type == 4) {
			timeStep = (int)(containerSize / years);
		}

		Debug.Log(hours);

		for (int i = timeStep; i < containerSize; i += timeStep) {
			GameObject timeStep = Instantiate (timeStepPrefab, timelineContainer, false);
			timeStep.GetComponent<RectTransform>().anchoredPosition = new Vector2(i,0);
		}
	}
	
	// Update is called once per frame
	void Update () {
		float containerSize = timelineContainer.rect.width;

		if (movementBehaviour.distanceToStart != 0) {
			currentPositionMarker.GetComponent<RectTransform>().anchoredPosition = new Vector2 (timelineContainer.rect.width * ((float)movementBehaviour.distanceToStart/(float)movementBehaviour.distanceComplete), 0);
		}
	}
}
