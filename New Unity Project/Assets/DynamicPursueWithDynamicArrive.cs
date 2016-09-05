using UnityEngine;
using System.Collections;

public class DynamicPursueWithDynamicArrive : BaseAIController {
	[SerializeField]protected float m_slowDownRadius;
	[SerializeField]protected float m_minDistance;

	// Update is called once per frame
	void Update () {
		//find nearest target
		if (m_target == null) {
			m_target = this.FindNearestHuman ();
			DynamicEvade evade = m_target.GetComponent<DynamicEvade> ();
			if(evade != null){
				evade.SetTarget (gameObject);
			}
		}

		if (m_target == null) {
			return;
		}
		this.Pursue ();
	}

	public void Pursue(){
		/*	Dynamic Seek:
			1. Linear Acceleration = target.position - character.position
			2. Clip to max acceleration
			3. Clip to max speed
			4. Angular Acceleration = 0*/

		Vector3 futurePosition = m_target.transform.position + m_target.GetComponent<Rigidbody> ().velocity;
		futurePosition.y = 0;

		//1. Linear Acceleration = target.position - character.position
		float distance = Vector3.Distance (this.transform.position, futurePosition) - m_minDistance;

		//2. Clip to max acceleration
		float appliedAcceleration = Mathf.Lerp (m_maxAccelerationMagnitude, 0, (m_slowDownRadius - distance) / m_slowDownRadius);

		Vector3 direction = (futurePosition - this.transform.position).normalized;

		m_rigidbody.AddForce (Vector3.ClampMagnitude (direction * appliedAcceleration, Mathf.Abs (appliedAcceleration)));


		//3. Clip to max speed
		if (m_rigidbody.velocity.magnitude > m_velocityLimit) {
			m_rigidbody.velocity = m_rigidbody.velocity.normalized * m_velocityLimit;
		}

		//4. Angular Acceleration = 0
		m_rigidbody.angularVelocity = Vector3.zero;
		this.transform.LookAt (futurePosition);
	}
}
