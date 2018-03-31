using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Employee : MonoBehaviour {

	public enum TeamTypes {
		Support, Marketing, Developers, ResearchAndDevelopment, 
		Training, Sales, Devops, Administrators
	}

	public enum EmployeeLocation {
		Home, Office, Remote
	}

	public string fullName;
	public int age;
	public int happiness;
	public int performance;
	public int productivity;
	public int burnOut;
	public int compensation;
	public TeamTypes team;
	public EmployeeLocation location;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
