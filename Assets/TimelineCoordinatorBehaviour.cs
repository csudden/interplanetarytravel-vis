using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.SceneManagement;

public class TimelineCoordinatorBehaviour : MonoBehaviour, IPointerDownHandler {

	public Transform startPlanet;
	public Transform destinationPlanet;

	public RectTransform timelineContent;
	public RectTransform timelineContainer;
	public RectTransform timeStepContainer;
	public RectTransform bordersContainer;
	public RectTransform planetMarkerContainer;
	public RectTransform spaceshipPositionContainer;
	public RectTransform spacecraftSelectionSet;
	public RectTransform jumpToSpaceshipPositionButton;

	public GameObject timeStepPrefab;
	public GameObject timeStepTooDenseMarker;
	public GameObject currentSpaceshipPositionMarker;
	public GameObject currentCameraPositionMarker;
	public GameObject planetPositionMarker;
	public GameObject startPlanetMarker;
	public GameObject destinationPlanetMarker;

	public GameObject infoPanelMiddle;

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
	public Text unitsDisplay;

	public Gradient gradientSpaceshipSelected;
	public Gradient gradientSpaceshipUnselected;
	public Color colorSelected;
	public Color colorUnselected;

	public enum TimeStep {Minutes, Hours, Days, Months, Years};

	public TimeStep timestepSetting = TimeStep.Months;

	public GameObject detailsDisplay;

	MovementBehaviour[] movementBehaviourSpaceships;
	// Use this for initialization
	void Start () {
		movementBehaviourSpaceships = spaceshipParent.GetComponentsInChildren<MovementBehaviour>();
		timestepSetting = TimeStep.Months;
		StartCoroutine (LateStart (0.01f));

		switchToPlanetSystemOverview ();
	}

	void switchToPlanetSystemOverview(){
		planetSystemOverview.SetActive (true);
		timelineContent.gameObject.SetActive (false);
		jumpToSpaceshipPositionButton.gameObject.SetActive (false);
	}

	void switchToRegularPlanetSystemView(){
		timelineContent.gameObject.SetActive (true);
		planetSystemOverview.SetActive (false);
		jumpToSpaceshipPositionButton.gameObject.SetActive (true);
	}
		
	IEnumerator LateStart(float waitTime){
		yield return new WaitForSeconds (waitTime);
		//StartJourneyAllShips();
	}

	public void SetPlaybackSpeed(float multiplier){
		timeMultiplier = multiplier;
	}

	public void SetNextPlaybackSpeed(Text btnText){
		if (timeMultiplier <= 1000000000) {
			timeMultiplier *= 10;
		}
		btnText.text = timeMultiplier.ToString ("n0");
	}

	public void SetPreviousPlaybackSpeed(Text btnText){
		if (timeMultiplier >= 10) {
			timeMultiplier /= 10;
		}
		btnText.text = timeMultiplier.ToString ("n0");
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

	public void CreateTimesteps(){
		CreateTimesteps ((int)timestepSetting);
	}

	public void CreateTimestepsNextUnit(Text btnText){
		int index = ((int)timestepSetting + 1);

		if (index < Enum.GetNames(typeof(TimeStep)).Length){
			timestepSetting = (TimeStep)index;
			btnText.text = Enum.GetNames (typeof(TimeStep)) [index];
			CreateTimesteps ((int)timestepSetting);
		}
	}

	public void CreateTimestepsPreviousUnit(Text btnText){
		
		int index = (int)timestepSetting - 1;

		if (index >= 0) {
			timestepSetting = (TimeStep)index;
			btnText.text = Enum.GetNames (typeof(TimeStep)) [index];
			CreateTimesteps ((int)timestepSetting);
		}
	}
		
	public void CreateTimesteps(int type){
		unitsDisplay.text = ((TimeStep)type).ToString();
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
		} else {
			//Debug.Log ("Timestep too dense");
			GameObject timeStepMarker = Instantiate (timeStepTooDenseMarker, timeStepContainer, false);
		}
	}

	double GetTimePassed(){

		double seconds = (selectedMovementBehaviour.distanceToStart / ((float)selectedMovementBehaviour.kilometersPerSecond/1000000f));
		double minutes = ((selectedMovementBehaviour.distanceToStart / ((float)selectedMovementBehaviour.kilometersPerSecond/1000000f * 60f)));
		double hours = ((selectedMovementBehaviour.distanceToStart / ((float)selectedMovementBehaviour.kilometersPerSecond/1000000f * 60f * 60f)));
		double days = ((selectedMovementBehaviour.distanceToStart / ((float)selectedMovementBehaviour.kilometersPerSecond/1000000f * 60f * 60f * 24f)));
		double months = ((selectedMovementBehaviour.distanceToStart / ((float)selectedMovementBehaviour.kilometersPerSecond/1000000f * 60f * 60f * 24f * 30f)));
		double years = ((selectedMovementBehaviour.distanceToStart / ((float)selectedMovementBehaviour.kilometersPerSecond/1000000f * 60f * 60f * 24f * 365f)));

		seconds = Mathf.Abs((int)seconds%60);
		minutes = Mathf.Abs((int)minutes%60);
		hours = Mathf.Abs((int)hours%24);
		days = Mathf.Abs((int)(days%30.436875));
		months = Mathf.Abs((int)months%12);
		years = Mathf.Abs((int)years);

		secondsPassed.text = seconds.ToString();
		minutesPassed.text = minutes.ToString();
		hoursPassed.text = hours.ToString();
		daysPassed.text = days.ToString();
		monthsPassed.text = months.ToString();
		yearsPassed.text = years.ToString();


		//Debug.Log (" years: " + years + " months: " + months + " days: " + days + " hours: " + hours + " minutes: " + minutes + " seconds: " + seconds);
		return seconds;
	}


