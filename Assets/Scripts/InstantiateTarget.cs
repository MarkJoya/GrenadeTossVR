using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateTarget : MonoBehaviour {

	public GameObject targetPrefab;
	private Vector3 startPosition;
	private Quaternion startRotation;

	// Use this for initialization
	void Start()
	{
		startPosition = transform.position;
		startRotation = transform.rotation;
		CreateTarget();
	}

	public void CreateTarget()
	{
		Instantiate(targetPrefab, startPosition, startRotation);
	}
}
