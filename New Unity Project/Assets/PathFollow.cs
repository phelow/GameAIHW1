using UnityEngine;
using System.Collections;

public class PathFollow : DynamicPursueWithDynamicArrive{
	// Update is called once per frame
	void Update () {
		/*
		 * 1: Predict character's location after a short period of time.
		 * 2: Map character's predicted position to the nearest point on the path.
		 * 3: Set the target some distance ahead
		 * 4: From the characer's predited position call seek to reach target
		 * */
		if (m_target == null || Vector3.Distance (this.transform.position, m_target.transform.position) <= m_minDistance) {
			//1: Predict character's location after a short period of time.
			Vector3 nextPosition = this.transform.position + m_rigidbody.velocity;

			//2: Map character's predicted position to the nearest point on the path.
			GameObject nearestNode = this.FindNearestTarget ("Node", nextPosition);
			//3: Set the target some distance ahead
			m_target = nearestNode.GetComponent<PathNode> ().m_nextNode;
		}



		//4: From the characer's predited position call seek to reach target
		this.Pursue ();
	}
}
