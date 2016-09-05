using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour {
	[SerializeField]private float m_lifeTime;
	// Use this for initialization
	void Start () {
		StartCoroutine (this.WaitToKill(m_lifeTime));
	}

	private IEnumerator WaitToKill(float lifeTime){
		yield return new WaitForSeconds (lifeTime);
		Destroy (this.gameObject);
	}
}
