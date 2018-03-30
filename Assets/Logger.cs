using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Logger : MonoBehaviour {

	public Text logTextbox;

	// Use this for initialization
	void Start () {
		logTextbox.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddLogEntry(string message) {
		logTextbox.text += message + "\n";
	}
}
