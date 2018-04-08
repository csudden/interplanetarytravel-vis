using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleRendering : MonoBehaviour {

	public LineRenderer[] lr;
	public MeshRenderer[] mr;

	// Use this for initialization
	void Start () {
		StartCoroutine ("LateStart");
	}

	IEnumerator LateStart() {
		yield return new WaitForEndOfFrame();
		lr = GetComponentsInChildren<LineRenderer> ();
		mr = GetComponentsInChildren<MeshRenderer> ();
		//yield return null;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ToggleActiveState(){
		gameObject.SetActive (!gameObject.activeSelf);
	}

	public void Show(){
		
		foreach (LineRenderer l in lr) {
			l.enabled = true;
		}

		foreach (MeshRenderer m in mr) {
			m.enabled = true;
		}
	}

	public void Hide(){
		foreach (LineRenderer l in lr) {
			l.enabled = false;
		}

		foreach (MeshRenderer m in mr) {
			m.enabled = false;
		}
	}

	public void Toggle(){
		foreach (LineRenderer l in lr) {
			l.enabled = !l.enabled;
			Debug.Log (l.name);
		}

		foreach (MeshRenderer m in mr) {
			m.enabled = !m.enabled;
			Debug.Log (m.name);
		}
	}
}
