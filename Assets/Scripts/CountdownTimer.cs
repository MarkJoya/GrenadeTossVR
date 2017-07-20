using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour {

	public Text txtRef;
	private double timeLeft;

	public void Start()
	{
		timeLeft = 60;
	}
	
	// Update is called once per frame
	public void Update()
	{
		if (timeLeft > 0)
		{
			timeLeft -= Time.deltaTime;
		}
		txtRef.text = timeLeft.ToString("F0");
	}

	//public void Time
}
