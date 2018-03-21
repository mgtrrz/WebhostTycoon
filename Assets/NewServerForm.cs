using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewServerForm : MonoBehaviour {

	public InputField hostname;
	public Dropdown serverTypeDropdown;
	public Dropdown cpuDropdown;
	public Dropdown softwareOption;
	public Dropdown hardDriveDropdown;
	public Dropdown hardDriveCapacityDropdown;
	public Text serverInfoDisplay;
	public Text serverCostDisplay;


	public GameObject serverPrefab;
	private GameObject serverParent;


	private GameManager gameManager;

	//List<string> serverTypes = new List<string>() {"Desktop", "Workstation", "Server1U", "Server2U", "Storage4U"};
	List<string> serverTypes = new List<string>();
	List<string> cpuTypes = new List<string>();
	List<string> hardDriveTypes = new List<string>();
	List<string> hardDriveCapacityTypes = new List<string>();
	List<string> software = new List<string>();

	// Use this for initialization
	void Start () {
		serverParent = GameObject.Find("Servers");
		gameManager = FindObjectOfType<GameManager>();
		
		foreach ( ServerChassis sc in gameManager.allServerChassis) {
			serverTypes.Add(sc.name + " ($" + sc.cost + ")");
		}
		
		foreach ( CPU cpu in gameManager.allCpus ) {
			cpuTypes.Add(cpu.name + " ($" + cpu.cost + ")");
		}

		foreach ( StorageDrive hd in gameManager.allStorageDrives ) {
			hardDriveTypes.Add(hd.name + " ($" + hd.cost + ")");
		}

		foreach ( Software sw in gameManager.allSoftware ) {
			software.Add(sw.name + " ($" + sw.cost + "/mo)");
		}


		serverTypeDropdown.ClearOptions();
		serverTypeDropdown.AddOptions(serverTypes);

		cpuDropdown.ClearOptions();
		cpuDropdown.AddOptions(cpuTypes);

		softwareOption.ClearOptions();
		softwareOption.AddOptions(software);

		hardDriveDropdown.ClearOptions();
		hardDriveDropdown.AddOptions(hardDriveTypes);
		

		UpdateHdCapacity();
	}

	public void UpdateHdCapacity() {
		// Clear the list
		hardDriveCapacityTypes.Clear();

		for(int i=1; i <= gameManager.allServerChassis[serverTypeDropdown.value].hardDriveCapacity; i++) {
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
		gameObject.SetActive(false);
	}

	void UpdateServerInformationDisplay() {
		var cores = gameManager.allCpus[cpuDropdown.value].cores;
		var logicalCores = cores;
		if ( gameManager.allCpus[cpuDropdown.value].hyperthreaded ) {
			logicalCores = cores * 2;
		}
		serverInfoDisplay.text = cpuTypes[cpuDropdown.value] + "\n" 
							   + cores + "/" + logicalCores + "\n" 
							   + gameManager.allServerChassis[serverTypeDropdown.value].hardDriveCapacity;
	}

	void UpdateServerCostDisplay() {

		serverCostDisplay.text = "$" + UpfrontCost + "\n"
							   + "$" + MonthlyRentalCost + "\n"
							   + "$0";
	}

	public int UpfrontCost {
		get { 
			int cost = 0;

			cost += gameManager.allCpus[cpuDropdown.value].cost;
			cost += gameManager.allServerChassis[serverTypeDropdown.value].cost;
			cost += gameManager.allStorageDrives[hardDriveDropdown.value].cost * ( hardDriveCapacityDropdown.value + 1 );
			cost += gameManager.allSoftware[softwareOption.value].cost;

			return cost;
		}
	}

	public int MonthlyRentalCost {
		get { 
			int cost = 0;
			cost += gameManager.allSoftware[softwareOption.value].cost;

			return cost;
		}
	}

	public void PurchaseServer() {
		// If we can't afford this server, back out. Also maybe give a warning/error message
		if ( !gameManager.MakePurchase(UpfrontCost) ) {
			return;
		}

		// We're purchasing the server! Here, we're going to instantiate the server from the prefab and make it a child
		// of the "Servers" parent to keep things tidy.
		var newServer = Instantiate(serverPrefab, Vector2.zero, Quaternion.identity, serverParent.transform) as GameObject;
		var serverComponent = newServer.GetComponent<Server>();

		serverComponent.hostname = hostname.text + "." + gameManager.domain + gameManager.companyTld;
		serverComponent.serverChassis = gameManager.allServerChassis[serverTypeDropdown.value];
		serverComponent.processor = gameManager.allCpus[cpuDropdown.value];
		serverComponent.acceptCustomers = true;

		for(int i = 1; i <= (hardDriveCapacityDropdown.value + 1); i++) {
			serverComponent.hardDrives.Add(gameManager.allStorageDrives[hardDriveDropdown.value]);
		}

		// Adding it to our GameManager (Or is this necessary?)
		gameManager.servers.Add(serverComponent);
		CloseForm();

		ServerInfoUI sui = FindObjectOfType<ServerInfoUI>();
		sui.UpdateServerInfoDisplay();

	}
}
