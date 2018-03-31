using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Customer : MonoBehaviour {

	public string customerName;
	public int age;
	public string primarySite;
	public Plan plan;

	public float cpuUsage;
	public float averageCpuUsage;
	public float highestCpuUsage;
	public float diskUsage;
	public int sites;

	public float wattCost;


	public Dictionary<string, int> dateJoined;


	public int satisfaction;
	public float churn;
	public int nps;



	private int tickTimer;

	private int isExperiencingIssues;
	private int isHavingNoIssues;
	private int cancelThreshold;


	public CustomerType cxType;

	//private GameManager gameManager;

	public Server myServer;

	// Use this for initialization
	void Start () {
		//gameManager = FindObjectOfType<GameManager>();
		CalculateDiskUsage();
	}
	
	// Update is called once per frame
	void Update () {


	}

	public void CustomerTick() {
		CalculateCpuUsage();
		CalculateDiskUsage();

		if ( isExperiencingIssues > 0 ) {
			isExperiencingIssues -= 1;
		}

		CancelDecision();
		CheckIfAccountNoLongerNeeded();
	}

	public void CustomerDailyTick() {
		isHavingNoIssues++;
		CalculateCancellationIndex();
	}

	public void CustomerMonthlyTick() {
		
	}


	public void CalculateCustomerSatisfaction(float cpuUsage, float diskUsage, bool isFunctional) {
		// Check to see how their server is doing
		// We want to also check how their interactions with support has been

		if ( isExperiencingIssues == 0 ) { 

			if ( !isFunctional ) { // Server is basically offline
				ModifySatisfaction(-18);
				isExperiencingIssues = 10;
				isHavingNoIssues = 0;
				return;
			} else if ( cpuUsage > 90f ) { // Major server issues, services are crashing and sites may be down
				
				if ( Random.Range(1,100) > 60 ) {
					ModifySatisfaction(-14);
					isExperiencingIssues = 12;
					isHavingNoIssues = 0;
				}
				return;
			} else if ( cpuUsage > 75f ) { // Major server issues, sites may be slow to respond

				if ( Random.Range(1,200) > 155 ) {
					ModifySatisfaction(-9);
					isExperiencingIssues = 12;
					isHavingNoIssues = 0;
				}
				return;
			} else if ( cpuUsage > 62f ) { // Customer is seeing issues but it's intermittent

				if ( Random.Range(1,200) > 175 ) {
					ModifySatisfaction(-4);
					// We need to wait some time, otherwise, this will just keep relentlessly lowering the customer
					// satisfaction
					isExperiencingIssues = 12;
					isHavingNoIssues = 0;
				}
				return;
			} else if ( diskUsage >= 100 ) { // Services can't work when disk usage is full
				ModifySatisfaction(-20);
				isExperiencingIssues = 10;
				isHavingNoIssues = 0;
				return;
			}
		}


		// This is where we calculate happiness. Otherwise, things are going good, let's update our
		// satisfaction slowly over time.
		if ( isExperiencingIssues == 0 ) {
			
			if ( Random.Range(1,200) > 148 ) {
				if ( isHavingNoIssues >= 2) {
					ModifySatisfaction(5);
					isHavingNoIssues = 0;
				}
			}

		}

	}

	private void CalculateCancellationIndex() {
		if ( satisfaction < 20 ) {
			cancelThreshold += 5;
		} else if ( satisfaction < 40 ) {
			cancelThreshold += 2;
		} else if ( satisfaction < 60 ) {
			// do nothing
		} else if ( satisfaction < 80 ) {
			cancelThreshold -= 2;
		} else if ( satisfaction <= 100 ) {
			cancelThreshold -= 5;
		}

		if ( cancelThreshold > 100 ) {
			cancelThreshold = 100;
		} else if ( cancelThreshold < 0 ) {
			cancelThreshold = 0;
		}
	}

	private void CancelDecision() {
		if ( Random.Range(0, 1000) < cancelThreshold ) {
			CancelUser();
		}
	}

	// The percentage where a user may cancel randomly
	// at any time simply because they no longer
	// need the account.
	private void CheckIfAccountNoLongerNeeded() {
		int i = Random.Range(0,100000);
		if ( i > 7124 && i < 7131 ) {
			CancelUser();
		}
	}

	public void CancelUser() {
		//Debug.Log("User " + customerName + " wishes to cancel!"); 
		GameManager.gameManager.AddLogEntry(customerName + " cancelled their account");
		// First remove from the server i'm attached to
		myServer.RemoveCustomer(this);
		// Then destroy this gameobject
		Destroy(gameObject);
	}

	private void ModifySatisfaction(int amount) {
		satisfaction += amount;

		if ( satisfaction > 100 ) {
			satisfaction = 100;
		} else if ( satisfaction < 0 ) {
			satisfaction = 0;
		}
	}

	private void CalculateCpuUsage() {
		/* Calculating this customer's CPU usage */
		if ( Random.Range(1f, 100f) >= cxType.burstPercentage ) {
			cpuUsage = Random.Range(cxType.minCpuUsage, cxType.maxCpuUsage);
		} else {
			cpuUsage = Random.Range(cxType.maxCpuUsage, cxType.burstCpuUsage);
		}

		/* Saving some stats of this customer's CPU usage  */
		if ( cpuUsage > highestCpuUsage ) {
			highestCpuUsage = cpuUsage;
		}
	}

	private void CalculateDiskUsage() {

		if ( diskUsage == 0 ) { // Calculate first time usage
			// This is the percentage of the plan's allotted disk usage.
			// e.g. 10 GB limit, 5% of 10 GB is 0.5 GB
			diskUsage = Random.Range( plan.diskSpace * (cxType.minDiskUsage / 100) , plan.diskSpace * (cxType.maxDiskUsage / 100) );
		} else {

			if ( Random.Range(1, 500) >= 480 ) {
				// If we're above our allowed diskusage, make it more difficult
				// for users to exceed the plan if there is no hard limit in place.
				// We may need to change this for "malicious" users
				if ( diskUsage > plan.diskSpace ) {
					if ( Random.Range(1,1000) <= 980 ) {
						return;
					}
				}

				// Calculating an amount for adding usage
				float additionalUsage = Random.Range( 0f , 2f );

				// We're more likely to ADD files than we are delete 
				if ( Random.Range(1,100) <= 60 ) {
					diskUsage += additionalUsage;
				} else {
					diskUsage -= additionalUsage;
				}


			}

		}


		// If a hard limit is in place, e.g. they cannot create any more files once they hit their quota,
		// cap the limit at the current plan disk space. Otherwise if there is no hard limit in place,
		// let them use as much as their percentages allow
		if ( diskUsage > plan.diskSpace && plan.diskSpaceHardLimit ) {
			diskUsage = plan.diskSpace;
		} 

		// We can't have negative space, so let's just generate a random low number
		if ( diskUsage <= 0 ) {
			diskUsage = Random.Range(0f, 0.50f);
		}
	}


}
