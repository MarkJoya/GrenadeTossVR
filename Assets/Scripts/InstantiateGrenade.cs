using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateGrenade : MonoBehaviour {

	public GameObject grenadePrefab;
	public int SPAWN_LIMIT;

	private bool isOccupied;
	private int spawnedObjects = 0;

	private float currentTime;
	private float lastSpawnTime;

	// Use this for initialization
	void Start()
	{
		CreateGrenade();
	}

	void Update()
	{
		if (!isOccupied) CreateGrenade();
	}

	public void CreateGrenade()
	{
		if (spawnedObjects < SPAWN_LIMIT)
		{
			Instantiate(grenadePrefab, this.transform.position, this.transform.rotation);
			spawnedObjects++;
		}
	}
	
	public void OnTriggerExit(Collider other)
	{
		isOccupied = false;
	}

	public void OnTriggerEntry(Collider other)
	{
		isOccupied = true;
	}

	public void OnTriggerStay(Collider other)
	{
		isOccupied = true;
	}
	
}
