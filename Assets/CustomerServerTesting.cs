using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CustomerServerTesting : MonoBehaviour {

	public GameObject serverPrefab;
	private GameObject serverParent;

	public List<GameObject> processors;



	// Use this for initialization
	void Start () {
		serverParent = GameObject.Find("Servers");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddServer() {
		var newServer = Instantiate(serverPrefab, Vector2.zero, Quaternion.identity, serverParent.transform) as GameObject;
		var serverComponent = newServer.GetComponent<Server>();

		serverComponent.hostname = "test";
		foreach (GameObject cpu in processors) {
			Debug.Log(cpu.name);
			if ( cpu.name == "Intel_Core_i3_4200" ) {
				serverComponent.processor = cpu.GetComponent<CPU>();
				break;
			}
		}
	}

	public void AddCustomer() {

	}
}
