using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Sphere))]
public class WaveGenerator : MonoBehaviour
{


	// The sphere mesh component
	private Sphere sphere;

	// The accumulated time
	private float time;

	// The wave data objects that define the generation
	private List<WaveData> waveData;
	
	void Awake() 
	{
		sphere = GetComponent<Sphere>();
		time = 0.0f;
	}

	void Start()
	{
		waveData = new List<WaveData> (new WaveData[] {
			new WaveData(0.02f, 10.0f, 0.05f),
			new WaveData(0.01f, 20.0f, 0.05f),
			new WaveData(0.01f, 30.0f, 0.2f),
			new WaveData(0.0015f, 80.0f, 0.5f)
		});
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

			foreach(WaveData wave in waveData)
			{
				heights[i] += wave.GetValue(time, angle);
			}

			heights[i] = 1.0f + heights[i];
		}

		sphere.SetRadiuses(heights);
	}
}
