using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateGrenade : MonoBehaviour {

	public GameObject grenadePrefab;

	// Use this for initialization
	void Start () {
		CreateGrenade();
	}

	public void CreateGrenade()
	{
		Instantiate(grenadePrefab, transform.position, transform.rotation);
	}
	
}
