using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="ServerType", menuName="Server/Type", order = 1)]
public class ServerType : ScriptableObject {

	public string description;

	public bool customerUse;
	public bool utilityUse;
	// Use this for initialization

}
