using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="CustomerType", menuName="Customer/Type", order = 1)]
public class CustomerType : ScriptableObject {
	public float minCpuUsage;
	public float maxCpuUsage;
	public float burstCpuUsage;
	public float burstPercentage;

	public float minDiskUsage;
	public float maxDiskUsage;
	public float commonPercentage;
}
