using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButtons : MonoBehaviour {

	public GameObject serverBuildSheetUI;
	private GameManager gameManager;

	void Start() {
		gameManager = FindObjectOfType<GameManager>();
	}

	public void ShowServerBuildSheet() {
		serverBuildSheetUI.SetActive(true);
	}

	public void AddCustomer() {
		gameManager.AddCustomer();
	}
}
