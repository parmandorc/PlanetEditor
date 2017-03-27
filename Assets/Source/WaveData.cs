using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class represents all the data for a wave
public class WaveData
{
	public float amplitude;
	public float frequency;
	public float speed;
	public float offset;

	public WaveData() {}

	public WaveData(float _amplitude, float _frequency, float _speed, float _offset = 0.0f)
	{
		amplitude = _amplitude;
		frequency = _frequency;
		speed = _speed;
		offset = _offset;
	}

	// Returns the function value
	public float GetValue(float t, float delta)
	{
		return Mathf.Sin((t * speed + delta) * frequency + offset) * amplitude;
	}
}
