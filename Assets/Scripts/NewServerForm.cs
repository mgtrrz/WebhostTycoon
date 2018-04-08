using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class NewServerForm : MonoBehaviour {

	public InputField hostname;
	public Dropdown serverTypeDropdown;
	public Text serverTypeHelper;
	public Dropdown serverEnclosureDropdown;
	public Dropdown cpuDropdown;
	public Dropdown softwareDropdown;
	public Dropdown hardDriveDropdown;
	public Dropdown hardDriveCapacityDropdown;
	public Text serverInfoDisplay;
	public Text serverCostDisplay;


	public GameObject serverPrefab;
	private GameObject serverParent;


	//private GameManager.gameManager GameManager.gameManager;

	//List<string> serverTypes = new List<string>() {"Desktop", "Workstation", "Server1U", "Server2U", "Storage4U"};
	List<string> serverType = new List<string>();
	List<string> serverEnclosureTypes = new List<string>();
	List<string> cpuTypes = new List<string>();
	List<string> hardDriveTypes = new List<string>();
	List<string> hardDriveCapacityTypes = new List<string>();
	List<string> software = new List<string>();

	// Use this for initialization
	void Start () {
		serverParent = GameObject.Find("Servers");
		//GameManager.gameManager = FindObjectOfType<GameManager.gameManager>();

		
		foreach ( ServerType st in GameManager.gameManager.allServerTypes ) {
			serverType.Add(st.name);
		}
		

		
		foreach ( ServerChassis sc in GameManager.gameManager.allServerChassis) {
			serverEnclosureTypes.Add(sc.name + " ($" + sc.cost + ")");
		}
		
		foreach ( CPU cpu in GameManager.gameManager.allCpus ) {
			cpuTypes.Add(cpu.name + " ($" + cpu.cost + ")");
		}

		foreach ( StorageDrive hd in GameManager.gameManager.allStorageDrives ) {
			hardDriveTypes.Add(hd.name + " ($" + hd.cost + ")");
		}

		foreach ( Software sw in GameManager.gameManager.allSoftware ) {
			software.Add(sw.name + " ($" + sw.monthlyCost + "/mo)");
		}

		serverTypeDropdown.ClearOptions();
		serverTypeDropdown.AddOptions(serverType);

		serverEnclosureDropdown.ClearOptions();
		serverEnclosureDropdown.AddOptions(serverEnclosureTypes);

		cpuDropdown.ClearOptions();
		cpuDropdown.AddOptions(cpuTypes);

		softwareDropdown.ClearOptions();
		softwareDropdown.AddOptions(software);

		hardDriveDropdown.ClearOptions();
		hardDriveDropdown.AddOptions(hardDriveTypes);
		
		hostname.text = NameGenerator.hostnameGenerator();

		UpdateHdCapacity();
		UpdateServerTypeHelperText();
	}

	public void OnEnable() {
		hostname.text = NameGenerator.hostnameGenerator();
	}

	public void UpdateHdCapacity() {
		// Clear the list
		hardDriveCapacityTypes.Clear();

		for(int i=1; i <= GameManager.gameManager.allServerChassis[serverEnclosureDropdown.value].hardDriveCapacity; i++) {
			hardDriveCapacityTypes.Add("x" + i);
		}
		hardDriveCapacityDropdown.ClearOptions();
		hardDriveCapacityDropdown.AddOptions(hardDriveCapacityTypes);
		UpdateServerCostDisplay();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateServerInformationDisplay();
		UpdateServerCostDisplay();
	}

	public void CloseForm() {
		Destroy(gameObject);
	}

	void UpdateServerInformationDisplay() {
		var cores = GameManager.gameManager.allCpus[cpuDropdown.value].cores;
		var logicalCores = cores;
		if ( GameManager.gameManager.allCpus[cpuDropdown.value].hyperthreaded ) {
			logicalCores = cores * 2;
		}
		serverInfoDisplay.text = cpuTypes[cpuDropdown.value] + "\n" 
							   + cores + "/" + logicalCores + "\n" 
							   + GameManager.gameManager.allServerChassis[serverEnclosureDropdown.value].hardDriveCapacity;
	}

	void UpdateServerCostDisplay() {

		serverCostDisplay.text = "$" + UpfrontCost + "\n"
							   + "$" + MonthlyRentalCost + "\n"
							   + "$0";
	}

	public int UpfrontCost {
		get { 
			int cost = 0;

			cost += GameManager.gameManager.allCpus[cpuDropdown.value].cost;
			cost += GameManager.gameManager.allServerChassis[serverEnclosureDropdown.value].cost;
			cost += GameManager.gameManager.allStorageDrives[hardDriveDropdown.value].cost * ( hardDriveCapacityDropdown.value + 1 );
			cost += GameManager.gameManager.allSoftware[softwareDropdown.value].monthlyCost;

			return cost;
		}
	}

	public int MonthlyRentalCost {
		get { 
			int cost = 0;
			cost += GameManager.gameManager.allSoftware[softwareDropdown.value].monthlyCost;

			return cost;
		}
	}

	public void UpdateServerTypeHelperText() {
		serverTypeHelper.text = GameManager.gameManager.allServerTypes[serverTypeDropdown.value].description;
	}

	public void PurchaseServer() {
		// If we can't afford this server, back out. Also give a warning/error message
		if ( !GameManager.gameManager.MakePurchase(UpfrontCost) ) {
			GameManager.gameManager.ShowDialogueBox("You do not have enough money to make this purchase!", "Error");
			return;
		}

		// We're purchasing the server! Here, we're going to instantiate the server from the prefab and make it a child
		// of the "Servers" parent to keep things tidy.
		var newServer = Instantiate(serverPrefab, Vector2.zero, Quaternion.identity, serverParent.transform) as GameObject;
		var serverComponent = newServer.GetComponent<Server>();

		serverComponent.hostname = hostname.text;
		serverComponent.serverChassis = GameManager.gameManager.allServerChassis[serverEnclosureDropdown.value];
		serverComponent.processor = GameManager.gameManager.allCpus[cpuDropdown.value];
		serverComponent.software = GameManager.gameManager.allSoftware[softwareDropdown.value];

		serverComponent.originalServerCost = UpfrontCost;
		serverComponent.originalBuildDate = GameManager.gameManager.GetCurrentGameDate();
		serverComponent.serverType = GameManager.gameManager.allServerTypes[serverTypeDropdown.value];

		serverComponent.acceptCustomers = true;

		for(int i = 1; i <= (hardDriveCapacityDropdown.value + 1); i++) {
			serverComponent.hardDrives.Add(GameManager.gameManager.allStorageDrives[hardDriveDropdown.value]);
		}

		// Adding it to our GameManager.gameManager (Or is this necessary?)
		GameManager.gameManager.servers.Add(serverComponent);
		CloseForm();

		ServerInfoUI sui = FindObjectOfType<ServerInfoUI>();
		sui.UpdateServerInfoDisplay();

	}
}
