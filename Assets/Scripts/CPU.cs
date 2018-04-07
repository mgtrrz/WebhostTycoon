using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CpuBrand {
	Intel, AMD
}

public enum CpuClass {
	Consumer, Enthusiast, Enterprise
}

[CreateAssetMenu(fileName="CPU", menuName="Server/CPU", order = 2)]
public class CPU : ScriptableObject {

	public CpuBrand brand;
	public int cores;
	public bool hyperthreaded;
	public CpuClass cpuClass;
	public float frequency;

	public int cost;

	public int minWattage;
	public int maxWattage;
}
