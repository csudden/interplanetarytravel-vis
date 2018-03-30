using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagBehaviour : MonoBehaviour {

	public GameObject owner;

	LineRenderer lr;
	void Start(){
		lr = GetComponent<LineRenderer> ();
		if (lr != null) {
			owner = gameObject.transform.parent.gameObject;
		}

		if (gameObject.layer == 8) {
			owner = gameObject;
		}
	}
}
