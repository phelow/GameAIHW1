using UnityEngine;
using System.Collections;

public class Deparent : MonoBehaviour {
	[SerializeField]private GameObject m_psuedoParent;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = m_psuedoParent.transform.position + m_psuedoParent.transform.forward * 100;
	}
}
