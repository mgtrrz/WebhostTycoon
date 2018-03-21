using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Server))]
[System.Serializable]
public class ServerEditor : Editor {

	public override void OnInspectorGUI() {
		DrawDefaultInspector();

		Server myServer = (Server)target;

		EditorGUILayout.LabelField("Server Information", EditorStyles.boldLabel);
		EditorGUILayout.LabelField("Hard Drive Capacity:", myServer.hardDriveCapacity.ToString());
		EditorGUILayout.LabelField("CPU Cores:", myServer.cpuCores.ToString());
		EditorGUILayout.LabelField("Logical Cores:", myServer.logicalCores.ToString());
		EditorGUILayout.LabelField("Processor Name:", myServer.processorName);
		EditorGUILayout.LabelField("CPU Speed:", myServer.processorSpeed.ToString());
		EditorGUILayout.LabelField("Disk Space:", myServer.GetTotalDiskSpace + " GB");
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Current CPU Usage:", myServer.cpuUsage.ToString());
		EditorGUILayout.LabelField("Current Disk Usage:", myServer.diskUsage.ToString());
	}
}
