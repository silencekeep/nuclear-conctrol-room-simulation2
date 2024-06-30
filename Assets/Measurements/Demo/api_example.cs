/* A sample of how to retrieve data from the Measurements API. See the documentation for 
 * all the available data available. While in play mode, view the inspector window for
 * the API Example gameobject. By default, the Sphere in the demo is selected, but you 
 * can drop in any gameobject that has the Measurements script attached.
 * 
 * As the sphere moves around randomly, you can see how the distance values change. 
 * As you move and scale the other objects in the scene, the GUI should update as well.
 * */

using UnityEngine;

public class api_example : MonoBehaviour {

	public Measurements measurements;
	public float speed = 1f;
	public float boxSize = 5f;
	public GUIStyle style = GUIStyle.none;
	private float lastTime = 0f;
	private float duration = 3f;
	private Vector3 direction;

	void Update()
	{
		if (measurements != null)
		{
			//moving the object in a new direction every couple of seconds
			if(Time.time > lastTime + duration) {
				direction = new Vector3(Random.Range(-1.0f, 1.0f), 0f, Random.Range(-1.0f, 1.0f)).normalized;
				lastTime = Time.time;
			}
			measurements.transform.position += direction * speed * Time.deltaTime;
			if (measurements.transform.position.x > boxSize || measurements.transform.position.x < -boxSize || measurements.transform.position.z > boxSize || measurements.transform.position.z < -boxSize)
			{
				measurements.transform.position = new Vector3(Mathf.Clamp(measurements.transform.position.x, -boxSize, boxSize), 1, Mathf.Clamp(measurements.transform.position.z, -boxSize, boxSize));
				lastTime = Time.time - duration;
			}
		}
	}

	void OnGUI()
	{
		if (measurements != null)
		{
			Vector3 dimensions = measurements.getDimensions();
			Vector2 distance = measurements.getDistance();
			Vector4 angles = measurements.getAngles();
			GUI.Box(new Rect(1, 1, 300, 130),
				"GameObject: " + measurements.name + "\n" +
				"Measurement Unit: " + measurements.getMeasurementUnit() + "\n" +
				"Measurement Source: " + measurements.getMeasurementSource() + "\n" +
				"\n" +
				"Width: " + dimensions.x + "\n" +
				"Height: " + dimensions.y + "\n" +
				"Depth: " + dimensions.z + "\n" +
				"\n" +
				"Distance Object: " + measurements.getDistanceObject().name + "\n" +
				"center to center: " + distance.x + "\n" +
				"edge to edge: " + distance.y + "\n" +
				"\n" +
				"Angles to " + measurements.getDistanceObject().name + "\n" +
				"XZ: " + angles.x + "\u00B0" + "\n" +
				"YZ: " + angles.y + "\u00B0" + "\n" +
				"XY: " + angles.z + "\u00B0" + "\n" +
				"V3: " + angles.w + "\u00B0",
			style);
		}
	}
}
