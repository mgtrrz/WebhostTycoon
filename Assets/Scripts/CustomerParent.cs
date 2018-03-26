using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerParent : MonoBehaviour {

	public void BroadcastTick() {
		// Debug.Log("Got request to Broadcast Tick to our servers");
		BroadcastMessage("CustomerTick", SendMessageOptions.DontRequireReceiver );
	}

	public void BroadcastMonthlyTick() {
		// Debug.Log("Got request to Broadcast the Monthly Tick to our servers");
		BroadcastMessage("CustomerMonthlyTick", SendMessageOptions.DontRequireReceiver );
	}

	public void BroadcastDailyTick() {
		BroadcastMessage("CustomerDailyTick", SendMessageOptions.DontRequireReceiver );
	}
}
