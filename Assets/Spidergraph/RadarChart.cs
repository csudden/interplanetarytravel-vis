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

	void Update(){
		//if ((int)Time.time % 1 == 0) {
			if (tcb != null) {
				if (tcb.selectedMovementBehaviour != null) {
					// DIRTY PROC TO MAKE TINY VALUES VISIBLE IN SPIDERGRAPH TODO: REVISE IF THERE IS TIME
					float p = 10f;
					float multiplier = 1f;
					if (tcb.maxSize != 0 && tcb.maxThrust != 0 && tcb.maxPayload != 0 && tcb.maxSpeed != 0 && tcb.maxWeight != 0) {
					if ((tcb.selectedMovementBehaviour.sizeMeters / tcb.maxSize) >= 0.5f || (tcb.selectedMovementBehaviour.thrustKiloNewton / tcb.maxThrust) >= 0.5f || (tcb.selectedMovementBehaviour.payloadKilogramm / tcb.maxPayload) >= 0.5f || ((float)tcb.selectedMovementBehaviour.kilometersPerSecond / tcb.maxSpeed) >= 0.5f ||  (tcb.selectedMovementBehaviour.weightKilogramm / tcb.maxWeight) >= 0.5f) {
							SetValue (0, (float)(Mathf.Log (10 * ((multiplier * tcb.selectedMovementBehaviour.sizeMeters / tcb.maxSize)) + 1, p)));
							SetValue (1, (float)(Mathf.Log (10 * ((multiplier * tcb.selectedMovementBehaviour.thrustKiloNewton / tcb.maxThrust)) + 1, p)));
							SetValue (2, (float)(Mathf.Log (10 * ((multiplier * tcb.selectedMovementBehaviour.payloadKilogramm / tcb.maxPayload)) + 1, p)));
							SetValue (3, (float)(Mathf.Log (10 * ((multiplier * (float)tcb.selectedMovementBehaviour.kilometersPerSecond / tcb.maxSpeed)) + 1, p)));
							SetValue (4, (float)(Mathf.Log (10 * ((multiplier * tcb.selectedMovementBehaviour.weightKilogramm / tcb.maxWeight)) + 1, p)));
						}else {
							multiplier = 5f;
						Debug.Log ("huihGFFD");
							SetValue (0, (float)(Mathf.Log (10 * ((multiplier * tcb.selectedMovementBehaviour.sizeMeters / tcb.maxSize)) + 1, p)));
							SetValue (1, (float)(Mathf.Log (10 * ((multiplier * tcb.selectedMovementBehaviour.thrustKiloNewton / tcb.maxThrust)) + 1, p)));
							SetValue (2, (float)(Mathf.Log (10 * ((multiplier * tcb.selectedMovementBehaviour.payloadKilogramm / tcb.maxPayload)) + 1, p)));
							SetValue (3, (float)(Mathf.Log (10 * ((multiplier * (float)tcb.selectedMovementBehaviour.kilometersPerSecond / tcb.maxSpeed)) + 1, p)));
							SetValue (4, (float)(Mathf.Log (10 * ((multiplier * tcb.selectedMovementBehaviour.weightKilogramm / tcb.maxWeight)) + 1, p)));
						}
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
