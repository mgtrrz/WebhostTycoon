using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerServerUI : MonoBehaviour {

	public CustomerItem customerItem;
	public Server server;
	public GameObject viewportContent;

	// Use this for initialization
	void Start () {
		UpdateCustomerListUI();
	}
	
	// Update is called once per frame
	void Update () {

	}

	private void UpdateCustomerListUI() {
		// Deleting existing items
		foreach (Transform child in viewportContent.transform) {
			Destroy(child.gameObject);
		}

		// populating with customers
		foreach ( Customer customer in server.customers ) {
			CustomerItem ci = Instantiate(customerItem, Vector3.zero, Quaternion.identity, viewportContent.transform);
			ci.customer = customer;
		}
	}

	public void CloseWindow() {
		Destroy(gameObject);
	}
}
