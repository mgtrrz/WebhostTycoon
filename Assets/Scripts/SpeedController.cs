using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedController : MonoBehaviour {

	public Button pause;
	public Button normal;
	public Button fast;
	public Button fastest;

	public Color32 pauseColor;
	public Color32 speedColor;

	public float fastSpeedTimeScale;
	public float fastestSpeedTimeScale;

	// Use this for initialization
	void Start () {
		ResetButtonColors();

		if ( Time.timeScale == 0f ) {
			pause.GetComponent<Image>().color = pauseColor;
		} else if ( Time.timeScale == 1f ) {
			normal.GetComponent<Image>().color = speedColor;
		} else if ( Time.timeScale == fastSpeedTimeScale ) {
			fast.GetComponent<Image>().color = speedColor;
		} else if ( Time.timeScale == fastestSpeedTimeScale ) {
			fastest.GetComponent<Image>().color = speedColor;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ResetButtonColors() {
		var whiteColor = new Color32(255,255,255,100);
		pause.GetComponent<Image>().color = whiteColor;
		normal.GetComponent<Image>().color = whiteColor;
		fast.GetComponent<Image>().color = whiteColor;
		fastest.GetComponent<Image>().color = whiteColor;
	}

	public void PauseSpeed() {
		// Reset all button colors first
		ResetButtonColors();
		// Change color of button
		pause.GetComponent<Image>().color = pauseColor;
		// Change time
		Time.timeScale = 0f;
	}

	public void NormalSpeed() {
		ResetButtonColors();
		normal.GetComponent<Image>().color = speedColor;
		Time.timeScale = 1f;
	}

	public void FastSpeed() {
		ResetButtonColors();
		fast.GetComponent<Image>().color = speedColor;
		Time.timeScale = fastSpeedTimeScale;
	}

	public void FastestSpeed() {
		ResetButtonColors();
		fastest.GetComponent<Image>().color = speedColor;
		Time.timeScale = fastestSpeedTimeScale;
	}
}
