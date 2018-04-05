using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;

public class Server : MonoBehaviour {

	public string hostname;


	public ServerChassis serverChassis; // The selected enclosure for this server
	public CPU processor; // The CPU this server is running
	public List<StorageDrive> hardDrives; // A list of hard drives configured on this server
	public Software software; // The software running on this server
	public ServerType serverType;
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

	public float powerUsage; // Wattage 
	public int powerCost; // Cost ^

	public int originalServerCost;
	public Dictionary<string, int> originalBuildDate; // "Month" = 1, "Day" = 1, "Year" = 1
	public List<float> cpuUsageOverTime;

	public bool active; // Whether this server is turned "on" or "off"
	

	private int cpuLogTimer;
	private int diskLogTimer;

	//private GameManager.gameManager GameManager.gameManager;
	public enum ServerType {
		[Description("Example Customer")]
		Customer = 0, 
		[Description("Example Utility")]
		Utility = 1
	}

	void Start() {
		//GameManager.gameManager = FindObjectOfType<GameManager.gameManager>();
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
		CalculatePowerUsage();

		// Money
		CalculateRevenue();
		CalculateMonthlyExpenses();

		CalculateCustomerSatisfaction();

		BroadcastServerPerformance(); // For sending server usage to customers
		LogServerPerformance(); // For notifying the player about server performance

		if ( cpuLogTimer > 0 ) {
			cpuLogTimer -= 1;
		}

		if ( diskLogTimer > 0 ) {
			diskLogTimer -= 1;
		}
	}

	public void ServerMonthlyTick() {
		// Debug.Log("Server: " + hostname + " got MONTHLY ServerTick broadcast!");
		// Calculating our monthly revenue and expenses
		GameManager.gameManager.MakeProfit( serverCosts );
	}


	public void BroadcastServerPerformance() {
		foreach (Customer cx in customers) {
			cx.CalculateCustomerSatisfaction(cpuUsage, CalculateDiskPercentage(), isFunctional());
		}
	}

	private void LogServerPerformance() {

		if ( cpuLogTimer == 0 ) {
			if ( cpuUsage >= 60 ) {
				GameManager.gameManager.AddLogEntry(hostname + " CPU usage is over 60%! Customer sites may be slow or unresponsive.");
				cpuLogTimer = 24;
			} else if ( cpuUsage >= 100 ) {
				GameManager.gameManager.AddLogEntry(hostname + " CPU usage is over 100%! Services are failing. Customer sites are down.");
				cpuLogTimer = 6;
			}
		}

		if ( diskLogTimer == 0 ) {
			if ( CalculateDiskPercentage() >= 80 ) {
				GameManager.gameManager.AddLogEntry(hostname + " disk usage is nearing 100%. Optimize customer accounts or add new hard drives.");
				diskLogTimer = 24;
			} else if ( CalculateDiskPercentage() >= 80 ) {
				GameManager.gameManager.AddLogEntry(hostname + " disk usage is at 100%! Services are unable to operate reliably. Customer sites are down.");
				diskLogTimer = 6;
			}
		}
	}

	private void CalculateCustomerSatisfaction() {
		if ( customers.Count == 0 ) {
			return;
		}
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

	private void CalculatePowerUsage() {
		// CPUs have a minimum watt and maximum watt
		// When CPU usage is at 0%, use minimum watt. If at 100% use maximum
		// value = ( percent * (max-min) / 100) + min
		powerUsage = ( cpuUsage * (processor.maxWattage - processor.minWattage) / 100 ) + processor.minWattage;

		// It'll be interesting to see how this would work but it may be too complicated for users.
		// If we went forward, we'd have to keep a list of usage over time and either pay for electric
		// at the end of the month or each day
		// powerCost = powerUsage
	}


	// Do Not Use to Completely Kick a Customer.
	// This will remove them from the server, but not
	// remove the Customer gameObject from the game.
	// Use Customer.CancelUser();
	public void RemoveCustomer(Customer customer) {
		customers.Remove(customer);
	}

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
