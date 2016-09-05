using UnityEngine;
using System.Collections;

public class DynamicWander : BaseAIController {
	[SerializeField]private GameObject m_target;
	// Update is called once per frame
	void Update () {
		Vector3 direction = (m_target.transform.position - this.transform.position).normalized;

		m_rigidbody.AddForce (direction * m_maxAccelerationMagnitude);

		if (m_rigidbody.velocity.magnitude > m_velocityLimit) {
			m_rigidbody.velocity = m_rigidbody.velocity.normalized * m_velocityLimit;
		}

		m_rigidbody.angularVelocity = Vector3.zero;
		Vector3 lookAtPos = m_target.transform.position;

		lookAtPos.y = transform.position.y;
		this.transform.LookAt (lookAtPos);
	}
}
