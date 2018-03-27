using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SourceTargetSelectionIllustrationBehaviour : MonoBehaviour {

	GameObject sourceSelector;
	GameObject targetSelector;
	GameObject timeline;
	TimelineCoordinatorBehaviour tcb;
	// Use this for initialization
	void Start () {
		sourceSelector = GameObject.Find ("SourceSelector");
		targetSelector = GameObject.Find ("TargetSelector");
		timeline = GameObject.Find ("Timeline");
		if (timeline != null) {
			tcb = timeline.GetComponent<TimelineCoordinatorBehaviour> ();
		}

		StartCoroutine (LateStart (0.01f));
	}

	IEnumerator LateStart(float waitTime){
		yield return new WaitForSeconds (waitTime);
		if (tcb.destinationPlanet == null) {
			targetSelector.SetActive (false);
		}
	}
	
	void OnMouseOver(){
		if (sourceSelector != null && targetSelector != null) {
			if (tcb.startPlanet == null) {
				sourceSelector.SetActive (true);
				sourceSelector.transform.position = new Vector3 (gameObject.transform.position.x, sourceSelector.transform.position.y, sourceSelector.transform.position.z);
			}
			if (tcb.startPlanet != null) {
				targetSelector.SetActive (true);
				targetSelector.transform.position = new Vector3 (gameObject.transform.position.x, targetSelector.transform.position.y, targetSelector.transform.position.z);
			}
		}
	}

	void OnMouseExit(){
		if (sourceSelector != null && targetSelector != null) {
			if (tcb.startPlanet == null) {
				sourceSelector.SetActive (false);
			}

			if (tcb.destinationPlanet == null) {
				targetSelector.SetActive (false);
			}
		}
	}
}
