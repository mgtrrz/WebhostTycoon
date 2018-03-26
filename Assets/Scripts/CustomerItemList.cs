using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerItemList : MonoBehaviour {

	public CustomerItem customerItem;
	public Server serverToDisplay;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		foreach ( Customer customer in serverToDisplay.customers ) {
			Instantiate(customerItem, Vector3.zero, Quaternion.identity, transform);
		}
	}
}
