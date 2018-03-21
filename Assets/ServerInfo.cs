using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerInfo : MonoBehaviour {

	public Text hostname;
	public Text cpuUsage;
	public Text diskUsage;
	public Text customers;
	public Text profit;
	public Text accepting;

	public Server server;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		UpdateServerInfo();
	}

	public void UpdateServerInfo() {
		if ( server == null ) {
			return;
		}
		hostname.text = server.hostname;
		cpuUsage.text = "CPU Usage: " + server.cpuUsage.ToString("0.#\\%");
		diskUsage.text = "Disk Usage: " + server.diskUsage.ToString("0.#\\%");
		customers.text = "Customers: " + server.customers.Count;
		profit.text = "Profit: $" + server.serverIncome;
		if ( server.acceptCustomers ) {
			accepting.text = "Accepting: Yes";
		} else {
			accepting.text = "Accepting: No";
		}
	}
}
