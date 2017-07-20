using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour {

	public Text txtRef;
	public GameObject scoreTextObject;

	private double timeLeft = 15;
	
	public void Update()
	{
		if (timeLeft > 0)
		{
			timeLeft -= Time.deltaTime;
			txtRef.text = timeLeft.ToString("F2");
		}
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
}
