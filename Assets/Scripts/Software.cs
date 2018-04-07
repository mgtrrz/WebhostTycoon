using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Software", menuName="Server/Software", order = 3)]
public class Software : ScriptableObject {

	public string description;
	public bool allowsCustomers;

	public bool dedicated;
	public int difficulty;
	public int monthlyCost;
	public int oneTimeCost;
}
