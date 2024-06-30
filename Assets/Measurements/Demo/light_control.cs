using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* *********************************************************************
 * Light control script to read the distance of any measurements component
 * and control the intensity of any connected point light. The closer the
 * distance, the more intense the light. 
 * *********************************************************************/

public class light_control : MonoBehaviour {

	public Measurements measurements;
	public Light pointlight;
	Vector2 distance;

	private void Start()
	{
		if (!measurements) measurements = GetComponent<Measurements>();
		if (!pointlight) pointlight = GetComponentInChildren<Light>();
	}

	// Update is called once per frame
	void Update () {
		if (measurements && pointlight) {
			distance = measurements.getDistance();
			pointlight.intensity = Mathf.Clamp(4 - distance.y, 0, 4);
		}
	}
	
}
