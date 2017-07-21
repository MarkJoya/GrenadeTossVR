using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour {

	public Text txtRef;
	public GameObject scoreTextObject;

	private static double PRE_TIME = 3;
	private static double GAME_TIME = 7;

	private double preTimeLeft = PRE_TIME;
	private double timeLeft = GAME_TIME;

	private bool timerOn = false;
	private bool preTimerOn = false;

	public void Update()
	{
		//Display instructions if timer is not on
		if (!timerOn)
		{
			txtRef.fontSize = 2;
			txtRef.text = "Squeeze yellow box to start";
		}
		//Show time remaining if timer is started
		else if (timeLeft > 0)
		{
			txtRef.fontSize = 5;
			if (preTimeLeft > 0)
			{
				preTimeLeft -= Time.deltaTime;
				txtRef.text = "Start in.. " + preTimeLeft.ToString("F2");
			}
			else
			{
				preTimerOn = false;
				timeLeft -= Time.deltaTime;
				txtRef.text = timeLeft.ToString("F2");
			}
		}
		//When time runs out, show final score
		else
		{
			txtRef.fontSize = 2;
			int score = scoreTextObject.GetComponent<ScoreBoard>().GetScore();
			txtRef.text = "Score: " + score.ToString() + "\n" + "Squeeze yellow box to restart";
		}
	}

	public double GetTimeLeft()
	{
		return timeLeft;
	}

	public void StartTimer()
	{
		preTimeLeft = PRE_TIME;
		timeLeft = GAME_TIME;
		scoreTextObject.GetComponent<ScoreBoard>().ResetScore();
		timerOn = true;
		preTimerOn = true;
	}

	public bool IsTimerOn()
	{
		return timerOn;
	}

	public bool IsPreTimerOn()
	{
		return preTimerOn;
	}
}
