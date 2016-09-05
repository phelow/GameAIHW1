using UnityEngine;
using System.Collections;

public class BaseAIController : MonoBehaviour {
	protected GameObject m_target;
	private Animator m_animator;							// a reference to the animator on the character
	[SerializeField]protected Rigidbody m_rigidbody;
	[SerializeField]protected float m_maxAccelerationMagnitude;
	[SerializeField]protected float m_velocityLimit;

	// Use this for initialization
	void Start () {
		m_animator = gameObject.GetComponent<Animator>();	
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LateUpdate(){
		m_animator.SetFloat("Speed", m_rigidbody.velocity.magnitude);
	}

	protected GameObject FindNearestHuman(){
		return FindNearestTarget ("Human", this.transform.position);
	}

	protected virtual GameObject FindNearestTarget(string targetLabel, Vector3 position){
		GameObject closestTarget = null;
		float minDist = Mathf.Infinity;
		foreach(GameObject go in GameObject.FindGameObjectsWithTag (targetLabel)){
			float distance = Vector3.Distance (position, go.transform.position);
			if (distance < minDist) {
				minDist = distance;
				closestTarget = go;
			}
		}

		return closestTarget;
	}
}
