using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DisplayDetailsBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {



	public GameObject detailsPanel;

	public Text spaceshipName;
	public Text speed;
	public Text size;
	public Text weight;
	public Text payload;
	public Text thrust;

	TimelineCoordinatorBehaviour tcb;
	// Use this for initialization
	void Start () {
		tcb = GameObject.Find ("Timeline").GetComponent<TimelineCoordinatorBehaviour>();
	}

	public void OnPointerExit (PointerEventData eventData)
	{
		detailsPanel.SetActive (false);
	}

	public void OnPointerEnter (PointerEventData eventData)
	{
		detailsPanel.SetActive (true);
		if (tcb.selectedMovementBehaviour != null) {
			spaceshipName.text = tcb.selectedMovementBehaviour.name;
			speed.text = tcb.selectedMovementBehaviour.kilometersPerSecond.ToString("N") + "km/s";
			size.text = tcb.selectedMovementBehaviour.sizeMeters.ToString("N") + "m";
			weight.text = tcb.selectedMovementBehaviour.weightKilogramm.ToString("N0") + "kg";
			payload.text = tcb.selectedMovementBehaviour.payloadKilogramm.ToString("N0") + "kg";
			thrust.text = tcb.selectedMovementBehaviour.thrustKiloNewton.ToString("N0") + "kN";
		}
	}
}
