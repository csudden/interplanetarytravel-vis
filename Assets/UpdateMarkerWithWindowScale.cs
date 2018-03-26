using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UpdateMarkerWithWindowScale : UIBehaviour {

	TimelineCoordinatorBehaviour tcb;
	// Use this for initialization
	void Start () {
		tcb = gameObject.GetComponent<TimelineCoordinatorBehaviour> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected override void OnRectTransformDimensionsChange(){
		base.OnRectTransformDimensionsChange ();
		Debug.Log ("I've been resized");
		if (tcb != null) {
			tcb.CreateTimesteps ();
		}
	}
}
