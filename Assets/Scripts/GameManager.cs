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
	public int popularity;
	public int satisfaction;
	private int nps;

	/* ---------------- */
	/*      Prefabs     */
	/* ---------------- */
	[Space]
	[Space]
	[Header("Server Component Prefabs")]
	public List<ServerChassis> allServerChassis;
	public List<ServerType> allServerTypes;
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
	public GameObject newServerForm;

	public Text logTextbox;

	/* ---------------- */
	/*  Game Variables  */
	/* ---------------- */




	private CustomerParent customerParent;
	private ServerParent serverParent;

	public enum ServerEnvironment {
		Home, Provider, Datacenter, Cloud
	}
	
	// Singleton stuff
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
		// This will fail in other scenes, so we'll need to handle a better way to do this.
		customerParent = FindObjectOfType<CustomerParent>();
		serverParent = FindObjectOfType<ServerParent>();

		/* Initializing our Clock */
		day = 1;
		week = 1;
		month = 1;
		year = 1;
		funds = 1000;

		// Dev variables
		if ( GameMode == "DEV" ) {
			playerName = "Marcus Gutierrez";
			companyName = "LoudServers";
			domain = "loudservers";
			companyTld = ".com";
		}

		// UI Display
		UpdateFundsDisplay();
		UpdateSummarizedDisplay();
	}
	

	void Update () {
		
	}

	// Handles our in-game tick/hour!
	// Doing it in FixedUpdate means we can pause time using Unity's built in tools
	void FixedUpdate() {
		if ( counter <= 0 ) {
			TickClock();
			counter = timeInterval;
		}

		counter -= Time.fixedDeltaTime;
	}
	
	// Private method for incrementing hours, days, months, weeks
	// for our in-game clock.
	private void TickClock() {
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

	// Our in-game tick. Responsible for broadcasting ticks to customers and servers
	private void Tick() {

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
	
	// in-game tick, sending out a broadcast once every day.
	private void DailyTick() {
		serverParent.BroadcastDailyTick();
	}

	// in-game tick, sending out a broadcast once every month.
	private void MonthlyTick() {
		// for our monthly costs!
		serverParent.BroadcastMonthlyTick();
	}

	// Returns current day
	public int currentDay {
		get {
			return day;
		}
	}

	// UI
	// Updates funds
	public void UpdateFundsDisplay() {
		fundsTextbox.text = "$" + funds;
	}

	// UI
	// Updates the hour display
	public void UpdateHourDisplay() {
		hourTextbox.text = hour.ToString();
	}

	// UI
	// Updates the day and month display
	public void UpdateDayMonthDisplay() {
		dayMonthTextbox.text = GameDate.GetMonthNameFromInt(month)  + " " + day;
	}
	
	// UI
	// Updates the year display (Could we just combine these methods?)
	public void UpdateYearDisplay() {
		yearTextbox.text = year.ToString();
	}

	// UI
	// Updates information about this current game
	public void UpdateSummarizedDisplay() {
		websiteName.text = domain + companyTld;

		totalCustomersTextbox.text = customerParent.transform.childCount.ToString();
		totalServersTextbox.text = servers.Count.ToString();

		popularityTextbox.text = popularity.ToString();
		satisfactionTextbox.text = satisfaction.ToString();
	}


	// UI
	// Log text box that shows current messages
	public void AddLogEntry(string message) {
		if ( logTextbox.cachedTextGenerator.lineCount > 8 ) {
			logTextbox.text = logTextbox.text.Substring(logTextbox.text.IndexOf('\n') + 1);
		}

		logTextbox.text += message + "\n";
	}


	// Use this method to subtract money from the players funds
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


	// Use this method to add money to the players funds
	public void MakeProfit(int amount) {
		funds += amount;
		UpdateFundsDisplay();
	}

	// Method for calculating how many customers we should be getting per tick.
	public void CalculateCustomerTraction() {

		int tempPop = 0;
		int highPop = 0;
		if ( popularity <= 5 ) {
			tempPop = 5;
			highPop = 50;
		} else {
			tempPop = (int)popularity;
			highPop = popularity*popularity;
		}
		var randomPop = UnityEngine.Random.Range(0, highPop );

		Debug.Log("Popularity: " + popularity + ". TempPop: " + tempPop + ". HighPop: " + highPop + ". Our Random Number: " + randomPop);
		// Determining if we get a customer in this tick
		if ( randomPop <= tempPop) {
			AddCustomer();
		}
	}

	// Getting the satisfaction of all of the customers.
	// We might just want to iterate through the servers instead of the customer
	// since the server already calculates satisfaction for each of its customers.
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

	// Returns current Game Date
	public Dictionary<string, int> GetCurrentGameDate() {
		var dateDict = new Dictionary<string, int>();
		dateDict.Add("Month", month);
		dateDict.Add("Day", day);
		dateDict.Add("Year", year);
		return dateDict;
	}

	public void ModifyPopularity(int amt) {
		popularity += amt;
		if (popularity < 0) {
			popularity = 0;
		}
	}


	// Method for adding a customer
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

			//GameObject serverParent = GameObject.Find("Customers");
			// Add them in there
			Customer customer = Instantiate(customerPrefab, Vector3.zero, Quaternion.identity, serverToUse.transform);

			string[] gender = new string[] {"male", "female"};

			string randGend = gender[UnityEngine.Random.Range(0,2)];

			customer.age = UnityEngine.Random.Range(18, 115);

			customer.customerName = NameGenerator.generateRandomName(randGend);
			customer.cxType = ReturnRandomCustomerType();
			customer.dateJoined = GetCurrentGameDate();

			customer.myServer = serverToUse;

			// Picking a plan at UnityEngine.Random for now
			int iRand = UnityEngine.Random.Range(0, allPlans.Count);
			customer.plan = allPlans[iRand];

			serverToUse.customers.Add(customer);
			AddLogEntry(customer.customerName + " created an account!");
		}
	}
	
	// Returns a random customer Type depending on the CustomerType list
	// Each CustomerType has a "commonPercentage" variable that we use to calculate
	// the percentage each customer should get that CustomerType
	public CustomerType ReturnRandomCustomerType() {
		if (allCustomerTypes == null) {
			throw new Exception("There are no CustomerTypes to choose from!");
		}

		float maxPercentage = 0f;
		foreach (CustomerType ct in allCustomerTypes) {
			if ( ct == null ) {continue;}
			maxPercentage += ct.commonPercentage;
		}

		int randInt = UnityEngine.Random.Range(1, (int)maxPercentage + 1);
		float totalPercentage = 0f;

		for (int i = 0; i <= allCustomerTypes.Count; i++) {
			if ( allCustomerTypes[i] == null ) { // If we came across an empty entry in this list
				continue; // bypass it
			}

			totalPercentage += allCustomerTypes[i].commonPercentage;

			if ( randInt <= totalPercentage ) {
				Debug.Log("Returning CustomerType: " + allCustomerTypes[i].name + ". RandInt = " + randInt + " <= " + totalPercentage);
				return allCustomerTypes[i];
			}
		}

		// if we still made it this far, return the default and throw an error message
		return allCustomerTypes[0];
	}

	// For showing a dialogue box with a message and header.
	public void ShowDialogueBox(string message, string header) {
		GameObject dialogue = Instantiate(dialogueBox, Vector3.zero, Quaternion.identity, GameObject.Find("Canvas").transform);
		dialogue.GetComponent<MessageDialog>().SetMessage(message, header);
	}

	// Base for saving the game but not currently functioning
	public void SaveGame() {
		var bf = new BinaryFormatter();
		var file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

		var playerData = new PlayerData();
		playerData.health = 50;
		playerData.experience = 1500;
		bf.Serialize(file, playerData);
		file.Close();
	}

	// Base for loading the game but not currently functioning
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