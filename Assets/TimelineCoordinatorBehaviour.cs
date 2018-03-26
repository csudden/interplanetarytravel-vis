using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimelineCoordinatorBehaviour : MonoBehaviour {

	public Transform startPlanet;
	public Transform destinationPlanet;

	public RectTransform timelineContainer;
	public RectTransform timeStepContainer;
	public RectTransform planetMarkerContainer;

	public GameObject timeStepPrefab;
	public GameObject currentPositionMarker;
	public GameObject planetPositionMarker;
	private int timeStep = 100;
	public float timeMultiplier = 1f;

	public GameObject spaceshipParent;
	public MovementBehaviour selectedMovementBehaviour;
	public CoordinateSystemCreator coordinateSystemCreator;


	public Text yearsPassed;
	public Text monthsPassed;
	public Text daysPassed;
	public Text hoursPassed;
	public Text minutesPassed;
	public Text secondsPassed;

	public Gradient gradientSpaceshipSelected;
	public Gradient gradientSpaceshipUnselected;

	public enum TimeStep {Minutes, Hours, Days, Months, Years};

	public TimeStep timestepSetting = TimeStep.Months;

	MovementBehaviour[] movementBehaviourSpaceships;
	// Use this for initialization
	void Start () {
		movementBehaviourSpaceships = spaceshipParent.GetComponentsInChildren<MovementBehaviour>();
		timestepSetting = TimeStep.Months;
		StartCoroutine (LateStart (0.01f));
	}

	IEnumerator LateStart(float waitTime){
		yield return new WaitForSeconds (waitTime);
		StartJourneyAllShips();
	}

	public void SetPlaybackSpeed(float multiplier){
		timeMultiplier = multiplier;
	}


	public void CreateTimestepsMinutes(){
		CreateTimesteps ((int)TimeStep.Minutes);
		timestepSetting = TimeStep.Minutes;
	}
	public void CreateTimestepsHours(){
		CreateTimesteps ((int)TimeStep.Hours);
		timestepSetting = TimeStep.Hours;
	}
	public void CreateTimestepsDays(){
		CreateTimesteps ((int)TimeStep.Days);
		timestepSetting = TimeStep.Days;
	}
	public void CreateTimestepsMonths(){
		CreateTimesteps ((int)TimeStep.Months);
		timestepSetting = TimeStep.Months;
	}
	public void CreateTimestepsYears(){
		CreateTimesteps ((int)TimeStep.Years);
		timestepSetting = TimeStep.Years;
	}
		
	public void CreateTimesteps(int type){
		foreach (RectTransform t in timeStepContainer){
			Destroy (t.gameObject);
		}

		float containerSize = timelineContainer.rect.width;

		double minutes = ((selectedMovementBehaviour.distanceComplete / (selectedMovementBehaviour.kilometersPerSecond/1000000f * 60f)));
		double hours = ((selectedMovementBehaviour.distanceComplete / (selectedMovementBehaviour.kilometersPerSecond/1000000f * 60f * 60f)));
		double days = ((selectedMovementBehaviour.distanceComplete / (selectedMovementBehaviour.kilometersPerSecond/1000000f * 60f * 60f * 24f)));
		double months = ((selectedMovementBehaviour.distanceComplete / (selectedMovementBehaviour.kilometersPerSecond/1000000f * 60f * 60f * 24f * 30)));
		double years = ((selectedMovementBehaviour.distanceComplete / (selectedMovementBehaviour.kilometersPerSecond/1000000f * 60f * 60f * 24f * 365)));

		if (type == (int)TimeStep.Minutes) {
			timeStep = (int)(containerSize / minutes);
			Debug.Log("Minutes needed:" + minutes);
		} else if (type == (int)TimeStep.Hours) {
			timeStep = (int)(containerSize / hours);
			Debug.Log("Hours needed:" + hours);
		} else if (type == (int)TimeStep.Days) {
			timeStep = (int)(containerSize / days);
			Debug.Log("Days needed:" + days);
		} else if (type == (int)TimeStep.Months) {
			timeStep = (int)(containerSize / months);
			Debug.Log("Months needed:" + months);
		} else if (type == (int)TimeStep.Years) {
			timeStep = (int)(containerSize / years);
			Debug.Log("Years needed:" + years);
		}


		// CREATE TIMESTEPS IN TIMELINE
		if ((int)timeStep > 0) {
			for (int i = timeStep; i < containerSize; i += timeStep) {
				GameObject timeStepMarker = Instantiate (timeStepPrefab, timeStepContainer, false);
				timeStepMarker.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (i, 0);
			}
		}
	}

	// TODO: Draw markers again on resize
	void OnResize(){
		Debug.Log ("resized");
	}

	double GetTimePassed(){

		double seconds = (selectedMovementBehaviour.distanceToStart / ((float)selectedMovementBehaviour.kilometersPerSecond/1000000f));
		double minutes = ((selectedMovementBehaviour.distanceToStart / ((float)selectedMovementBehaviour.kilometersPerSecond/1000000f * 60f)));
		double hours = ((selectedMovementBehaviour.distanceToStart / ((float)selectedMovementBehaviour.kilometersPerSecond/1000000f * 60f * 60f)));
		double days = ((selectedMovementBehaviour.distanceToStart / ((float)selectedMovementBehaviour.kilometersPerSecond/1000000f * 60f * 60f * 24f)));
		double months = ((selectedMovementBehaviour.distanceToStart / ((float)selectedMovementBehaviour.kilometersPerSecond/1000000f * 60f * 60f * 24f * 30f)));
		double years = ((selectedMovementBehaviour.distanceToStart / ((float)selectedMovementBehaviour.kilometersPerSecond/1000000f * 60f * 60f * 24f * 365f)));

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


		//Debug.Log (" years: " + years + " months: " + months + " days: " + days + " hours: " + hours + " minutes: " + minutes + " seconds: " + seconds);
		return seconds;
	}
		
	public void StartJourneyAllShips(){
		foreach (MovementBehaviour mb in movementBehaviourSpaceships) {
			mb.SetStartAndDestination(startPlanet, destinationPlanet);
			if (mb != selectedMovementBehaviour) {
				mb.GetComponentInChildren<TrailCoordinatorBehaviour> ().SetColorGradient(gradientSpaceshipUnselected);
			} else {
				mb.GetComponentInChildren<TrailCoordinatorBehaviour> ().SetColorGradient(gradientSpaceshipSelected);
			}
			mb.StartJourney();
		}

		selectedMovementBehaviour.GetComponentInChildren<TrailCoordinatorBehaviour>().SetColorGradient (gradientSpaceshipSelected);
		CreateTimesteps((int)TimeStep.Months);
	}

	void CreatePlanetMarkersInTimeline(){
		// CREATE PLANET MARKERS IN TIMELINE
		foreach (RectTransform t in planetMarkerContainer){
			Destroy (t.gameObject);
		}
		float containerSize = timelineContainer.rect.width;
		if (selectedMovementBehaviour.destinationPlanet.transform.position.x > selectedMovementBehaviour.startPlanet.position.x) {

			foreach (GameObject planet in coordinateSystemCreator.planets) {
				float distance = Mathf.Abs (planet.transform.position.x - selectedMovementBehaviour.startPlanet.position.x);

				if (distance < selectedMovementBehaviour.distanceComplete && (planet.transform.position.x > selectedMovementBehaviour.startPlanet.position.x)) {
					//Debug.Log (distance+ "dis");
					//Debug.Log (movementBehaviour.distanceComplete+"disComp");
					GameObject planetMarker = Instantiate (planetPositionMarker, planetMarkerContainer, false);
					planetMarker.GetComponent<RectTransform> ().anchoredPosition = new Vector2 ((float)(distance/selectedMovementBehaviour.distanceComplete) * containerSize, 0);
				}
			}
		} else if (selectedMovementBehaviour.destinationPlanet.transform.position.x < selectedMovementBehaviour.startPlanet.position.x) {

			foreach (GameObject planet in coordinateSystemCreator.planets) {
				float distance = Mathf.Abs (planet.transform.position.x - selectedMovementBehaviour.startPlanet.position.x);

				if (distance < selectedMovementBehaviour.distanceComplete && (planet.transform.position.x < selectedMovementBehaviour.startPlanet.position.x)) {
					//Debug.Log (distance+ "dis");
					//Debug.Log (movementBehaviour.distanceComplete+"disComp");
					GameObject planetMarker = Instantiate (planetPositionMarker, planetMarkerContainer, false);
					planetMarker.GetComponent<RectTransform> ().anchoredPosition = new Vector2 ((float)(distance/selectedMovementBehaviour.distanceComplete) * containerSize, 0);
				}
			}
		}
	}


	
	// Update is called once per frame
	void Update () {
		GetTimePassed ();

		// Check which object has been clicked

		if (Input.GetMouseButtonDown (0)) {
			RaycastHit[] hit;
			hit = Physics.RaycastAll (Camera.main.ScreenPointToRay (Input.mousePosition),20f);
			if (hit.Length>0) {
				Debug.Log ("Length: " + hit.Length);
				foreach (RaycastHit h in hit) {
					if (h.collider.gameObject.layer == 8) {
						foreach (MovementBehaviour mb in movementBehaviourSpaceships) {
							mb.GetComponentInChildren<TrailCoordinatorBehaviour>().SetColorGradient(gradientSpaceshipUnselected);
						}
						Debug.Log (h.collider.gameObject.name + " was hit with distance: " + h.distance);
						selectedMovementBehaviour = h.collider.gameObject.GetComponent<MovementBehaviour>();
						selectedMovementBehaviour.GetComponentInChildren<TrailCoordinatorBehaviour>().SetColorGradient(gradientSpaceshipSelected);
						CreateTimesteps((int)timestepSetting);
						Camera.main.GetComponent<CameraFollowBehaviour> ().SetObjectToFollow(selectedMovementBehaviour.gameObject);
					}
				}
			}
		}

		// Apply Time Scaling to all available Spaceships
		foreach (MovementBehaviour mb in movementBehaviourSpaceships) {
			mb.SetTimeMultiplier(timeMultiplier);
		}

		float containerSize = timelineContainer.rect.width;

		//Debug.Log (GetTimePassed ()+ " Timepassed seconds");
		if (selectedMovementBehaviour.distanceToStart != 0) {
			Debug.Log (selectedMovementBehaviour.distanceToStart);
			currentPositionMarker.GetComponent<RectTransform>().anchoredPosition = new Vector2 (timelineContainer.rect.width * ((float)selectedMovementBehaviour.distanceToStart/(float)selectedMovementBehaviour.distanceComplete), 0);
			CreatePlanetMarkersInTimeline();
		}
	}
}
