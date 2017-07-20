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
		CountdownTimer script = timerTextObject.GetComponent<CountdownTimer>();

		//Don't increment score if game hasn't started or is finished
		if (script.IsTimerOn() && !script.IsPreTimerOn())
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

	public void ResetScore()
	{
		score = 0;
	}
}
