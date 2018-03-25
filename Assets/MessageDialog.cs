using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageDialog : MonoBehaviour {

	public Text headerText;
	public Text messageText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetMessage(string message, string header="Error") {
		messageText.text = message;
		headerText.text = header;
	}

	public void CloseWindow() {
		Destroy(gameObject);
	}
}
