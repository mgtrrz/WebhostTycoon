using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager : MonoBehaviour {

	const string GameMode = "DEV";

	public static GameManager gameManager;

	/* ---------------- */
	/*       Clock      */
	/* ---------------- */
	private int hour;
	private int day;
	private int week;
	private int month;
	private int year;
	public float timeInterval;
	private float counter; /* MAKE PRIVATE */	

	public List<Server> servers;

	/* ------------------ */
	/*   Player Details   */
	/* ------------------ */
	public string playerName;
	public string companyName;
	public string domain;
	public string companyTld; // .com, .org, .net, etc.
	public string difficulty;
	public bool acceptingCustomers;
	public ServerEnvironment environment;
	public bool skipSetup;

	/* ------------------ */
	/* Player Performance */
	/* ------------------ */
	private int funds;
	public float popularity;
	public int satisfaction;
	private int nps;

	/* ---------------- */
	/*      Prefabs     */
	/* ---------------- */
	[Space]
	[Space]
	[Header("Server Component Prefabs")]
	public List<ServerChassis> allServerChassis;
	public List<CPU> allCpus;
	public List<StorageDrive> allStorageDrives;
	public List<CustomerType> allCustomerTypes;
	public List<Plan> allPlans;
	public List<Software> allSoftware;
	public Server serverPrefab;
	public Customer customerPrefab;


	/* ---------------- */
	/*    UI elements   */
	/* ---------------- */
	[Space]
	[Space]
	[Header("UI Elements")]
	public Text websiteName;
	public Text fundsTextbox;
	public Text hourTextbox;
	public Text dayMonthTextbox;
	public Text yearTextbox;

	public Text totalCustomersTextbox;
	public Text totalServersTextbox;
	public Text popularityTextbox;
	public Text satisfactionTextbox;
	public GameObject dialogueBox;

	public Text logTextbox;

	/* ---------------- */
	/*  Game Variables  */
	/* ---------------- */




	private CustomerParent customerParent;
	private ServerParent serverParent;

	public enum ServerEnvironment {
		Home, Provider, Datacenter, Cloud
	}

	void Awake() {
		// Singleton
		if (gameManager == null) {
			// Keep this persistent across all scenes
			DontDestroyOnLoad(gameObject);
			gameManager = this;
		} else if (gameManager != this) {
			Destroy(gameObject);
		}
	}
	void Start () {
		// Keep this persistent across all scenes

		customerParent = FindObjectOfType<CustomerParent>();
		serverParent = FindObjectOfType<ServerParent>();
		/* Initializing our Clock */
		day = 1;
		week = 1;
		month = 1;
		year = 1;
		funds = 1000;

		if ( GameMode == "DEV" ) {
			playerName = "Marcus Gutierrez";
			companyName = "LoudServers";
			domain = "loudservers";
			companyTld = ".com";
		}


		UpdateFundsDisplay();
		UpdateSummarizedDisplay();
	}
	
	void Update () {
		
	}

	void FixedUpdate() {
		if ( counter <= 0 ) {
			TickClock();
			counter = timeInterval;
		}

		counter -= Time.fixedDeltaTime;
	}

	public void TickClock() {
		hour++;

		if ( hour > 23 ) {
			day++;
			hour = 0;
			DailyTick();
		}

		if ( day > 28 ) {
			day = 1;
			month++;
			MonthlyTick();
		}

		if ( ( day == 1 || day == 8 || day == 15 || day == 22 ) && hour == 0 ) {
			week++;
		}

		if ( week > 48 ) {
			week = 1;
		}

		if ( month > 12 ) {
			month = 1;
			year++;
		}

		Tick();
	}

	public void UpdateFundsDisplay() {
		fundsTextbox.text = "$" + funds;
	}

	public void UpdateHourDisplay() {
		hourTextbox.text = hour.ToString();
	}

	public void UpdateDayMonthDisplay() {
		dayMonthTextbox.text = GameDate.GetMonthNameFromInt(month)  + " " + day;
	}
	
	public void UpdateYearDisplay() {
		yearTextbox.text = year.ToString();
	}

	public void UpdateSummarizedDisplay() {
		websiteName.text = domain + companyTld;

		totalCustomersTextbox.text = customerParent.transform.childCount.ToString();
		totalServersTextbox.text = servers.Count.ToString();

		popularityTextbox.text = popularity.ToString();
		satisfactionTextbox.text = satisfaction.ToString();
	}


	public void AddLogEntry(string message) {
		if ( logTextbox.cachedTextGenerator.lineCount > 8 ) {
			logTextbox.text = logTextbox.text.Substring(logTextbox.text.IndexOf('\n') + 1);
		}

		logTextbox.text += message + "\n";
	}

	public void Tick() {

		// Send a message to all of our servers to calculate their things!
		serverParent.BroadcastTick();
		// And our customers
		customerParent.BroadcastTick();

		CalculateCustomerTraction();
		CalculateCustomerSatisfaction();

		UpdateHourDisplay();
		UpdateDayMonthDisplay();
		UpdateYearDisplay();
		UpdateSummarizedDisplay();
	}
	
	public void DailyTick() {
		customerParent.BroadcastDailyTick();
	}

	public void MonthlyTick() {
		// for our monthly costs!
		serverParent.BroadcastMonthlyTick();
		customerParent.BroadcastMonthlyTick();
	}

	public bool MakePurchase(int amount) {
		// We have more money than the amount
		// they want to purchase, so we can allow it
		if ( funds >= amount ) {
			funds -= amount;
			UpdateFundsDisplay();
			return true;
		}
		return false;
	}

	public void MakeProfit(int amount) {
		funds += amount;
		UpdateFundsDisplay();
	}


	public void CalculateCustomerTraction() {

		// Determining if we get a customer in this tick
		if ( UnityEngine.Random.Range(0, 10000) < popularity * 10 ) {
			AddCustomer();
		}
	}

	private void CalculateCustomerSatisfaction() {
		if ( customerParent.transform.childCount == 0 ) {
			return;
		}

		int totalSatisfaction = 0;
		foreach (Transform customerGameObject in customerParent.transform) {
			totalSatisfaction += customerGameObject.GetComponent<Customer>().satisfaction;
		}

		satisfaction = totalSatisfaction / customerParent.transform.childCount;
	}
	
	private void CalculateCustomerPopularity() {

	}

	public Dictionary<string, int> GetCurrentGameDate() {
		var dateDict = new Dictionary<string, int>();
		dateDict.Add("Month", month);
		dateDict.Add("Day", day);
		dateDict.Add("Year", year);
		return dateDict;
	}

	public void AddCustomer() {
		if (!acceptingCustomers) { // if we're not accepting customers, bounce outta here
			return;
		}

		// Check to see if we have any servers first
		Server serverToUse = null;

		if ( servers != null && servers.Count != 0 ) {
			// Iterate through those servers
			foreach(Server server in servers) {
				// If the server is accepting customers..
				if ( server.acceptCustomers ) {
					// Check to see how many cutomers there are on the box
					// the server with the fewest amount of customers will
					// be the lucky candidate
					if ( serverToUse != null ) {
						if ( serverToUse.customers.Count >= server.customers.Count ) {
							serverToUse = server;
						}
					} else {
						serverToUse = server;
					}

				}
			}

			GameObject serverParent = GameObject.Find("Customers");
			// Add them in there
			Customer customer = Instantiate(customerPrefab, Vector3.zero, Quaternion.identity, serverParent.transform);

			string[] gender = new string[] {"male", "female"};

			string randGend = gender[UnityEngine.Random.Range(0,2)];

			customer.age = UnityEngine.Random.Range(18, 115);

			customer.customerName = NameGenerator.generateRandomName(randGend);
			customer.cxType = CalculateCustomerType();
			customer.dateJoined = GetCurrentGameDate();

			customer.myServer = serverToUse;

			// Picking a plan at UnityEngine.Random for now
			int iRand = UnityEngine.Random.Range(0, allPlans.Count);
			customer.plan = allPlans[iRand];

			serverToUse.customers.Add(customer);
			AddLogEntry(customer.customerName + " created an account!");
		}
	}
	
	public CustomerType CalculateCustomerType() {
		/*
		var customerTypes = new List<KeyValuePair<float, CustomerType>>();
		foreach ( CustomerType cType in allCustomerTypes) {
			customerTypes.Add(new KeyValuePair<float, CustomerType>(cType.commonPercentage, cType));
		}
		*/
		int randInt = UnityEngine.Random.Range(1,100);
		if ( randInt < 84 ) {
			return allCustomerTypes[0];
		} else if ( randInt < 92 ) {
			return allCustomerTypes[1];
		} else if ( randInt < 97 ) {
			return allCustomerTypes[2];
		} else {
			return allCustomerTypes[3];
		}
	}

	public void ShowDialogueBox(string message, string header) {
		GameObject dialogue = Instantiate(dialogueBox, Vector3.zero, Quaternion.identity, GameObject.Find("Canvas").transform);
		dialogue.GetComponent<MessageDialog>().SetMessage(message, header);
	}

	public void SaveGame() {
		var bf = new BinaryFormatter();
		var file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

		var playerData = new PlayerData();
		playerData.health = 50;
		playerData.experience = 1500;
		bf.Serialize(file, playerData);
		file.Close();
	}

	public void LoadGame() {
		if ( File.Exists(Application.persistentDataPath + "/playerInfo.dat") ) {
			var bf = new BinaryFormatter();
			var file = File.Open(Application.persistentDataPath + "/playerInfo.data", FileMode.Open);
			PlayerData playerData = (PlayerData)bf.Deserialize(file);
			file.Close();

			// Then you can take playerData and load them into the gameManager instance
			// whatever = playerData.whatever
		}
	}
}

[Serializable]
class PlayerData {
	public float health;
	public float experience;
}