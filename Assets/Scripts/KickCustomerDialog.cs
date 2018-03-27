using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KickCustomerDialog : MonoBehaviour {

	public Dropdown kickReasonsDropdown;
	public InputField messageToCustomerInput;
	public List<string> kickReason;
	public Customer customer;
	public GameObject customerDetailsWindow; // To close after we delete our user

	// public Button kickButton;

	// Use this for initialization
	void Start () {
		kickReasonsDropdown.ClearOptions();
		kickReasonsDropdown.AddOptions(kickReason);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CloseWindow() {
		Destroy(gameObject);
	}

	public void KickCustomer() {
		// We probably want to add this to a special section where that user may have lasting effects on the company
		// but for now, we'll delete the user.
		customer.CancelUser();
		Destroy(customerDetailsWindow);
		Destroy(gameObject);
	}
}
