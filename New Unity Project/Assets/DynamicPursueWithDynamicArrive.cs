using UnityEngine;
using System.Collections;

public class DynamicPursueWithDynamicArrive : BaseAIController {
	[SerializeField]protected float m_slowDownRadius;
	[SerializeField]protected float m_minDistance;
	[SerializeField]protected float m_maxAngularAcceleration = 10.0f;

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
		this.Arrive ();
	}

	public void Pursue(){
		/*	Dynamic Seek:
			1. Linear Acceleration = target.position - character.position
			2. Clip to max acceleration
			3. Clip to max speed
			4. Add angular velocity*/

		Vector3 futurePosition = m_target.transform.position + m_target.GetComponent<Rigidbody> ().velocity;
		futurePosition.y = 0;


		//2. Clip to max acceleration

		//calcualte rotation
		Vector3 direction = (futurePosition - this.transform.position).normalized;

		Vector3 headingDistance = Vector3.Cross (transform.forward + m_rigidbody.angularVelocity, direction);

		float angle = Mathf.Asin (Mathf.Clamp(headingDistance.magnitude,-1,1));

		Vector3 w = headingDistance.normalized * angle / Time.fixedDeltaTime;

		Quaternion q = gameObject.transform.rotation * m_rigidbody.inertiaTensorRotation;
		Vector3 Torque = Vector3.Scale (m_rigidbody.inertiaTensor, Quaternion.Inverse (q) * w);

		if (Torque.magnitude > m_maxAngularAcceleration) {
			Torque = Torque.normalized * m_maxAngularAcceleration;
		}

		m_rigidbody.AddTorque(Torque);

		m_rigidbody.AddForce (Vector3.ClampMagnitude (transform.forward * m_maxAccelerationMagnitude, Mathf.Abs (m_maxAccelerationMagnitude)));

	}

	public void Arrive(){
		Vector3 futurePosition = m_target.transform.position + m_target.GetComponent<Rigidbody> ().velocity;
		futurePosition.y = 0;
		//1. Linear Acceleration = target.position - character.position
		float distance = Vector3.Distance (this.transform.position, futurePosition) - m_minDistance;

		float velocityLimit = Mathf.Lerp (m_velocityLimit, 0, Mathf.Pow(Mathf.Max (m_slowDownRadius - distance, 0) / m_slowDownRadius,.1f));

		//3. Clip to max speed
		if (m_rigidbody.velocity.magnitude > velocityLimit) {
			m_rigidbody.velocity = m_rigidbody.velocity.normalized * velocityLimit;
		}
	}
}
