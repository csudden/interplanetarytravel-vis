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

	public float kilometersPerSecond = 2.91f;
	// Use this for initialization
	void Start () {
		StartJourney ();
	}

	public void StartJourney () {
		startPosition = new Vector3(startPlanet.position.x, startPlanet.position.y, 0);
		endPosition = new Vector3(destinationPlanet.position.x, destinationPlanet.position.y, 0);
		distanceToStart = 0;
		GetComponentInChildren<TrailCoordinatorBehaviour>().ResetTrail ();
		gameObject.transform.position = new Vector3(startPlanet.position.x, startPlanet.position.y, gameObject.transform.position.z);
	}

	float time = 0f;
	// Update is called once per frame
	void Update () {
		currentPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);

		//distanceToStart =  Mathf.Abs(currentPosition.x - startPosition.x);
		distanceToDestination = Mathf.Abs(endPosition.x - currentPosition.x);
		distanceComplete = Mathf.Abs(endPosition.x - startPosition.x);

		time += Time.deltaTime;
		if (time > 1) {
			//Debug.Log (transform.position.x);
		}
			
		if (distanceToDestination > 0){
			distanceToStart += (kilometersPerSecond/1000000f) * Time.deltaTime;
		}

		if (distanceComplete > distanceToStart) {
			if (destinationPlanet.position.x > startPlanet.position.x) {
				transform.localPosition = (new Vector3 (startPlanet.position.x+(float)distanceToStart, 0, 0));
				transform.localEulerAngles = new Vector3 (0, 0, 0);
			} else {
				transform.localPosition = (new Vector3 (startPlanet.position.x-(float)distanceToStart, 0, 0));
				transform.localEulerAngles = new Vector3 (0, 180, 0);
			}
		} else {
			gameObject.transform.position = new Vector3(destinationPlanet.position.x, destinationPlanet.position.y, gameObject.transform.position.z);
		}
	}
}
