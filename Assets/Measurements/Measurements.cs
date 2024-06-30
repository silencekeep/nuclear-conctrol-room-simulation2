using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MeasurementUnits
{
	centimeters, meters, kilometers, yards, inches, feet, miles
}
public enum MeasurementSources
{
	Renderer, Mesh, Collider
}

[ExecuteInEditMode]
public class Measurements : MonoBehaviour {

	/* *********************************************************************************
	 *									Settings
	 * *********************************************************************************/

		// Public
	public MeasurementUnits MeasurementUnit = MeasurementUnits.meters;
	public MeasurementSources MeasurementSource = MeasurementSources.Renderer;
	public GameObject DistanceObject;
		// Private
	private Bounds bounds;
	private Vector3 distanceObjectDir;
	private Vector3 distanceObjectXZ;
	private Vector3 distanceObjectYZ;
	private Vector3 distanceObjectXY;
	[HideInInspector] public bool showSourceError = false;

	/* *********************************************************************************
	 *									Calculations
	 *									
	 * Calculated variables for inspector viewing and for external reference. Can be used
	 * for realtime dimension and distance measurements. See below for explanation of vars.
	 * 
	 * {variable}_meters : The dimension/length in meters, aka unity units
	 * {variable}		 : The dimension/length in preferred measurement unit
	 * {variable}_string : The dimension/length in preferred measurement unit, for labels
	 * 
	 * angle_{axis}		 : The angle in degrees for given axis
	 * angle_{axis}_string : The angle string in degrees for labels
	 * 
	 * *********************************************************************************/

	// Dimensions
	[HideInInspector] public float width_meters = 0f;
	[HideInInspector] public float height_meters = 0f;
	[HideInInspector] public float depth_meters = 0f;
	[HideInInspector] public float width = 0f;
	[HideInInspector] public float height = 0f;
	[HideInInspector] public float depth = 0f;
	[HideInInspector] public string width_string = "";
	[HideInInspector] public string height_string = "";
	[HideInInspector] public string depth_string = "";
		// Angles
	[HideInInspector] public float angle_xz = 0f;
	[HideInInspector] public float angle_yz = 0f;
	[HideInInspector] public float angle_xy = 0f;
	[HideInInspector] public float angle_v3 = 0f;
	[HideInInspector] public string angle_xz_string = "";
	[HideInInspector] public string angle_yz_string = "";
	[HideInInspector] public string angle_xy_string = "";
	[HideInInspector] public string angle_v3_string = "";
	// Distance
	[HideInInspector] public float center_to_center_meters = 0f; //distance in meters
	[HideInInspector] public float center_to_center = 0f;
	[HideInInspector] public string center_to_center_string = "";
	[HideInInspector] public float edge_to_edge_meters = 0f;
	[HideInInspector] public float edge_to_edge = 0f;
	[HideInInspector] public string edge_to_edge_string = "";

	/* *********************************************************************************
	 *									Conversions
	 * *********************************************************************************/

	private float meter_to_feet = 3.28084f;
	private float meter_to_centimeters = 100f;
	private float meter_to_kilometers = 0.001f;
	private float meter_to_inches = 39.3701f;
	private float meter_to_yards = 1.09361f;
	private float meter_to_miles = 0.000621371f;

	/* *********************************************************************************
	 *								   API Functions
	 * *********************************************************************************/
		
		// Measurement Units
	public MeasurementUnits getMeasurementUnit(){ return MeasurementUnit; }
	public void setMeasurementUnit(MeasurementUnits unit){ MeasurementUnit = unit; }
		
		// Measurement Source
	public MeasurementSources getMeasurementSource() { return MeasurementSource; }
	public void setMeasurementSource(MeasurementSources source) { MeasurementSource = source; }

		// Distance Object
	public GameObject getDistanceObject() { return DistanceObject; }
	public void setDistanceObject(GameObject obj) { DistanceObject = obj; }
		
		// Calculated Values
	public Vector3 getDimensions(){ return new Vector3(width, height, depth); }
	public Vector3 getDimensionsInMeters() { return new Vector3(width_meters, height_meters, depth_meters); }
	public Vector2 getDistance() { return new Vector2(center_to_center, edge_to_edge); }
	public Vector2 getDistanceInMeters() { return new Vector2(center_to_center_meters, edge_to_edge_meters); }
	public Vector4 getAngles() { return new Vector4(angle_xz, angle_yz, angle_xy, angle_v3); }
	
		// Conversion Values
	public float getConversionValue(MeasurementUnits unit)
	{
		switch (unit)
		{
			case (MeasurementUnits.centimeters):
				return meter_to_centimeters;
			case (MeasurementUnits.kilometers):
				return meter_to_kilometers;
			case (MeasurementUnits.yards):
				return meter_to_yards;
			case (MeasurementUnits.inches):
				return meter_to_inches;
			case (MeasurementUnits.feet):
				return meter_to_feet;
			case (MeasurementUnits.miles):
				return meter_to_miles;
			default:
				return 1;
		}
	}
	

	/* *********************************************************************************
	 *								Internal Functions
	 * *********************************************************************************/

