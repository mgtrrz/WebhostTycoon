using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="ServerChassis", menuName="Server/Chassis", order = 4)]
public class ServerChassis : ScriptableObject {
	
	public string description;
	public int cost;
	public int size;
	public bool rackCompatible;
	public int hardDriveCapacity;
	public int resellValue;
}
