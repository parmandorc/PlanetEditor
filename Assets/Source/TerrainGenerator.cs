using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Sphere))]
public class TerrainGenerator : MonoBehaviour 
{
	const float NoiseSpaceBounds = 10000.0f;

	// The sphere mesh component
	private Sphere sphere;

	void Awake() 
	{
		sphere = GetComponent<Sphere>();
	}

	void Start()
	{
		Vector3 seedOffset = new Vector3(
			Random.Range(-NoiseSpaceBounds, NoiseSpaceBounds),
			Random.Range(-NoiseSpaceBounds, NoiseSpaceBounds),
			Random.Range(-NoiseSpaceBounds, NoiseSpaceBounds)
		);

		Vector3[] points = sphere.GetPoints();
		float[] heights = new float[points.Length];

		// Determine the height of each point
		for (int i = 0; i < points.Length; i++)
		{
			heights[i] = 0.875f + Noise.FractalPerlin3D(seedOffset + points[i], 1.0f, 5, 2.0f, 0.5f, 1.0f, 0.0f) * 0.75f;
		}

		sphere.SetRadiuses(heights);
	}
}
