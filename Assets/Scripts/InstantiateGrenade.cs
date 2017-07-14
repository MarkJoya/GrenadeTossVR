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
		Instantiate(grenadePrefab, this.transform.position, this.transform.rotation);
	
	}
	
	public void OnTriggerExit(Collider other)
	{
		Debug.Log("EXIT");
		if (other.CompareTag("Grenade"))
		{
			//CreateGrenade();
		}
	}

}