	public float maxSpeed = 0f;
	public float maxWeight = 0f;
	public float maxPayload = 0f;
	public float maxThrust = 0f;
	public float maxSize = 0f;

	public void StartJourneyAllShips(){
		switchToRegularPlanetSystemView ();

		if (startPlanet != null && destinationPlanet != null) {
			foreach (RectTransform t in spaceshipPositionContainer) {
				Destroy (t.gameObject);
			}
			
			foreach (MovementBehaviour mb in movementBehaviourSpaceships) {
				// TODO: GETTER/SETTER
				if (mb.kilometersPerSecond > maxSpeed) {
					maxSpeed = (float)mb.kilometersPerSecond;
				}
				if (mb.weightKilogramm > maxWeight) {
					maxWeight = mb.weightKilogramm;
				}
				if (mb.payloadKilogramm > maxPayload) {
					maxPayload = mb.payloadKilogramm;
				}
				if (mb.thrustKiloNewton > maxThrust) {
					maxThrust = mb.thrustKiloNewton;
				}
				if (mb.sizeMeters > maxSize) {
					maxSize = mb.sizeMeters;
				}

				mb.SetStartAndDestination (startPlanet, destinationPlanet);
				startPlanetMarker.GetComponent<TagBehaviour> ().owner = startPlanet.gameObject;
				destinationPlanetMarker.GetComponent<TagBehaviour> ().owner = destinationPlanet.gameObject;
				if (mb != selectedMovementBehaviour) {
					mb.GetComponentInChildren<TrailCoordinatorBehaviour> ().SetColorGradient (gradientSpaceshipUnselected);

					GameObject spaceShipPositionMarkerTmp = Instantiate (currentSpaceshipPositionMarker, spaceshipPositionContainer, false);
					spaceShipPositionMarkerTmp.GetComponent<TagBehaviour> ().owner = mb.gameObject;
					mb.SetSpaceshipPositionMarker (spaceShipPositionMarkerTmp);

				} else {
					mb.GetComponentInChildren<TrailCoordinatorBehaviour> ().SetColorGradient (gradientSpaceshipSelected);

					GameObject spaceShipPositionMarkerTmp = Instantiate (currentSpaceshipPositionMarker, spaceshipPositionContainer, false);
					spaceShipPositionMarkerTmp.GetComponent<TagBehaviour> ().owner = mb.gameObject;
					mb.SetSpaceshipPositionMarker (spaceShipPositionMarkerTmp);
				}

				SelectSpaceship (selectedMovementBehaviour.gameObject);
				Camera.main.GetComponent<CameraFollowBehaviour> ().ResetFocus (false);
				mb.StartJourney ();
			}
		

		CreateTimesteps((int)timestepSetting);
		CreatePlanetMarkersInTimeline();
		}
	}

