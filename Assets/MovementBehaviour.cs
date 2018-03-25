using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour {

	public Transform startPlanet;
	public Transform destinationPlanet;

	private Vector3 startPosition;
	private Vector3 currentPosition;
	private Vector3 endPosition;

	public float distanceToStart;
	public float distanceToDestination;
	public float distanceComplete;

	public float speed = 0.05f;
	// Use this for initialization
	void Start () {
		StartJourney ();
	}

	public void StartJourney () {
		startPosition = new Vector3(startPlanet.position.x, startPlanet.position.y, 0);
		endPosition = new Vector3(destinationPlanet.position.x, destinationPlanet.position.y, 0);
		GetComponentInChildren<TrailCoordinatorBehaviour>().ResetTrail ();
		gameObject.transform.position = new Vector3(startPlanet.position.x, startPlanet.position.y, gameObject.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		currentPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);

		distanceToStart =  Mathf.Abs(currentPosition.x - startPosition.x)*1000000;
		distanceToDestination = Mathf.Abs(endPosition.x - currentPosition.x)*1000000;
		distanceComplete = Mathf.Abs(endPosition.x - startPosition.x)*1000000;
		if (distanceComplete > distanceToStart) {
			if (destinationPlanet.position.x > startPlanet.position.x) {
				transform.Translate (new Vector3 (speed, 0, 0));
				transform.localEulerAngles = new Vector3 (0, 0, 0);
			} else {
				transform.Translate (new Vector3 (speed, 0, 0));
				transform.localEulerAngles = new Vector3 (0, 180, 0);
			}
		} else {
			gameObject.transform.position = new Vector3(destinationPlanet.position.x, destinationPlanet.position.y, gameObject.transform.position.z);
		}
	}
}
