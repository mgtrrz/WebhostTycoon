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

	public CustomerType cxType;

	private GameManager gameManager;

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

	}

	public void CustomerMonthlyTick() {

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
		// Calculate first time usage
		if ( diskUsage == 0 ) {
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
				float additionalUsage = Random.Range( cxType.minDiskUsage , (plan.diskSpace * (cxType.maxDiskUsage / 100) ) / 8 );

				// We're more likely to ADD files than we are delete 
				if ( Random.Range(1,100) <= 70 ) {
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
