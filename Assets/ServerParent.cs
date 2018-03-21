using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerParent : MonoBehaviour {

	/*
	All of the servers created should be a child of this ServerParent GameObject.
	There's two methods in this class to send a request to all of the child GameObjects.
	Those are "BroadcastTick()" and "BroadcastMonthlyTick()"
	Tick of course is sent at a constant interval that can change (but is by default about every second)
	while MonthlyTick is sent once once a new in-game month has started
	*/
	public void BroadcastTick() {
		Debug.Log("Got request to Broadcast Tick to our servers");
		BroadcastMessage("ServerTick", SendMessageOptions.DontRequireReceiver );
	}

	public void BroadcastMonthlyTick() {
		Debug.Log("Got request to Broadcast the Monthly Tick to our servers");
		BroadcastMessage("ServerMonthlyTick", SendMessageOptions.DontRequireReceiver );
	}
}
