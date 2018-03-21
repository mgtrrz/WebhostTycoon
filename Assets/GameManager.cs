using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

	const string GameMode = "DEV";

	/* ---------------- */
	/*       Clock      */
	/* ---------------- */
	public int hour;
	public int day;
	public int week;
	public int month;
	public int year;
	public float timeInterval;
	public float counter; /* MAKE PRIVATE */

	public List<string> months = new List<string>() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"};
	

	public List<Server> servers;

	/* ------------------ */
	/*   Player Details   */
	/* ------------------ */
	public string playerName;
	public string companyName;
	public string domain;
	public string companyTld; // .com, .org, .net, etc.
	public string difficulty;


	/* ------------------ */
	/* Player Performance */
	/* ------------------ */
	public int funds;
	public float popularity;
	public int satisfaction;
	public int nps;

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
	public Text fundsTextbox;
	public Text hourTextbox;
	public Text dayMonthTextbox;
	public Text yearTextbox;

	public Text totalCustomersTextbox;
	public Text totalServersTextbox;
	public Text popularityTextbox;
	public Text satisfactionTextbox;

	/* ---------------- */
	/*  Game Variables  */
	/* ---------------- */




	private GameObject customerParent;


	void Start () {
		customerParent = GameObject.Find("Customers");
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

		if ( counter <= 0 ) {
			TickClock();
			Tick();
			counter = timeInterval;
		}

		counter -= Time.fixedDeltaTime;
		
	}

	public void TickClock() {
		hour++;

		if ( hour > 23 ) {
			day++;
			hour = 0;
		}

		if ( day > 28 ) {
			day = 1;
			month++;
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

		UpdateHourDisplay();
		UpdateDayMonthDisplay();
		UpdateYearDisplay();
		UpdateSummarizedDisplay();
	}

	public void UpdateFundsDisplay() {
		fundsTextbox.text = "$" + funds;
	}

	public void UpdateHourDisplay() {
		hourTextbox.text = hour.ToString();
	}

	public void UpdateDayMonthDisplay() {
		dayMonthTextbox.text = months[month - 1] + " " + day ;
	}
	
	public void UpdateYearDisplay() {
		yearTextbox.text = year.ToString();
	}

	public void UpdateSummarizedDisplay() {
		totalCustomersTextbox.text = customerParent.transform.childCount.ToString();
		totalServersTextbox.text = servers.Count.ToString();

		popularityTextbox.text = popularity.ToString();
		satisfactionTextbox.text = satisfaction.ToString();
	}

	public void Tick() {
		if (  servers != null ) {
			foreach (Server server in servers) {
				server.Tick();
			}
		}
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

	public void AddCustomer() {
		// Check to see if we have any servers first
		Server serverToUse = null;
		if ( servers != null ) {
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
		customer.customerName = "Sweaty Palms";
		customer.cxType = CalculateCustomerType();

		// Picking a plan at random for now
		int iRand = Random.Range(0, allPlans.Count);
		customer.plan = allPlans[iRand];

		serverToUse.customers.Add(customer);

		}
	}
	
	public CustomerType CalculateCustomerType() {
		/*
		var customerTypes = new List<KeyValuePair<float, CustomerType>>();
		foreach ( CustomerType cType in allCustomerTypes) {
			customerTypes.Add(new KeyValuePair<float, CustomerType>(cType.commonPercentage, cType));
		}
		*/
		int randInt = Random.Range(1,100);
		if ( randInt < 78 ) {
			return allCustomerTypes[0];
		} else if ( randInt < 86 ) {
			return allCustomerTypes[1];
		} else if ( randInt < 94 ) {
			return allCustomerTypes[2];
		} else {
			return allCustomerTypes[3];
		}
	}
}
