using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimelineCoordinatorBehaviour : MonoBehaviour {

	public RectTransform timelineContainer;
	public RectTransform timeStepContainer;
	public RectTransform planetMarkerContainer;

	public GameObject timeStepPrefab;
	public GameObject currentPositionMarker;
	public GameObject planetPositionMarker;
	private int timeStep = 100;
	public float timeMultiplier = 1f;

	public MovementBehaviour movementBehaviour;
	public CoordinateSystemCreator coordinateSystemCreator;


	public Text yearsPassed;
	public Text monthsPassed;
	public Text daysPassed;
	public Text hoursPassed;
	public Text minutesPassed;
	public Text secondsPassed;

	// Use this for initialization
	void Start () {
		
	}

	public void SetPlaybackSpeed(float multiplier){
		timeMultiplier = multiplier;
	}

	int timeStepChoice = 0;
	public void CreateTimestepsMinutes(){
		CreateTimesteps (0);
		timeStepChoice = 0;
	}
	public void CreateTimestepsHours(){
		CreateTimesteps (1);
		timeStepChoice = 1;
	}
	public void CreateTimestepsDays(){
		CreateTimesteps (2);
		timeStepChoice = 2;
	}
	public void CreateTimestepsMonths(){
		CreateTimesteps (3);
		timeStepChoice = 3;
	}
	public void CreateTimestepsYears(){
		CreateTimesteps (4);
		timeStepChoice = 4;
	}
		
	public void CreateTimesteps(int type){
		foreach (RectTransform t in timeStepContainer){
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
				GameObject timeStep = Instantiate (timeStepPrefab, timeStepContainer, false);
				timeStep.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (i, 0);
			}
		}
	}

	// TODO: Draw markers again on resize
	void OnResize(){
		Debug.Log ("resized");
	}

	double GetTimePassed(){

		double seconds = (movementBehaviour.distanceToStart / (movementBehaviour.kilometersPerSecond/1000000));
		double minutes = ((movementBehaviour.distanceToStart / (movementBehaviour.kilometersPerSecond/1000000f * 60f)));
		double hours = ((movementBehaviour.distanceToStart / (movementBehaviour.kilometersPerSecond/1000000f * 60f * 60f)));
		double days = ((movementBehaviour.distanceToStart / (movementBehaviour.kilometersPerSecond/1000000f * 60f * 60f * 24f)));
		double months = ((movementBehaviour.distanceToStart / (movementBehaviour.kilometersPerSecond/1000000f * 60f * 60f * 24f * 30)));
		double years = ((movementBehaviour.distanceToStart / (movementBehaviour.kilometersPerSecond/1000000f * 60f * 60f * 24f * 365)));

		seconds = (int)seconds%60;
		minutes = (int)minutes%60;
		hours = (int)hours%24;
		days = (int)(days%30.436875);
		months = (int)months%12;
		years = (int)years;

		secondsPassed.text = seconds.ToString();
		minutesPassed.text = minutes.ToString();
		hoursPassed.text = hours.ToString();
		daysPassed.text = days.ToString();
		monthsPassed.text = months.ToString();
		yearsPassed.text = years.ToString();


		Debug.Log (" years: " + years + " months: " + months + " days: " + days + " hours: " + hours + " minutes: " + minutes + " seconds: " + seconds);
		return seconds;
	}

	void CreatePlanetMarkersInTimeline(){
		// CREATE PLANET MARKERS IN TIMELINE
		foreach (RectTransform t in planetMarkerContainer){
			Destroy (t.gameObject);
		}
		float containerSize = timelineContainer.rect.width;
		if (movementBehaviour.destinationPlanet.transform.position.x > movementBehaviour.startPlanet.position.x) {

			foreach (GameObject planet in coordinateSystemCreator.planets) {
				float distance = Mathf.Abs (planet.transform.position.x - movementBehaviour.startPlanet.position.x);

				if (distance < movementBehaviour.distanceComplete && (planet.transform.position.x > movementBehaviour.startPlanet.position.x)) {
					//Debug.Log (distance+ "dis");
					//Debug.Log (movementBehaviour.distanceComplete+"disComp");
					GameObject planetMarker = Instantiate (planetPositionMarker, planetMarkerContainer, false);
					planetMarker.GetComponent<RectTransform> ().anchoredPosition = new Vector2 ((float)(distance/movementBehaviour.distanceComplete) * containerSize, 0);
				}
			}
		} else if (movementBehaviour.destinationPlanet.transform.position.x < movementBehaviour.startPlanet.position.x) {

			foreach (GameObject planet in coordinateSystemCreator.planets) {
				float distance = Mathf.Abs (planet.transform.position.x - movementBehaviour.startPlanet.position.x);

				if (distance < movementBehaviour.distanceComplete && (planet.transform.position.x < movementBehaviour.startPlanet.position.x)) {
					//Debug.Log (distance+ "dis");
					//Debug.Log (movementBehaviour.distanceComplete+"disComp");
					GameObject planetMarker = Instantiate (planetPositionMarker, planetMarkerContainer, false);
					planetMarker.GetComponent<RectTransform> ().anchoredPosition = new Vector2 ((float)(distance/movementBehaviour.distanceComplete) * containerSize, 0);
				}
			}
		}
	}


	
	// Update is called once per frame
	void Update () {

		movementBehaviour.SetTimeMultiplier(timeMultiplier);

		float containerSize = timelineContainer.rect.width;

		Debug.Log (GetTimePassed ()+ " Timepassed seconds");
		if (movementBehaviour.distanceToStart != 0) {
			currentPositionMarker.GetComponent<RectTransform>().anchoredPosition = new Vector2 (timelineContainer.rect.width * ((float)movementBehaviour.distanceToStart/(float)movementBehaviour.distanceComplete), 0);
			CreatePlanetMarkersInTimeline();
		}
	}
}
