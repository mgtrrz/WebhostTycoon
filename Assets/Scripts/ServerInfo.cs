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
	public Image bg;

	public Server server;


	public GameObject editServerUI;
	// Use this for initialization
	void Start () {
		bg.canvasRenderer.SetAlpha( 1f );
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
		diskUsage.text = "Disk Usage: " + server.CalculateDiskPercentage().ToString("0.#\\%");
		customers.text = "Customers: " + server.customers.Count;
		profit.text = "Profit: $" + (server.serverIncome - server.serverCosts);
		if ( server.acceptCustomers ) {
			accepting.text = "Accepting: Yes";
		} else {
			accepting.text = "Accepting: No";
		}

		if ( server.cpuUsage >= 60 || server.CalculateDiskPercentage() >= 80 ) {
			// print( server.hostname + " server load is above 60%!");
			bg.GetComponent<Image>().CrossFadeAlpha(1f, .2f, false);
		} else {
			bg.GetComponent<Image>().CrossFadeAlpha(0f, .2f, false);
		}
	}

	public void EditServerUI() {
		GameObject serverUI = Instantiate(editServerUI, Vector3.zero, Quaternion.identity, GameObject.Find("Canvas").transform);
		serverUI.GetComponent<ServerDetails>().server = server;
	}
}
