using UnityEngine;
using System.Collections;

public class MoveTarget : MonoBehaviour {
	[SerializeField]private float m_timeBetweenMoves;
	[SerializeField]private float m_minLerpTime;
	[SerializeField]private float m_maxLerpTime;
	[SerializeField]private float m_radius;
	[SerializeField]private GameObject m_circleCenter;
	[SerializeField]private GameObject m_targetPosition;
	[SerializeField]private GameObject m_lastPosition;

	// Use this for initialization
	void Start () {
		StartCoroutine (KeepMovingTarget(m_timeBetweenMoves,m_minLerpTime,m_maxLerpTime,m_radius,m_circleCenter,m_targetPosition,m_lastPosition));
	}

	private IEnumerator KeepMovingTarget(float timeBetweenMoves, float minLerpTime, float maxLerpTime, float radius, GameObject circleCenter, GameObject targetPosition, GameObject lastPosition){
		while (true) {
			yield return new WaitForSeconds (timeBetweenMoves);
			float lerpTime = Random.Range (minLerpTime, maxLerpTime);

			lastPosition.transform.position = this.transform.position;
			Vector3 newMovement = new Vector3 (Random.Range (-radius, radius), 0, Random.Range (-radius, radius)).normalized * radius;

			targetPosition.transform.position = m_circleCenter.transform.position + newMovement;
			float tPassed = 0.0f;

			while (tPassed < lerpTime) {
				tPassed += Time.deltaTime;
				transform.position = Vector3.Lerp (lastPosition.transform.position, targetPosition.transform.position, tPassed / lerpTime);
				yield return new WaitForFixedUpdate ();
			}
		}
	}
}
