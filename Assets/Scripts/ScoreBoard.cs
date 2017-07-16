using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {

	public Text txtRef;
	private int score = 0;

	public void AddPoint()
	{
		score++;
	}

	public void Update()
	{
		txtRef.text = "Score: " + score.ToString();
	}
}
