using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ServerDetails : MonoBehaviour {
 
	public int recommendedCustomersPerThread;
	public Server server;

	private int recommendedCustomers;
	public Text hostname;
	public Text serverDetailsLeftTextbox;
	public Text serverDetailsRightTextbox;
	public Text serverDetailsRevenueInfo;
	public InputField maxCustomersTextbox;
	public Text recommendedCustomersStatic;
	public Toggle acceptNewCustomers;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		UpdateServerDetails();
	}

	void CalculateRecommendedCustomers() {
		recommendedCustomers = recommendedCustomersPerThread * server.logicalCores;
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

		recommendedCustomersStatic.text = recommendedCustomers.ToString();
	}

	public void ToggleAcceptCustomers() {
		server.acceptCustomers = acceptNewCustomers.isOn;
	}

	public void UpdateMaxCustomers() {
		server.maxCustomers = Int32.Parse(maxCustomersTextbox.text);
		// print(maxCustomersTextbox.text);
	}

	public void CloseServerDetailsWindow() {
		Destroy(gameObject);
	}
}
