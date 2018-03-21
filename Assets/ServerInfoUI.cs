using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerInfoUI : MonoBehaviour {

	public GameObject serverInfo;
	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<GameManager>();

	}
	
	// Update is called once per frame
	void Update () {

		
	}

	public void UpdateServerInfoDisplay() {
		if ( gameManager.servers != null ) {
			// Let's clear out the previous entries by deleting the child gameobjects
			foreach (Transform child in transform) {
				Destroy(child.gameObject);
			}

			foreach (Server server in gameManager.servers) {
				// Instantiating our UI child box thing
				GameObject si = Instantiate(serverInfo, Vector3.zero, Quaternion.identity, gameObject.transform);
				ServerInfo serverDetails = si.GetComponent<ServerInfo>();
				// Setting that UI child box thing to use this server
				serverDetails.server = server;
			}
		}
	}
}