	public void CreatePlanetMarkersInTimeline(){
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
					planetMarker.GetComponent<TagBehaviour> ().owner = planet;
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
					planetMarker.GetComponent<TagBehaviour> ().owner = planet;
				}
			}
		}
	}

	public void OnPointerDown(PointerEventData data){
		Debug.Log (data.pointerCurrentRaycast.gameObject);
		GameObject selectedObject = data.pointerCurrentRaycast.gameObject;
		if (selectedObject.GetComponent<TagBehaviour> () != null) {
			GameObject owner = selectedObject.GetComponent<TagBehaviour> ().owner;
			if (owner != null && selectedObject.transform.parent == spaceshipPositionContainer || selectedObject.transform.parent == spacecraftSelectionSet) {
				selectedObject.GetComponent<RectTransform> ().SetAsFirstSibling();
				SelectSpaceship (owner);
			}else if(owner != null && (selectedObject.transform.parent == planetMarkerContainer || selectedObject.transform.parent == bordersContainer)){
				Camera.main.GetComponent<CameraFollowBehaviour>().SetObjectToFollow (owner);
			}
		}
	}

	void SelectSpaceship(GameObject spaceship){
		foreach (MovementBehaviour mb in movementBehaviourSpaceships) {
			mb.GetComponentInChildren<TrailCoordinatorBehaviour>().SetColorGradient(gradientSpaceshipUnselected);
			mb.GetComponentInChildren<TrailCoordinatorBehaviour> ().SetToBack ();
		}

		selectedMovementBehaviour = spaceship.GetComponent<MovementBehaviour>();
		selectedMovementBehaviour.GetComponentInChildren<TrailCoordinatorBehaviour>().SetColorGradient(gradientSpaceshipSelected);
		selectedMovementBehaviour.GetComponentInChildren<TrailCoordinatorBehaviour> ().SetToFront ();

		GameObject selectedMarker = null;
		foreach (Transform spaceshipMarker in spaceshipPositionContainer) {
			if (spaceshipMarker.GetComponent<TagBehaviour> ().owner == spaceship) {
				spaceshipMarker.GetComponent<Image> ().color = colorSelected;
				selectedMarker = spaceshipMarker.gameObject;
			} else {
				GameObject owner = spaceshipMarker.GetComponent<TagBehaviour> ().owner;
				spaceshipMarker.GetComponent<Image> ().color = colorUnselected;
				//spaceshipMarker.GetComponent<RectTransform> ().SetAsFirstSibling ();
			}
		}
		if (selectedMarker != null) {
			selectedMarker.GetComponent<RectTransform> ().SetAsLastSibling ();
		}
		CreateTimesteps((int)timestepSetting);
		Camera.main.GetComponent<CameraFollowBehaviour>().SetObjectToFollow(selectedMovementBehaviour.gameObject);

	}


	public GameObject planetSystemOverview;

	// Update is called once per frame
	GameObject highlightedGameObject;
	void Update () {
		GetTimePassed ();

		// Check which object has been clicked

		if (Input.GetMouseButtonDown (0)) {
			RaycastHit[] hit;
			hit = Physics.RaycastAll (Camera.main.ScreenPointToRay (Input.mousePosition), 2000f);
			if (hit.Length > 0) {

				foreach (RaycastHit h in hit) {
					Debug.Log ("Length: " + hit.Length + "Object: " + h.collider.gameObject.name);
					if (h.collider.gameObject.layer == 8) {
						SelectSpaceship (h.collider.gameObject);
					}

					if (h.collider.gameObject.tag == "SourceAndDestinationPlanet") {
						GameObject[] planets = coordinateSystemCreator.planets.ToArray ();

						foreach (GameObject startP in planets) {
							if (h.collider.gameObject.name == startP.name) {
								
								if (startPlanet == null) {
									startPlanet = startP.transform;
								} else {
									destinationPlanet = startP.transform;
								}

								if (startPlanet && destinationPlanet != null) {
									StartJourneyAllShips ();
								}
							}
						}
					}
				}
			}
		}

		RaycastHit[] hits;
		hits = Physics.RaycastAll (Camera.main.ScreenPointToRay (Input.mousePosition), 2000f);
		if (hits.Length > 0) {

			foreach (RaycastHit h in hits) {
				if (h.collider.gameObject.layer == 8) {
					Debug.Log ("over spaceship");
					DisplayDetailsBehaviour dd = h.collider.gameObject.GetComponent<DisplayDetailsBehaviour>();
					highlightedGameObject = h.collider.gameObject;
					if (dd != null) {
						dd.ShowDetails ();
					}
				} else if (h.collider.gameObject.tag == "SourceAndDestinationPlanet") {
					Debug.Log ("over planet");
					DisplayDetailsBehaviour dd = h.collider.gameObject.GetComponent<DisplayDetailsBehaviour>();
					highlightedGameObject = h.collider.gameObject;
					if (dd != null) {
						dd.ShowDetails ();
					}
				} else if (h.collider.gameObject.tag == "Planets") {
					Debug.Log ("over planet travel mode");
					DisplayDetailsBehaviour dd = h.collider.gameObject.GetComponent<DisplayDetailsBehaviour>();
					highlightedGameObject = h.collider.gameObject;
					if (dd != null) {
						dd.ShowDetails ();
					}
				}
			}
		}else{
			if (detailsDisplay != null) {
				if (detailsDisplay.activeSelf == true){
					if (highlightedGameObject != null) {
						detailsDisplay.SetActive (false);
						highlightedGameObject = null;
					}
				}
			}
		}
	


		// Apply Time Scaling to all available Spaceships TODO: do it more performant instead
		foreach (MovementBehaviour mb in movementBehaviourSpaceships) {
			mb.SetTimeMultiplier(timeMultiplier);
			if (mb.distanceToStart != 0) {
				GameObject marker = mb.GetSpaceshipPositionMarker ();
				if (marker != null) {
					marker.GetComponent<RectTransform> ().anchoredPosition = new Vector2 ((float)(mb.distanceToStart / mb.distanceComplete) * timelineContainer.rect.width, 0);
				}

				if (mb.GetDestinationOnRight () == true) {
					currentCameraPositionMarker.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (timelineContainer.rect.width * ((Camera.main.transform.position.x - startPlanet.transform.position.x) / (float)selectedMovementBehaviour.distanceComplete), 0);
				}else{
					currentCameraPositionMarker.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (timelineContainer.rect.width * (-(Camera.main.transform.position.x - startPlanet.transform.position.x) / (float)selectedMovementBehaviour.distanceComplete), 0);
				}
			}

		}
			
	}

	public void RestartScene(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().name.ToString(),LoadSceneMode.Single);
	}

}
