using UnityEngine;
using System.Collections;

public class DynamicEvade : BaseAIController {
	public void SetTarget(GameObject target){
		this.m_target = target;
	}

	// Update is called once per frame
	void Update () {
		if (m_target == null) {
			return;
		}
		/*
		 * 1. Linear Acceleration = character.position - target.position
		 * 2. Clip to max acceleration
		 * 3. Clip to max speed
		 * 4. Angular Acceleration = 0
		 * */

		//1. Linear Acceleration = character.position - target.position
		Vector3 direction = (this.transform.position - m_target.transform.position + m_target.GetComponent<Rigidbody> ().velocity).normalized;
		transform.rotation = Quaternion.LookRotation(direction);

		m_rigidbody.AddForce (Vector3.ClampMagnitude (direction * this.m_maxAccelerationMagnitude, Mathf.Abs (this.m_maxAccelerationMagnitude)));


		//3. Clip to max speed
		if (m_rigidbody.velocity.magnitude > m_velocityLimit) {
			m_rigidbody.velocity = m_rigidbody.velocity.normalized * m_velocityLimit;
		}

		//4. Angular Acceleration = 0
		m_rigidbody.angularVelocity = Vector3.zero;
	}
}
