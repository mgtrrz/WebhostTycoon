using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerDetailsUI : MonoBehaviour {

	public Text customerDetailsLeftTextbox;
	public Text customerDetailsRightTextbox;
	public Button salesOpButton;
	public Button kickButton;

	public Customer customer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		UpdateDetailsUI();
	}

	private void UpdateDetailsUI() {
		customerDetailsLeftTextbox.text = customer.customerName + "\n" +
										  customer.age + "\n" +
										  customer.primarySite + "\n" +
										  customer.sites + "\n" +
										  GameDate.GetMonthNameFromInt( customer.dateJoined["Month"] ) + " " + customer.dateJoined["Day"] + ", Year: " + customer.dateJoined["Year"] +
										  "\n" + "\n" +
										  customer.plan.name;

		customerDetailsRightTextbox.text = customer.myServer.hostname + "\n" +
										   customer.satisfaction + "\n" +
										   customer.nps + "\n" + "\n" +
										   customer.cpuUsage.ToString("0.#\\%") + "\n" +
										   customer.averageCpuUsage + "\n" +
										   customer.diskUsage.ToString("0.#\\GB");

	}

	public void CloseWindow() {
		Destroy(gameObject);
	}

	public void SalesOp() {

	}

	public void KickCustomer() {

	}
}
