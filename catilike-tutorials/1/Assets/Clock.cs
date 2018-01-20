using System;
using UnityEngine;

public class Clock : MonoBehaviour {

	public Transform hoursTransform, minutesTransform,  secondsTransform;
	const float degreesPerHour = 30f;
	const float degresPerMinute = 6f;
	const float degresPerSecond = 6f;
	
	public bool continuous;

	void UpdateDiscrete () {
		DateTime time = DateTime.Now;
		hoursTransform.rotation = 
			Quaternion.Euler(0f, time.Hour * degreesPerHour, 0f);
		minutesTransform.rotation = 
			Quaternion.Euler(0f, time.Minute * degresPerMinute, 0f);
		secondsTransform.rotation = 
			Quaternion.Euler(0f, time.Second * degresPerSecond, 0f);
	}

	void UpdateContinuous () {
		TimeSpan time = DateTime.Now.TimeOfDay;
		hoursTransform.rotation = 
			Quaternion.Euler(0f, (float)time.TotalHours * degreesPerHour, 0f);
		minutesTransform.rotation = 
			Quaternion.Euler(0f, (float)time.TotalMinutes * degresPerMinute, 0f);
		secondsTransform.rotation = 
			Quaternion.Euler(0f, (float)time.TotalSeconds * degresPerSecond, 0f);
	}

	void Update () {
		if(continuous) {
			UpdateContinuous();
		} else {
			UpdateDiscrete();
		}
	}
}
