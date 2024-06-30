using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Measurements))]
public class MeasurementsEditor : Editor {

	private GUIStyle headerStyle;
	private GUIStyle labelStyle;
	private GUIStyle valueStyle;

	public override void OnInspectorGUI()
	{
		Measurements Measurements = (Measurements)target;

		//initial settings
		if (headerStyle == null)
		{
			headerStyle = new GUIStyle(GUI.skin.label);
			headerStyle.fontStyle = FontStyle.Bold;
			headerStyle.fontSize = 11;
		}
		if (labelStyle == null)
		{
			labelStyle = new GUIStyle(GUI.skin.label);
			labelStyle.fontStyle = FontStyle.Normal;
			labelStyle.fontSize = 10;
		}
		if (valueStyle == null)
		{
			valueStyle = new GUIStyle(GUI.skin.label);
			valueStyle.fontStyle = FontStyle.Bold;
			valueStyle.fontSize = 10;
		}

		DrawDefaultInspector();

		if (Measurements.showSourceError) EditorGUILayout.HelpBox("GameObject is missing the Measurement Source!", MessageType.Error, true);

		GUILayout.Space(15);
		EditorGUILayout.LabelField("Dimensions", headerStyle);

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Width", labelStyle, GUILayout.MaxWidth(120));
		EditorGUILayout.LabelField(Measurements.width_string, valueStyle);
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Height", labelStyle, GUILayout.MaxWidth(120));
		EditorGUILayout.LabelField(Measurements.height_string, valueStyle);
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Depth", labelStyle, GUILayout.MaxWidth(120));
		EditorGUILayout.LabelField(Measurements.depth_string, valueStyle);
		EditorGUILayout.EndHorizontal();

		GUILayout.Space(15);

		if (Measurements.DistanceObject != null)
		{
			EditorGUILayout.LabelField("Distance to " + Measurements.DistanceObject.name, headerStyle);
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Center to Center", labelStyle, GUILayout.MaxWidth(120));
			EditorGUILayout.LabelField(Measurements.center_to_center_string, valueStyle);
			EditorGUILayout.EndHorizontal();
			if (!Measurements.showSourceError)
			{
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField("Edge to Edge", labelStyle, GUILayout.MaxWidth(120));
				EditorGUILayout.LabelField(Measurements.edge_to_edge_string, valueStyle);
				EditorGUILayout.EndHorizontal();
			}

			GUILayout.Space(15);

			EditorGUILayout.LabelField("Angle to " + Measurements.DistanceObject.name, headerStyle);
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Vector2 XZ", labelStyle, GUILayout.MaxWidth(120));
			EditorGUILayout.LabelField(Measurements.angle_xz_string, valueStyle);
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Vector2 YZ", labelStyle, GUILayout.MaxWidth(120));
			EditorGUILayout.LabelField(Measurements.angle_yz_string, valueStyle);
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Vector2 XY", labelStyle, GUILayout.MaxWidth(120));
			EditorGUILayout.LabelField(Measurements.angle_xy_string, valueStyle);
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Vector3 Forward", labelStyle, GUILayout.MaxWidth(120));
			EditorGUILayout.LabelField(Measurements.angle_v3_string, valueStyle);
			EditorGUILayout.EndHorizontal();
		}
		else
		{
			EditorGUILayout.LabelField("Distance Object", headerStyle);
			EditorGUILayout.HelpBox("Get the distance and angle between this object and another gameobject by setting the DistanceObject field.", MessageType.Info, true);
		}
		
	}
}
