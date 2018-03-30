using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DisplayDetailsBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {



	public DetailPanelBehaviour detailsPanel;
	TimelineCoordinatorBehaviour tcb;
	// Use this for initialization
	TagBehaviour tb;
	MovementBehaviour tbOwner;

	void Start () {
		tcb = GameObject.Find ("Timeline").GetComponent<TimelineCoordinatorBehaviour>();
		tb = GetComponent<TagBehaviour> ();
		if (tb != null) {
			tbOwner = tb.owner.GetComponent<MovementBehaviour>();
		}

		if (detailsPanel == null) {
			detailsPanel = GetComponent<DetailPanelBehaviour> ();
		}

		if (detailsPanel != null) {
			detailsPanel.gameObject.SetActive (false);
		}
	}


	public void ShowDetails(){
		OnPointerEnter (null);
	}

	public void HideDetails(){
		OnPointerExit (null);
	}

	public void OnPointerExit (PointerEventData eventData)
	{
		detailsPanel.gameObject.SetActive (false);
	}

	public void OnPointerEnter (PointerEventData eventData)
	{

		if (tcb.selectedMovementBehaviour != null && tb == null) {
			detailsPanel.titleText.text = tcb.selectedMovementBehaviour.name;

			detailsPanel.gameObject.SetActive (true);
			detailsPanel.detailText.text = "";
			detailsPanel.detailText.text += "      Speed:  " + tcb.selectedMovementBehaviour.kilometersPerSecond.ToString ("N") + "km/s" + "\n";
			detailsPanel.detailText.text += "          Size:  " + tcb.selectedMovementBehaviour.sizeMeters.ToString ("N") + "m" + "\n";
			detailsPanel.detailText.text += "  Weight:  " + tcb.selectedMovementBehaviour.weightKilogramm.ToString ("N") + "kg" + "\n";
			detailsPanel.detailText.text += "Payload:  " + tcb.selectedMovementBehaviour.payloadKilogramm.ToString ("N") + "kg" + "\n";
			detailsPanel.detailText.text += "     Thrust:  " + tcb.selectedMovementBehaviour.thrustKiloNewton.ToString ("N") + "kN" + "\n";
		} else if (tb != null) {
			if (tb.owner != null) {
				detailsPanel.titleText.text = tbOwner.name;
				detailsPanel.gameObject.SetActive (true);
				detailsPanel.detailText.text = "";
				detailsPanel.detailText.text += "Distance To Start:  \n" + (tbOwner.distanceToStart * 1000000d).ToString ("N") + "km" + "\n";
				detailsPanel.detailText.text += "Distance To Target:  \n" + (tbOwner.distanceToDestination * 1000000d).ToString ("N") + "km" + "\n";
				detailsPanel.detailText.text += "Distance Complete:  \n" + (tbOwner.distanceComplete * 1000000d).ToString ("N") + "km" + "\n";
				detailsPanel.detailText.text += "Speed:  " + tbOwner.kilometersPerSecond.ToString ("N") + "km/s" + "\n";
				/*
				detailsPanel.detailText.text += "          Size:  " + tbOwner.sizeMeters.ToString ("N") + "m" + "\n";
				detailsPanel.detailText.text += "  Weight:  " + tbOwner.weightKilogramm.ToString ("N") + "kg" + "\n";
				detailsPanel.detailText.text += "Payload:  " + tbOwner.payloadKilogramm.ToString ("N") + "kg" + "\n";
				detailsPanel.detailText.text += "     Thrust:  " + tbOwner.thrustKiloNewton.ToString ("N") + "kN" + "\n";
				*/
			}
		}
	}
}
