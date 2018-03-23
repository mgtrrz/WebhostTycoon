using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ServerType {
	Desktop, Workstation, Server1U, Server2U, Storage4U
}



public class Server : MonoBehaviour {

	public string hostname;


	public ServerChassis serverChassis; // The selected enclosure for this server
	public CPU processor; // The CPU this server is running
	public List<StorageDrive> hardDrives; // A list of hard drives configured on this server
	public Software software; // The software running on this server

	public List<Customer> customers; // All the customers on this box (if any)

	/*
	These are probably unnecessary and may complicate things,
	but is something to consider for maybe more Expert settings?
	*/
	// public RaidTypes raid;
	// public OperatingSystem os;
	

	/* Differences:
		cpuLoad: 0-100% per thread. If a server has 4 threads total (2 cores hyperthreaded = 4 threads = 400% cpuLoad overhead)
				 then a load of 200% is 2 cores being utilized completely (2 x 100%). Total CPU usage
				 is actually at 50%
		cpuUsage: 0-100% per CPU. If cpuUsage is at 100%, then all 4 threads are at 100% (4 x 100% = 400% cpuLoad)
		To make things simple, we're probably only going to show cpuUsage to players.
	*/
	public float cpuLoad;
	public float cpuUsage; // Percentage based 0-100%


	public float diskUsage; // Amount of disk used by customers



	public bool acceptCustomers; // Whether to accept new customers onto this box
	public int maxCustomers; // Max amount of customers allowed on this box
	public int serverIncome; // Amount of money this server is making from customers on it
	public int serverCosts; // Amount of money it costs to run this server
	public int serverCustomerSatisfaction; // How happy customers are of server performance on average

	public int originalServerCost;
	public Dictionary<string, int> originalBuildDate; // "Month" = 1, "Day" = 1, "Year" = 1
	public List<float> cpuUsageOverTime;

	public bool active; // Whether this server is turned "on" or "off"


	private GameManager gameManager;

	void Start() {
		gameManager = FindObjectOfType<GameManager>();
	}


	public int hardDriveCapacity {
		get { 
			if ( serverChassis != null ) {
				return serverChassis.hardDriveCapacity;
			}
			 return 0;
		}
	}

	public int cpuCores {
		get { 
			if ( processor != null ) {
				return processor.cores; 
			}
			return 0;
		}
	}

	public int logicalCores {
		get {
			if ( processor != null ) {
				if ( processor.hyperthreaded ) {
					return processor.cores * 2;
				} else {
					return processor.cores;
				}
			}
			return 0;
		}
	}

	public string processorName {
		get { 
			if ( processor != null ) {
				return processor.brand.ToString() + " " + processor.name; 
			}
			return "";
		}
	}
	
	public float processorSpeed {
		get {
			if ( processor != null ) {
				return processor.frequency; 
			}
			return 0;
		}
	}

	public int GetTotalDiskSpace {
		get {
			int totalDiskSpace = 0;
			foreach ( StorageDrive hd in hardDrives ) {
				totalDiskSpace += hd.totalCapacity;
			}

			return totalDiskSpace;
		}
	}

	/* ------------------------------------------------ */



	public void ServerTick() {
		// Debug.Log("Server: " + hostname + " got ServerTick broadcast!");
		// Resources
		CalculateCpuUsage();
		CalculateDiskUsage();

		// Money
		CalculateRevenue();
		CalculateMonthlyExpenses();

		BroadcastServerPerformance();
	}

	public void ServerMonthlyTick() {
		// Debug.Log("Server: " + hostname + " got MONTHLY ServerTick broadcast!");
		// Calculating our monthly revenue and expenses
		gameManager.MakeProfit( serverIncome - serverCosts );
	}


	public void BroadcastServerPerformance() {
		foreach (Customer cx in customers) {
			cx.CalculateCustomerSatisfaction(cpuUsage, CalculateDiskPercentage(), isFunctional());
		}
	}


	private void CalculateCustomerSatisfaction() {
		int cxSatisfaction = 0;
		foreach (Customer cx in customers) {
			cxSatisfaction += cx.satisfaction;
		}
		serverCustomerSatisfaction = cxSatisfaction / customers.Count;
	}

	public void CalculateCpuUsage() {
		cpuLoad = 0;
		cpuUsage = 0;
		foreach (Customer cx in customers) {
			cpuLoad += cx.cpuUsage;
		}
		cpuUsage = cpuLoad / logicalCores; 
	}

	public void CalculateDiskUsage() {
		diskUsage = 0;
		foreach (Customer cx in customers) {
			diskUsage += cx.diskUsage;
		}
	}

	public float CalculateDiskPercentage() {
		return ( diskUsage / GetTotalDiskSpace ) * 100;
	}

	public void CalculateRevenue() {
		// Money coming into the server from customers
		serverIncome = 0;
		foreach (Customer cx in customers) {
			serverIncome += cx.plan.cost;
		}
	}

	public void CalculateMonthlyExpenses() {
		// Costs to run the server, like software licenses or electricity costs
		int cost = software.cost;
		serverCosts = cost;
	}

	// public void CalculateUpfrontExpenses() {
		
	// }


	public bool isFunctional() {
		/* Server needs a processor and hard drives to function */
		if ( processor == null ) {
			return false;
		}
		if ( hardDrives == null ) {
			return false;
		}



		return true;
	}
	
}
