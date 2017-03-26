using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Sphere))]
public class WaveGenerator : MonoBehaviour
{
	[SerializeField]
	private float WaveAmplitude;

	[SerializeField]
	private float WaveFrequency;

	[SerializeField]
	private float WaveSpeed;

	// The sphere mesh component
	private Sphere sphere;

	// The accumulated time
	private float time;

	void Awake() 
	{
		sphere = GetComponent<Sphere>();
		time = 0.0f;
	}

	void Update()
	{
		time += Time.deltaTime;

		Vector3[] points = sphere.GetPoints();
		float[] heights = new float[points.Length];

		// Determine the height of each point
		for (int i = 0; i < points.Length; i++)
		{
			float angle = Mathf.Acos(points[i].normalized.y);
			heights[i] = 1.0f + Mathf.Sin(time * WaveSpeed + angle * WaveFrequency) * WaveAmplitude;
		}

		sphere.SetRadiuses(heights);
	}
}
