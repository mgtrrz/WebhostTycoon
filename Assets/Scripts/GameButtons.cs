using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButtons : MonoBehaviour {

	public GameObject newServerForm;
	public Plan emptyPlan;

	void Start() {
	}

	public void ShowServerBuildSheet() {
		//serverBuildSheetUI.SetActive(true);
		Instantiate(newServerForm, Vector3.zero, Quaternion.identity, GameObject.Find("Canvas").transform);
	}

	public void AddCustomer() {
		GameManager.gameManager.AddCustomer();
	}

	public void CreatePlaceholderPlan() {
		//Instantiate()
		Plan newPlan = Instantiate(emptyPlan);
	}

	public void ModifyPlans() {

	}
}
