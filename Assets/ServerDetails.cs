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
	public Text serverProfitTextbox;
	public InputField maxCustomersTextbox;
	public Text recommendedCustomersStatic;
	public Toggle acceptNewCustomers;
	public Button newDriveButton;

	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<GameManager>();
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

		StorageDrive drive = server.hardDrives[0];
		newDriveButton.GetComponentInChildren<Text>().text = "Add New Drive ($" + drive.cost + ")";

		serverDetailsLeftTextbox.text = server.processorName + "\n" +
										server.cpuCores + "/" + server.logicalCores + "\n" +
										server.cpuUsage.ToString("0.#\\%") + "\n" +
										"n/a" + "\n\n" +
										server.customers.Count + "\n" +
										"n/a" + "\n\n" +
										server.serverCustomerSatisfaction;
		
		serverDetailsRightTextbox.text = drive.name + "\n" +
										 drive.totalCapacity + " GB x" +  server.hardDrives.Count  + "\n" +
										 server.hardDriveCapacity + "\n" +
										 server.diskUsage.ToString("0.#\\") + " GB / " + server.GetTotalDiskSpace + " GB" + "\n" +
										 server.CalculateDiskPercentage().ToString("0.#\\%") + "\n\n\n" +
										 GameDate.GetMonthNameFromInt(server.originalBuildDate["Month"]) + " " + server.originalBuildDate["Day"] + " Year: " + server.originalBuildDate["Year"] + "\n" +
										 "$" + server.originalServerCost;

		serverDetailsRevenueInfo.text = "$" + server.serverIncome + "\n" +
									    "$" + server.serverCosts + "\n" +
										"n/a";

		serverProfitTextbox.text = "$" + (server.serverIncome - server.serverCosts);

		if ( server.hardDrives.Count >= server.hardDriveCapacity ) {
			newDriveButton.interactable = false;
		} else {
			newDriveButton.interactable = true;
		}

		if ( server.acceptCustomers ) {
			acceptNewCustomers.isOn = true;
		} else {
			acceptNewCustomers.isOn = false;
		}

		recommendedCustomersStatic.text = recommendedCustomers.ToString();
	}

	public void ToggleAcceptCustomers() {
		server.acceptCustomers = acceptNewCustomers.isOn;
	}

	public void UpdateMaxCustomers() {
		server.maxCustomers = Int32.Parse(maxCustomersTextbox.text);
		// print(maxCustomersTextbox.text);
	}

	public void AddStorageDrive() {
		StorageDrive drive = server.hardDrives[0];

		if ( gameManager.MakePurchase(drive.cost) ) {
			server.hardDrives.Add(drive);
		} else {
			// We couldn't afford to buy the drive, so provide an error message
		}

	}

	public void CloseServerDetailsWindow() {
		Destroy(gameObject);
	}
}
