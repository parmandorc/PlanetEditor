using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

// This class represents all the data for a wave
public class WaveData
{
	[XmlAttribute("amplitude")]
	public float amplitude;

	[XmlAttribute("frequency")]
	public float frequency;

	[XmlAttribute("speed")]
	public float speed;

	[XmlAttribute("offset")]
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
