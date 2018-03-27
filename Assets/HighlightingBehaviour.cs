using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HighlightingBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	Color initialColor;
	Image img;

	public Color highlightingColor;

	MeshRenderer mr;
	void Start(){
		
		img = GetComponent<Image> ();
		mr = GetComponent<MeshRenderer> ();
		if (img != null) {
			initialColor = GetComponent<Image> ().color;
		}

		if (mr != null) {
			initialColor = mr.material.color;
		}
	}

	#region IPointerEnterHandler implementation
	public void OnPointerEnter (PointerEventData eventData)
	{
		if (img != null) {
			img.color = highlightingColor;
		}
	}
	#endregion

	void OnMouseOver(){
		if (mr != null) {
			mr.material.color = highlightingColor;
		}
	}

	void OnMouseExit(){
		if (mr != null) {
			mr.material.color = initialColor;
		}
	}

	#region IPointerExitHandler implementation

	public void OnPointerExit (PointerEventData eventData)
	{
		if (img != null) {
			img.color = new Color (initialColor.r, initialColor.g, initialColor.b);
		}

		if (mr != null) {
			mr.material.color = initialColor;
		}
	}

	#endregion
}
