using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerTypeDropdown : MonoBehaviour {

	private Dropdown serverType;
	// Use this for initialization
	void Start () {
		serverType = GetComponent<Dropdown>();
		serverType.ClearOptions();
		List<string> serverTypes = new List<string>() {"Desktop", "Workstation", "Server1U", "Server2U", "Storage4U"};
		serverType.AddOptions(serverTypes);


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
