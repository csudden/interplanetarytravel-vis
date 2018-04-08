using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoordinateSystemCreator : MonoBehaviour {

	public GameObject planetIndicatorLine;
	public GameObject planetName;
	public GameObject coordinateSystem;
	public GameObject planetNameContainer;
	public List<GameObject> planets = new List<GameObject>();

	// Use this for initialization
	void Start () {
		
		int i = 0;
		planets.Clear();

		foreach (Transform t in gameObject.transform) {
			++i;
			GameObject objIndicator = Instantiate(planetIndicatorLine, t.position, Quaternion.Euler(-90,0,0),coordinateSystem.transform);

			Vector3 offsetText = new Vector3 (t.position.x, t.position.y+0.1f, t.position.z);
			GameObject objName = Instantiate(planetName, Vector3.zero, Quaternion.Euler(0,0,0),planetNameContainer.transform);

			planets.Add(t.gameObject);

			string name = t.gameObject.name;
			if (name == "Sun") {
				objName.transform.position = new Vector3 (t.position.x, t.position.y + objName.transform.lossyScale.y + 0.6f, t.position.z);
			} else if (name == "Moon") {
				objName.transform.position = new Vector3 (t.position.x, t.position.y + objName.transform.lossyScale.y + 6f, t.position.z);
			}else{
				objName.transform.position = new Vector3 (t.position.x-objName.transform.lossyScale.y*10f, t.position.y+objName.transform.lossyScale.y*60f, t.position.z);
			}

			objName.GetComponent<TextMesh>().text = name;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
