using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour {

	public Text txtRef;
	public GameObject scoreTextObject;

	private double timeLeft = 15;

	private bool timerOn = false;
	
	public void Update()
	{
		//Display instructions if timer is not on
		if (!timerOn)
		{
			txtRef.text = "Squeeze yellow box to start";
		}
		//Show time remaining if timer is started
		else if (timeLeft > 0)
		{
			timeLeft -= Time.deltaTime;
			txtRef.text = timeLeft.ToString("F2");
		}
		//When time runs out, show final score
		else
		{
			int score = scoreTextObject.GetComponent<ScoreBoard>().GetScore();
			txtRef.text = "Score: " + score.ToString();
		}
	}

	public double GetTimeLeft()
	{
		return timeLeft;
	}

	public void StartTimer()
	{
		timerOn = true;
	}

	public bool IsTimerOn()
	{
		return timerOn;
	}
}
