using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[RequireComponent(typeof(Sphere))]
public class WaveGenerator : MonoBehaviour
{
	public const string dataFileName = "wavedata.xml";

	// The sphere mesh component
	private Sphere sphere;

	// The accumulated time
	private float time;

	// The wave data objects that define the generation
	private WaveDataContainer waveData;
	
	void Awake() 
	{
		sphere = GetComponent<Sphere>();
		time = 0.0f;
	}

	void Start()
	{
		// Load wave data from file
		waveData = WaveDataContainer.Load(Path.Combine(Application.persistentDataPath, dataFileName));

		if (waveData == null)
		{
			waveData = WaveDataContainer.Load(Path.Combine(Application.dataPath, dataFileName));
		}
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
