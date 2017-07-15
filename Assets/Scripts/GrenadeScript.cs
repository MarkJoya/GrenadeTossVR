using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeScript : MonoBehaviour {

	private bool timerSet;

	// Use this for initialization
	void Start ()
	{
		timerSet = false;	
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	// Sets timer to indicate that this grenade's explosion timer is ticking
	public void SetTimer()
	{
		timerSet = true;
	}

	public bool IsTimerSet()
	{
		return timerSet;
	}
}
