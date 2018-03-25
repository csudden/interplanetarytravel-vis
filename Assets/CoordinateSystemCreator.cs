using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoordinateSystemCreator : MonoBehaviour {

	public GameObject planetIndicatorLine;
	public GameObject planetName;
	public GameObject coordinateSystem;
	public GameObject planetNameContainer;
	// Use this for initialization
	void Start () {
		int i = 0;
		foreach (Transform t in gameObject.transform) {
			++i;
			GameObject objIndicator = Instantiate(planetIndicatorLine, t.position, Quaternion.Euler(-90,0,0),coordinateSystem.transform);
			Vector3 offsetText = new Vector3 (t.position.x, t.position.y, t.position.z);
			GameObject objName = Instantiate(planetName, offsetText, Quaternion.Euler(0,0,0),planetNameContainer.transform);

			if (i % 2 == 0) {
				objName.transform.position = new Vector3 (t.position.x, t.position.y, t.position.z);
			} else {
				objName.transform.position = new Vector3 (t.position.x, t.position.y+objName.transform.lossyScale.y*15, t.position.z);
			}

			objName.GetComponent<TextMesh>().text = t.gameObject.name;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
