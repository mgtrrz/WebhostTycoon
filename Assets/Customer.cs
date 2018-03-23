using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Customer : MonoBehaviour {

	public string customerName;
	public int age;
	public Plan plan;

	public float cpuUsage;
	public float averageCpuUsage;
	public float highestCpuUsage;
	public float diskUsage;
	public int sites;


	public int dayJoined;
	public int monthJoined;
	public int yearJoined;


	public int satisfaction;
	public float churn;
	public int nps;



	private int tickTimer;

	private int isExperiencingIssues;
	private int isHavingNoIssues;

	public CustomerType cxType;

	private GameManager gameManager;

	private Server customerServer;

	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<GameManager>();
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
	}

	public void CustomerDailyTick() {
		
		isHavingNoIssues++;
	}

	public void CustomerMonthlyTick() {
		
	}


	public void CalculateCustomerSatisfaction(float cpuUsage, float diskUsage, bool isFunctional) {
		// Check to see how their server is doing
		// We want to also check how their interactions with support has been

		if ( isExperiencingIssues == 0 ) { 

			if ( !isFunctional ) { // Server is basically offline
				modifySatisfaction(-18);
				isExperiencingIssues = 10;
				isHavingNoIssues = 0;
				return;
			} else if ( cpuUsage > 90f ) { // Major server issues, services are crashing and sites may be down
				
				if ( Random.Range(1,100) > 60 ) {
					modifySatisfaction(-14);
					isExperiencingIssues = 12;
					isHavingNoIssues = 0;
				}
				return;
			} else if ( cpuUsage > 75f ) { // Major server issues, sites may be slow to respond

				if ( Random.Range(1,200) > 155 ) {
					modifySatisfaction(-9);
					isExperiencingIssues = 12;
					isHavingNoIssues = 0;
				}
				return;
			} else if ( cpuUsage > 62f ) { // Customer is seeing issues but it's intermittent

				if ( Random.Range(1,200) > 175 ) {
					modifySatisfaction(-4);
					// We need to wait some time, otherwise, this will just keep relentlessly lowering the customer
					// satisfaction
					isExperiencingIssues = 12;
					isHavingNoIssues = 0;
				}
				return;
			} else if ( diskUsage >= 100 ) { // Services can't work when disk usage is full
				modifySatisfaction(-20);
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
					modifySatisfaction(5);
					isHavingNoIssues = 0;
				}
			}

		}

	}

	private void modifySatisfaction(int amount) {
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
	}


}
