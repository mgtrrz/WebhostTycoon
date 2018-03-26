using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StorageDrive : MonoBehaviour {

	public enum StorageBrand {
		WesternDigital, Samsung, Hitachi, SeaGate
	}

	public enum StorageType {
		HardDrive, SolidState
	}

	public new string name;
	public StorageType storageType;
	public StorageBrand storageBrand;
	public int totalCapacity;
	public int speed;
	public int cost;

	public float failureRate;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
