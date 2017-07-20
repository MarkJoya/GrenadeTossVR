using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateTarget : MonoBehaviour {

	public GameObject targetPrefab;
	public GameObject timerText;
	private Vector3 startPosition;
	private Quaternion startRotation;

	private GameObject targetClone;
	private float TARGET_RESPAWN_TIME = 2f;

	private bool targetDestroyed;
	private bool resumeUpdate = true;

	// Use this for initialization
	void Start()
	{
		startPosition = transform.position;
		startRotation = transform.rotation;
		CreateTarget();
	}

	// Called every frame
	// Checks if the target has been destroyed and creates a new one
	// Main functionality is paused until the target has been destroyed - prevents spawning every frame
	void Update()
	{
		targetDestroyed = isDestroyed(targetClone);
		CountdownTimer script = timerText.GetComponent<CountdownTimer>();
		double timeLeft = script.GetTimeLeft();
		/*
		if (script.IsPreTimerOn() && targetDestroyed)
		{
			CreateTarget();
			resumeUpdate = false;
		}
		else */if (resumeUpdate && targetDestroyed && timeLeft > 0)
		{
			StartCoroutine(CreateTargetWithDelay());
			resumeUpdate = false;
		}
	}

	// Creates a new target at spawn location
	public void CreateTarget()
	{
		targetClone = Instantiate(targetPrefab, startPosition, startRotation);
		//resumeUpdate = true;
	}

	// Creates a new target after specified delay time
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
}
