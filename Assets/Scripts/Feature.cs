using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Feature", menuName="Feature", order = 1)]
public class Feature : ScriptableObject {

	public string description;
	public int additionalValue;
	public int supportBurden;
	public int customerCost;
	public int companyCost;
	public PricingModel pricingModel;
	public int researchRequirement;
	public bool availableToUse;
	public bool optional;

	public enum PricingModel {
		Monthly,
		Yearly,
		Free
	}
	
}
