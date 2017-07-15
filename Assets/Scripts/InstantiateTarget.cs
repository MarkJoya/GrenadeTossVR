using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateTarget : MonoBehaviour {

	public GameObject targetPrefab;
	private Vector3 startPosition;
	private Quaternion startRotation;

	private GameObject targetClone;
	private float TARGET_RESPAWN_TIME = 2f;

	private bool isOccupied;
	private bool targetDestroyed;
	private bool resumeUpdate = true;

	// Use this for initialization
	void Start()
	{
		startPosition = transform.position;
		startRotation = transform.rotation;
		CreateTarget();
	}

	void Update()
	{
		targetDestroyed = isDestroyed(targetClone);
		if (resumeUpdate && targetDestroyed)
		{
			StartCoroutine(CreateTargetWithDelay());
			resumeUpdate = false;
		}
	}



	public void CreateTarget()
	{
		targetClone = Instantiate(targetPrefab, startPosition, startRotation);
	}

	private IEnumerator CreateTargetWithDelay()
	{
		yield return new WaitForSecondsRealtime(TARGET_RESPAWN_TIME);
		CreateTarget();
		resumeUpdate = true;

	}

	// UnityEngine overloads the == opeator for the GameObject type
	// and returns null when the object has been destroyed, but 
	// actually the object is still there but has not been cleaned up yet
	// if we test both we can determine if the object has been destroyed.
	private static bool isDestroyed(GameObject gObject)
	{
		return gObject == null && !ReferenceEquals(gObject, null);
	}

	/*
	public void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Target"))
		{
			isOccupied = false;
		}
	}

	public void OnTriggerEntry(Collider other)
	{
		if (other.CompareTag("Target"))
		{
			isOccupied = true;
		}
	}

	public void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Target"))
		{
			isOccupied = true;
		}
	}
	*/


}
