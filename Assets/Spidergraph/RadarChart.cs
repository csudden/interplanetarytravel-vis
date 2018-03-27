using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadarChart : MonoBehaviour {

	[SerializeField]
	private Image[] m_panels;
	[SerializeField]
	private float m_fullSize = 100f;
	[SerializeField]
	private float[] m_statusValues;

	public TimelineCoordinatorBehaviour tcb;

	void OnValidate() {
		if (m_statusValues.Length != m_panels.Length) {
			return;
		}

		for (int i = 0; i < 5; i++) {
			SetValue (i, m_statusValues[i]);
		}
	}

	MovementBehaviour movementBehaviour;
	void Update(){
			if (tcb != null) {
				if (tcb.selectedMovementBehaviour != null && tcb.selectedMovementBehaviour != movementBehaviour) {
					movementBehaviour = tcb.selectedMovementBehaviour;
					Debug.Log ("Chart updated");
					float p = 10f;
					float multiplier = 1f;
					float offset = 1.5f;
					if (tcb.maxSize != 0 && tcb.maxThrust != 0 && tcb.maxPayload != 0 && tcb.maxSpeed != 0 && tcb.maxWeight != 0) {
						SetValue (0, (float)(Mathf.Log (10 * ((multiplier * tcb.selectedMovementBehaviour.sizeMeters / tcb.maxSize)) + offset, p)));
						SetValue (1, (float)(Mathf.Log (10 * ((multiplier * tcb.selectedMovementBehaviour.thrustKiloNewton / tcb.maxThrust)) + offset, p)));
						SetValue (2, (float)(Mathf.Log (10 * ((multiplier * tcb.selectedMovementBehaviour.payloadKilogramm / tcb.maxPayload)) + offset, p)));
						SetValue (3, (float)(Mathf.Log (10 * ((multiplier * (float)tcb.selectedMovementBehaviour.kilometersPerSecond / tcb.maxSpeed)) + offset, p)));
						SetValue (4, (float)(Mathf.Log (10 * ((multiplier * tcb.selectedMovementBehaviour.weightKilogramm / tcb.maxWeight)) + offset, p)));
					}
				}
			}
	}

	public void SetValue (int index, float value) {
		m_statusValues[index] = value;

		Vector2 size = m_panels[index].rectTransform.sizeDelta;
		size.x = m_fullSize * value;
		m_panels[index].rectTransform.sizeDelta = size;

		int pre = (index + m_panels.Length - 1) % m_panels.Length;
		size = m_panels[pre].rectTransform.sizeDelta;
		size.y = m_fullSize * value;
		m_panels[pre].rectTransform.sizeDelta = size;
	}
}
