using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour {

	public Transform startPlanet;
	public Transform destinationPlanet;

	private Vector3 startPosition;
	private Vector3 currentPosition;
	private Vector3 endPosition;

	public double distanceToStart;
	public double distanceToDestination;
	public double distanceComplete;

	private LineRenderer lineRenderer;

	private GameObject spaceshipPositionMarker;

	public void SetSpaceshipPositionMarker(GameObject marker){
		spaceshipPositionMarker = marker;
	}

	public GameObject GetSpaceshipPositionMarker(){
		return spaceshipPositionMarker;
	}

	[Header("Spaceship Attributes")]
	public double kilometersPerSecond = 2.91f;
	public float weightKilogramm;
	public float payloadKilogramm;
	public float thrustKiloNewton;
	public float sizeMeters;

	private float timeMultiplier = 1f;
	// Use this for initialization
	void Start () {
		boxCollider = GetComponent<BoxCollider>();
		lineRenderer = GetComponentInChildren<LineRenderer> ();
	}

	public void StartJourney () {
		startPosition = new Vector3(startPlanet.position.x, startPlanet.position.y, 0);
		endPosition = new Vector3(destinationPlanet.position.x, destinationPlanet.position.y, 0);

		distanceToStart = 0;
		distanceToDestination = Mathf.Abs (endPosition.x - currentPosition.x);
		distanceComplete = Mathf.Abs (endPosition.x - startPosition.x);

		GetComponentInChildren<TrailCoordinatorBehaviour>().ResetTrail ();
		gameObject.transform.position = new Vector3(startPlanet.position.x, gameObject.transform.position.y, gameObject.transform.position.z);

		InitializeLineRenderer ();
		UpdateLineRenderer ();
	}

	public void InitializeLineRenderer(){
		if (lineRenderer != null) {
			lineRenderer.SetPosition (0, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, -0.65f));
		}
	}

	public void UpdateLineRenderer(){
		if (startPlanet != null) {
			if (lineRenderer != null) {
				lineRenderer.SetPosition (1, new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,lineRenderer.GetPosition(1).z));
			}
		}
	}

	public void StartJourney (Transform _startPlanet, Transform _destinationPlanet) {
		startPlanet = _startPlanet;
		destinationPlanet = _destinationPlanet;
		startPosition = new Vector3(startPlanet.position.x, startPlanet.position.y, 0);
		endPosition = new Vector3(destinationPlanet.position.x, destinationPlanet.position.y, 0);

		distanceToStart = 0;
		distanceToDestination = Mathf.Abs (endPosition.x - currentPosition.x);
		distanceComplete = Mathf.Abs (endPosition.x - startPosition.x);

		GetComponentInChildren<TrailCoordinatorBehaviour>().ResetTrail ();
		gameObject.transform.position = new Vector3(startPlanet.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
	}

	public void SetTimeMultiplier(float multiplier){
		timeMultiplier = multiplier;
	}

	public void SetStartPlanet(Transform _startPlanet){
		startPlanet = _startPlanet;
	}

	public void SetDestinationPlanet(Transform _destinationPlanet){
		destinationPlanet = _destinationPlanet;
	}

	public void SetStartAndDestination(Transform _startPlanet, Transform _destinationPlanet){
		SetStartPlanet (_startPlanet);
		SetDestinationPlanet (_destinationPlanet);
	}

	private BoxCollider boxCollider;
	float time = 0f;
	bool destinationOnRight = true;

	public bool GetDestinationOnRight(){
		return destinationOnRight;
	}
		
	// Update is called once per frame
	float paddingX = 0.003f;
	void Update () {
		if (destinationPlanet != null && startPlanet != null) {

			// Update Box Collider

			boxCollider.size = new Vector3 ((float)distanceToStart+paddingX, boxCollider.size.y, boxCollider.size.z);
			boxCollider.center = new Vector3 (-(float)distanceToStart / 2, boxCollider.center.y, boxCollider.center.z);

			currentPosition = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, 0);

			//distanceToStart =  Mathf.Abs(currentPosition.x - startPosition.x);


			if (destinationOnRight) {
				distanceToDestination = ((double)endPosition.x - (double)(startPosition.x+distanceToStart));
			} else {
				distanceToDestination = -((double)endPosition.x - (double)(startPosition.x-distanceToStart));
			}

			//Debug.Log ("Dist Dest:" + distanceToDestination);
			distanceComplete = Mathf.Abs (endPosition.x - startPosition.x);

			time += Time.deltaTime;
			if (time > 1) {
				//Debug.Log (transform.position.x);
			}

			if (distanceToDestination > 0d) {
				distanceToStart += (kilometersPerSecond / 1000000d) * (double)Time.fixedDeltaTime * (double)timeMultiplier;
			}
				
			if (distanceComplete > distanceToStart) {
				if (destinationPlanet.position.x > startPlanet.position.x) {
					transform.position = (new Vector3 (startPlanet.position.x + (float)distanceToStart, transform.position.y, transform.position.z));
					transform.localEulerAngles = new Vector3 (0, 0, 0);
					destinationOnRight = true;
				} else {
					transform.position = (new Vector3 (startPlanet.position.x - (float)distanceToStart, transform.position.y, transform.position.z));
					transform.localEulerAngles = new Vector3 (0, 180, 0);
					destinationOnRight = false;
				}
			} else {
				gameObject.transform.position = new Vector3 (destinationPlanet.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
				distanceToStart = distanceComplete;
				distanceToDestination = 0d;
			}


			UpdateLineRenderer ();
		}
	}
}
