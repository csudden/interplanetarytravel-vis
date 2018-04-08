using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DisplayDetailsBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {



	public DetailPanelBehaviour detailsPanel;
	RectTransform detailsPanelRect;
	TimelineCoordinatorBehaviour tcb;
	// Use this for initialization
	TagBehaviour tb;
	MovementBehaviour tbOwner;
	MovementBehaviour movementBehaviour;

	public bool showTagOwner;
	public bool showCustomText;
	public bool showObjectName;
	public string customTextTitle;
	public string customText;
	public float customHeight = 140f;

	void Start () {
		tcb = GameObject.Find ("Timeline").GetComponent<TimelineCoordinatorBehaviour>();
		tb = GetComponent<TagBehaviour> ();
		movementBehaviour = GetComponent<MovementBehaviour> ();
		if (tb != null) {
			if (tb.owner != null) {
				tbOwner = tb.owner.GetComponent<MovementBehaviour> ();
			}
		}

		if (detailsPanel == null) {
			detailsPanel = GetComponent<DetailPanelBehaviour> ();
		}

		if (detailsPanel != null) {
			detailsPanelRect = detailsPanel.GetComponent<RectTransform> ();
			detailsPanel.gameObject.SetActive (false);
		}
	}


	public void ShowDetails(){
		OnPointerEnter (null);
	}

	public void HideDetails(){
		OnPointerExit (null);
	}

	void OnMouseOver(){
		OnPointerEnter (null);
	}

	void OnMouseExit(){
		OnPointerExit (null);
	}

	public void OnPointerExit (PointerEventData eventData)
	{
		detailsPanel.gameObject.SetActive (false);
	}

	Rect rectTmp;
	public void OnPointerEnter (PointerEventData eventData)
	{
		//Debug.Log ("Entered");
		detailsPanel.gameObject.SetActive (true);
		//Debug.Log(detailsPanel.gameObject.activeSelf +  " active");
		if (showObjectName) {
			Debug.Log (gameObject.name);
			detailsPanelRect = detailsPanel.GetComponent<RectTransform> ();
			detailsPanelRect.sizeDelta = new Vector2(detailsPanelRect.sizeDelta.x,25);
			detailsPanel.titleText.text = gameObject.name;
			detailsPanel.detailText.text = "";
		}else if (showTagOwner) {
			detailsPanelRect = detailsPanel.GetComponent<RectTransform> ();
			detailsPanelRect.sizeDelta = new Vector2(detailsPanelRect.sizeDelta.x,25);
			detailsPanel.titleText.text = tb.owner.name;
			detailsPanel.detailText.text = "";
		}
		else if (showCustomText) {
			detailsPanelRect.sizeDelta = new Vector2(detailsPanelRect.sizeDelta.x,customHeight);
			detailsPanel.titleText.text = customTextTitle;
			detailsPanel.detailText.text = customText;
		}
		else if (tcb.selectedMovementBehaviour != null && tb == null) {
			detailsPanelRect.sizeDelta = new Vector2(detailsPanelRect.sizeDelta.x,120f);

			detailsPanel.titleText.text = tcb.selectedMovementBehaviour.name;

			detailsPanel.gameObject.SetActive (true);
			detailsPanel.detailText.text = "";
			detailsPanel.detailText.text += "      Speed:  " + (tcb.selectedMovementBehaviour.kilometersPerSecond*60d*60d).ToString ("N") + "km/h" + "\n";
			detailsPanel.detailText.text += "                       " + tcb.selectedMovementBehaviour.kilometersPerSecond.ToString ("N") + "km/s" + "\n";
			detailsPanel.detailText.text += "          Size:  " + tcb.selectedMovementBehaviour.sizeMeters.ToString ("N") + "m" + "\n";
			detailsPanel.detailText.text += "  Weight:  " + tcb.selectedMovementBehaviour.weightKilogramm.ToString ("N") + "kg" + "\n";
			detailsPanel.detailText.text += "Payload:  " + tcb.selectedMovementBehaviour.payloadKilogramm.ToString ("N") + "kg" + "\n";
			detailsPanel.detailText.text += "     Thrust:  " + tcb.selectedMovementBehaviour.thrustKiloNewton.ToString ("N") + "kN" + "\n";
		} else if (tb != null) {
			if (movementBehaviour != null) {
				detailsPanelRect.sizeDelta = new Vector2(detailsPanelRect.sizeDelta.x,190f);

				detailsPanel.titleText.text = tbOwner.name;
				detailsPanel.gameObject.SetActive (true);
				detailsPanel.detailText.text = "";
				detailsPanel.detailText.text += "Distance To Start:  \n" + (tbOwner.distanceToStart * 1000000d).ToString ("N") + "km" + "\n";
				detailsPanel.detailText.text += "Distance To Target:  \n" + (tbOwner.distanceToDestination * 1000000d).ToString ("N") + "km" + "\n";
				detailsPanel.detailText.text += "\n";
				detailsPanel.detailText.text += "Distance Complete:  \n" + (tbOwner.distanceComplete * 1000000d).ToString ("N") + "km" + "\n";
				detailsPanel.detailText.text += "\n";
				detailsPanel.detailText.text += "Max Speed:  \n";
				detailsPanel.detailText.text += (tbOwner.kilometersPerSecond*60d*60d).ToString ("N") + "km/h" + "\n";
				detailsPanel.detailText.text += tbOwner.kilometersPerSecond.ToString ("N") + "km/s" + "\n";
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
