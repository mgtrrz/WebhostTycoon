using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="CustomerType", menuName="Customer/Type", order = 1)]
public class CustomerType : ScriptableObject {
	public float minCpuUsage;
	public float maxCpuUsage;
	public float burstCpuUsage;
	public float burstPercentage;


	[Header("Visitor/Traffic values")]
	[Tooltip("Traffic will range from negativeMinVisitors and maxVisitors.")]
	public int negativeMinVisitors; 
	public int maxVisitors;
	public int burstVisitors;

	[Range(1,100)]
	public int minSiteOptimization;
	[Range(1,100)]
	public int maxSiteOptimization;

	[Header("Disk usage")]
	public float minDiskUsage;
	public float maxDiskUsage;

	[Space(5)]
	public float commonPercentage;
}
