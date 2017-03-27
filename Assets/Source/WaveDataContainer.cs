using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;

// Used for serialization of the wave data
// Based on: http://wiki.unity3d.com/index.php?title=Saving_and_Loading_Data:_XmlSerializer
[XmlRoot("WaveDataContainer")]
[XmlInclude(typeof(WaveData))]
public class WaveDataContainer : System.Collections.IEnumerable
{
	[XmlArray("WaveDataElements")]
	[XmlArrayItem("WaveData")]
	private List<WaveData> waveData;

	public WaveDataContainer()
	{
		waveData = new List<WaveData>();	
	}

	public WaveDataContainer(IEnumerable<WaveData> collection)
	{
		waveData = new List<WaveData>(collection);
	}

	public System.Collections.IEnumerator GetEnumerator()
	{
		foreach (WaveData item in waveData)
		{
			yield return item;
		}
	}

	public WaveData this[int key]
	{
		get
		{
			return waveData[key];
		}
		set
		{
			waveData[key] = value;
		}
	}

	public void Add(System.Object item)
	{
		waveData.Add(item as WaveData);
	}

	public void Save(string path)
	{
		var serializer = new XmlSerializer(typeof(WaveDataContainer));
		try
		{
			using(var stream = new FileStream(path, FileMode.Create))
			{
				serializer.Serialize(stream, this);
			}
		} 
		catch (IOException e)
		{
			Debug.LogException(e);
		}
	}

	public static WaveDataContainer Load(string path)
	{
		var serializer = new XmlSerializer(typeof(WaveDataContainer));
		try
		{
			using(var stream = new FileStream(path, FileMode.Open))
			{
				return serializer.Deserialize(stream) as WaveDataContainer;
			}
		} 
		catch (IOException e) 
		{
			Debug.LogException(e);
			return null;
		}
	}
}
