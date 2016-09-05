using UnityEngine;
using System.Collections;

public class SpawnPursuer : MonoBehaviour {
	[SerializeField] private GameObject m_spawnable;
	[SerializeField] private float m_spawnWaitTime;
	// Use this for initialization
	void Start () {
		StartCoroutine (Spawn (m_spawnWaitTime));
	}


	private IEnumerator Spawn(float spawnWaitTime){
		while (true) {
			yield return new WaitForSeconds (spawnWaitTime);
			GameObject.Instantiate(m_spawnable,transform.position,transform.rotation);
		}
	}
}
