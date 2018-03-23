using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerDetails : MonoBehaviour {

	public Server server;

	public Text hostname;
	public Text serverDetailsLeftTextbox;
	public Text serverDetailsRightTextbox;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		UpdateServerDetails();
	}

	void UpdateServerDetails() {
		if ( server == null ) { return; }

		hostname.text = server.hostname;

		serverDetailsLeftTextbox.text = server.processorName + "\n" +
										server.cpuCores + "/" + server.logicalCores + "\n" +
										server.cpuUsage.ToString("0.#\\%") + "\n" +
										"n/a" + "\n\n" +
										server.customers.Count + "\n" +
										"n/a" + "\n\n" +
										server.serverCustomerSatisfaction;
		
		serverDetailsRightTextbox.text = "n/a" + "\n" +
										 "n/a" + "\n" +
										 server.diskUsage.ToString("0.#\\") + " GB / " + server.GetTotalDiskSpace + " GB" + "\n" +
										 server.CalculateDiskPercentage().ToString("0.#\\%") + "\n\n" +
										 GameDate.GetMonthNameFromInt(server.originalBuildDate["Month"]) + " " + server.originalBuildDate["Day"] + " Year: " + server.originalBuildDate["Year"] + "\n" +
										 "$" + server.originalServerCost;
	}

	public void CloseServerDetailsWindow() {
		Destroy(gameObject);
	}
}
