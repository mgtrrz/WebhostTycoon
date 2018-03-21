using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HostingType {
	Shared, Reseller, VPS, Dedicated
}
public class Plan : MonoBehaviour {

	public new string name;
	public string description;
	public int diskSpace;
	public bool diskSpaceHardLimit;
	public int bandwidth;
	public int sites;
	public int cost;

}
