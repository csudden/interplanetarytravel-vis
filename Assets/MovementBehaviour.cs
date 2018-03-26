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

	public double kilometersPerSecond = 2.91f;
	private float timeMultiplier = 1f;
	// Use this for initialization
	void Start () {
		boxCollider = GetComponent<BoxCollider>();
	}

	public void StartJourney () {
		startPosition = new Vector3(startPlanet.position.x, startPlanet.position.y, 0);
		endPosition = new Vector3(destinationPlanet.position.x, destinationPlanet.position.y, 0);
		distanceToStart = 0;
		GetComponentInChildren<TrailCoordinatorBehaviour>().ResetTrail ();
		gameObject.transform.position = new Vector3(startPlanet.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
	}

	public void StartJourney (Transform _startPlanet, Transform _destinationPlanet) {
		startPlanet = _startPlanet;
		destinationPlanet = _destinationPlanet;
		startPosition = new Vector3(startPlanet.position.x, startPlanet.position.y, 0);
		endPosition = new Vector3(destinationPlanet.position.x, destinationPlanet.position.y, 0);
		distanceToStart = 0;
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

	private BoxCollider boxCollider;
	float time = 0f;
	// Update is called once per frame
	void Update () {
		if (destinationPlanet != null && startPlanet != null) {
			// Update Box Collider
			boxCollider.size = new Vector3 ((float)distanceToStart, boxCollider.size.y, boxCollider.size.z);
			boxCollider.center = new Vector3 (-(float)distanceToStart / 2, boxCollider.center.y, boxCollider.center.z);

			currentPosition = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, 0);

			//distanceToStart =  Mathf.Abs(currentPosition.x - startPosition.x);
			distanceToDestination = Mathf.Abs (endPosition.x - currentPosition.x);
			distanceComplete = Mathf.Abs (endPosition.x - startPosition.x);

			time += Time.deltaTime;
			if (time > 1) {
				//Debug.Log (transform.position.x);
			}
			
			if (distanceToDestination > 0) {
				distanceToStart += (kilometersPerSecond / 1000000d) * Time.deltaTime * timeMultiplier;
			}

			if (distanceComplete > distanceToStart) {
				if (destinationPlanet.position.x > startPlanet.position.x) {
					transform.localPosition = (new Vector3 (startPlanet.position.x + (float)distanceToStart, transform.localPosition.y, transform.localPosition.z));
					transform.localEulerAngles = new Vector3 (0, 0, 0);
				} else {
					transform.localPosition = (new Vector3 (startPlanet.position.x - (float)distanceToStart, transform.localPosition.y, transform.localPosition.z));
					transform.localEulerAngles = new Vector3 (0, 180, 0);
				}
			} else {
				gameObject.transform.position = new Vector3 (destinationPlanet.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
			}
		}
	}
}
