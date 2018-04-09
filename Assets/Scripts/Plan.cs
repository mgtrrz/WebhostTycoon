using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Plan", menuName="PlansAndPerks/Plan", order = 1)]
public class Plan : ScriptableObject {

	public string description;
	public int diskSpace;
	public bool diskSpaceHardLimit;
	public int bandwidth;
	public int sites;
	public int cost;
	public List<Feature> planFeatures;
	public HostingType hostingType;

	public enum HostingType {
		Shared, Reseller, VPS, Dedicated
	}

}
