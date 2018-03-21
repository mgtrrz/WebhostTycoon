using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CpuBrand {
	Intel, AMD
}

public enum CpuClass {
	Consumer, Enthusiast, Enterprise
}

public class CPU : MonoBehaviour {

	public new string name;

	public CpuBrand brand;
	public int cores;
	public bool hyperthreaded;
	public CpuClass cpuClass;
	public float frequency;

	public int cost;

	public int minWattage;
	public int maxWattage;
}
