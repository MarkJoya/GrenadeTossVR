using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO:
//spawn each prefab with a property of its original location
//then when destroyed retrieve that location and spawn it there?
public class TargetScript : MonoBehaviour {

	private Vector3 startPosition;
	private Quaternion startRotation;

	// Use this for initialization
	void Start () {
		startPosition = transform.position;
		startRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Vector3 GetStartPosition()
	{
		return this.startPosition;
	}

	public Quaternion GetStartRotation()
	{
		return this.startRotation;
	}
}
