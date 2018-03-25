using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineCoordinatorBehaviour : MonoBehaviour {

	public RectTransform timelineContainer;
	public GameObject timeStepPrefab;
	public int timeStep = 100;
	// Use this for initialization
	void Start () {
		
	}

	public void CreateTimesteps(){
		foreach (RectTransform t in timelineContainer){
			Destroy (t.gameObject);
		}

		float containerSize = timelineContainer.rect.width;

		for (int i = timeStep; i < containerSize; i += timeStep) {
			GameObject timeStep = Instantiate (timeStepPrefab, timelineContainer, false);
			timeStep.GetComponent<RectTransform> ().anchoredPosition = new Vector2(i,0);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
