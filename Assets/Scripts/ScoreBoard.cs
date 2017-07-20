using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {

	public Text txtRef;
	public GameObject timerTextObject;
	private int score;

	public void Start()
	{
		score = 0;
	}

	public void AddPoint()
	{
		//Don't increment score if game is finished
		if (timerTextObject.GetComponent<CountdownTimer>().GetTimeLeft() > 0)
		{
			score++;
		}
	}

	public void Update()
	{
		txtRef.text = "Score: " + score.ToString();
	}

	public int GetScore()
	{
		return this.score;
	}
}
