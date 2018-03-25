using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineCoordinatorBehaviour : MonoBehaviour {

	public RectTransform timelineContainer;
	public GameObject timeStepPrefab;
	public GameObject currentPositionMarker;
	public GameObject planetPositionMarker;
	public int timeStep = 100;

	public MovementBehaviour movementBehaviour;
	public CoordinateSystemCreator coordinateSystemCreator;
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
			Debug.Log("Minutes needed:" + minutes);
		} else if (type == 1) {
			timeStep = (int)(containerSize / hours);
			Debug.Log("Hours needed:" + hours);
		} else if (type == 2) {
			timeStep = (int)(containerSize / days);
			Debug.Log("Days needed:" + days);
		} else if (type == 3) {
			timeStep = (int)(containerSize / months);
			Debug.Log("Months needed:" + months);
		} else if (type == 4) {
			timeStep = (int)(containerSize / years);
			Debug.Log("Years needed:" + years);
		}


		// CREATE TIMESTEPS IN TIMELINE
		if ((int)timeStep > 0) {
			for (int i = timeStep; i < containerSize; i += timeStep) {
				GameObject timeStep = Instantiate (timeStepPrefab, timelineContainer, false);
				timeStep.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (i, 0);
			}
		}

		// CREATE PLANET MARKERS IN TIMELINE
		if (movementBehaviour.destinationPlanet.transform.position.x > movementBehaviour.startPlanet.position.x) {

			foreach (GameObject planet in coordinateSystemCreator.planets) {
				float distance = Mathf.Abs (planet.transform.position.x - movementBehaviour.startPlanet.position.x);

				if (distance < movementBehaviour.distanceComplete && (planet.transform.position.x > movementBehaviour.startPlanet.position.x)) {
					Debug.Log (distance+ "dis");
					Debug.Log (movementBehaviour.distanceComplete+"disComp");
					GameObject planetMarker = Instantiate (planetPositionMarker, timelineContainer, false);
					planetMarker.GetComponent<RectTransform> ().anchoredPosition = new Vector2 ((float)(distance/movementBehaviour.distanceComplete) * containerSize, 0);
				}
			}
		} else if (movementBehaviour.destinationPlanet.transform.position.x < movementBehaviour.startPlanet.position.x) {

			foreach (GameObject planet in coordinateSystemCreator.planets) {
				float distance = Mathf.Abs (planet.transform.position.x - movementBehaviour.startPlanet.position.x);

				if (distance < movementBehaviour.distanceComplete && (planet.transform.position.x < movementBehaviour.startPlanet.position.x)) {
					Debug.Log (distance+ "dis");
					Debug.Log (movementBehaviour.distanceComplete+"disComp");
					GameObject planetMarker = Instantiate (planetPositionMarker, timelineContainer, false);
					planetMarker.GetComponent<RectTransform> ().anchoredPosition = new Vector2 ((float)(distance/movementBehaviour.distanceComplete) * containerSize, 0);
				}
			}
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