	void runCalculations()
	{
		showSourceError = false;
		if (MeasurementSource == MeasurementSources.Renderer)
		{
			if(GetComponent<Renderer>() != null)
				bounds = GetComponent<Renderer>().bounds;
			else
				CheckSource();
		}
		
		if (MeasurementSource == MeasurementSources.Mesh)
		{
			if(GetComponent<Mesh>() != null)
				bounds = GetComponent<Mesh>().bounds;
			else
				CheckSource();
		}
		
		if (MeasurementSource == MeasurementSources.Collider)
		{
			if(GetComponent<Collider>() != null)
				bounds = GetComponent<Collider>().bounds;
			else
				CheckSource();
		}

		width_meters = bounds.size.x;
		height_meters = bounds.size.y;
		depth_meters = bounds.size.z;

		width = unitConversion(width_meters);
		height = unitConversion(height_meters);
		depth = unitConversion(depth_meters);

		width_string = unitConversionString(width);
		height_string = unitConversionString(height);
		depth_string = unitConversionString(depth);

		if (DistanceObject != null)
		{
			center_to_center_meters = Vector3.Distance(DistanceObject.transform.position, transform.position);
			center_to_center = unitConversion(center_to_center_meters);
			center_to_center_string = unitConversionString(center_to_center);

			Vector3 point1 = bounds.ClosestPoint(DistanceObject.transform.position);
			Vector3 point2 = DistanceObject.transform.position;
			if (MeasurementSource == MeasurementSources.Renderer && DistanceObject.GetComponent<Renderer>() != null)
				point2 = DistanceObject.GetComponent<Renderer>().bounds.ClosestPoint(point1);
			if (MeasurementSource == MeasurementSources.Mesh && DistanceObject.GetComponent<Mesh>() != null)
				point2 = DistanceObject.GetComponent<Mesh>().bounds.ClosestPoint(point1);
			if (MeasurementSource == MeasurementSources.Collider && DistanceObject.GetComponent<Collider>() != null)
				point2 = DistanceObject.GetComponent<Collider>().bounds.ClosestPoint(point1);

			edge_to_edge_meters = Vector3.Distance(point1, point2);
			edge_to_edge = unitConversion(edge_to_edge_meters);
			edge_to_edge_string = unitConversionString(edge_to_edge);

			distanceObjectDir = DistanceObject.transform.position - transform.position;
			distanceObjectXZ = distanceObjectYZ = distanceObjectXY = distanceObjectDir;
			distanceObjectYZ.x = distanceObjectXZ.y = distanceObjectXY.z = 0f;

			angle_xz = Vector3.Angle(distanceObjectXZ, transform.forward);
			angle_xz_string = angleString(angle_xz);
			angle_yz = Vector3.Angle(distanceObjectYZ, transform.forward);
			angle_yz_string = angleString(angle_yz);
			angle_xy = Vector3.Angle(distanceObjectXY, transform.right);
			if(angle_xy > 90 && angle_xy <= 180) angle_xy = Mathf.Abs(angle_xy - 180);
			angle_xy_string = angleString(angle_xy);
			angle_v3 = Vector3.Angle(distanceObjectDir, transform.forward);
			angle_v3_string = angleString(angle_v3);
		}
	}

	string unitConversionString(float val)
	{
		switch (MeasurementUnit)
		{
			case (MeasurementUnits.centimeters):
				return val.ToString() + " centimeters";
			case (MeasurementUnits.kilometers):
				return val.ToString() + " kilometers";
			case (MeasurementUnits.yards):
				return val.ToString() + " yards";
			case (MeasurementUnits.inches):
				return val.ToString() + " inches";
			case (MeasurementUnits.feet):
				return val.ToString() + " feet";
			case (MeasurementUnits.miles):
				return val.ToString() + " miles";
			default:
				return val.ToString() + " meters";
		}
	}
	float unitConversion(float val)
	{
		switch (MeasurementUnit)
		{
			case (MeasurementUnits.centimeters):
				return val * meter_to_centimeters;
			case (MeasurementUnits.kilometers):
				return val * meter_to_kilometers;
			case (MeasurementUnits.yards):
				return val * meter_to_yards;
			case (MeasurementUnits.inches):
				return val * meter_to_inches;
			case (MeasurementUnits.feet):
				return val * meter_to_feet;
			case (MeasurementUnits.miles):
				return val * meter_to_miles;
			default:
				return val;
		}
	}

	string angleString(float degrees)
	{
		return degrees.ToString() + "\u00B0";
	}

	void CheckSource()
	{
		showSourceError = false;
		if (GetComponent<Collider>() && MeasurementSource == MeasurementSources.Collider) return;
		if (GetComponent<Mesh>() && MeasurementSource == MeasurementSources.Mesh) return;
		if (GetComponent<Renderer>() && MeasurementSource == MeasurementSources.Renderer) return;

		if (GetComponent<Collider>() && MeasurementSource != MeasurementSources.Collider)
		{
			MeasurementSource = MeasurementSources.Collider;
			return;
		}
		if (GetComponent<Mesh>() && MeasurementSource != MeasurementSources.Mesh)
		{
			MeasurementSource = MeasurementSources.Mesh;
			return;
		}
		if (GetComponent<Renderer>() && MeasurementSource != MeasurementSources.Renderer)
		{
			MeasurementSource = MeasurementSources.Renderer;
			return;
		}

		showSourceError = true;
	}

	void Update()
	{
		runCalculations();
	}

	void Start()
	{
		CheckSource();
	}
}