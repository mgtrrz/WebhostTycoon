using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ServerType {
	Desktop, Workstation, Server1U, Server2U, Storage4U
}



public class Server : MonoBehaviour {

	public string hostname;
	public ServerType serverType;
	public ServerChassis serverChassis;
	public CPU processor;
	public List<StorageDrive> hardDrives;

	/*
	These are unnecessary
	public RaidTypes raid;
	public OperatingSystem os;
	*/

	/* Differences:
		cpuLoad: 0-100% per thread. If a server has 4 threads total (2 cores hyperthreaded = 4 threads = 400% cpuLoad overhead)
				 then a load of 200% is 2 cores being utilized completely (2 x 100%). Total CPU usage
				 is actually at 50%
		cpuUsage: 0-100% per CPU. If cpuUsage is at 100%, then all 4 threads are at 100% (4 x 100% = 400% cpuLoad)
		To make things simple, we're probably only going to show cpuUsage to players.
	*/
	public float cpuLoad;
	public float cpuUsage;
	public float diskUsage;
	public List<Customer> customers;

	/* Whether to accept new customers onto this box */
	public bool acceptCustomers;

	/* Max amount of customers allowed on this box */
	public int maxCustomers;

	/* Amount of money this server is making from customers on it */
	public int serverIncome;

	/* Amount of money it costs to run this server */
	public int serverCosts;

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

	/*
	public float CurrentCpuUsage {
		get {
			foreach
		}
	}

	*/

	public void Tick() {
		CalculateCpuUsage();
		CalculateDiskUsage();
		CalculateRevenue();
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

	public void CalculateRevenue() {
		serverIncome = 0;
		foreach (Customer cx in customers) {
			serverIncome += cx.plan.cost;
		}
	}

	public void CalculateMonthlyExpenses() {
		serverCosts = 200;
	}

	public void CalculateUpfrontExpenses() {

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
