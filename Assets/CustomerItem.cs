using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerItem : MonoBehaviour {

	public Text customerNameTextbox;
	public Text cpuTextbox;
	public Text diskTextbox;
	public Button infoButton;

	public Customer customer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		UpdateCustomerInfo();
	}

	public void UpdateCustomerInfo() {
		if ( customer != null ) {
			customerNameTextbox.text = customer.customerName;
			cpuTextbox.text = customer.cpuUsage.ToString("0.#\\%");
			diskTextbox.text = customer.diskUsage.ToString("0.#\\GB");
		}
	}
}
