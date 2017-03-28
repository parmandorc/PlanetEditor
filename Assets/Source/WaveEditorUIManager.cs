using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveEditorUIManager : MonoBehaviour
{
	private EditorManager editorManager;

	[SerializeField]
	private Slider AmplitudeSlider;

	[SerializeField]
	private Slider FrequencySlider;

	[SerializeField]
	private Slider SpeedSlider;

	[SerializeField]
	private Slider OffsetSlider;

	private WaveData waveData;

	private bool ignoreInput = false;

	void Start()
	{
		// Find parent EditorManager. Need to search for it, since the reference is lost in the prefab.
		for (Transform parent = transform.parent; editorManager == null && parent != null; parent = parent.parent)
		{
			editorManager = parent.GetComponent<EditorManager>();
		}
	}

	public void SetWaveData(WaveData _waveData) 
	{ 
		waveData = _waveData;	

		ignoreInput = true;
		{
			AmplitudeSlider.value = waveData.amplitude;
			FrequencySlider.value = waveData.frequency;
			SpeedSlider.value = waveData.speed;
			OffsetSlider.value = waveData.offset;
		}
		ignoreInput = false;
	}

	// Parameters setting
	public void SetAmplitude(float value) { if (!ignoreInput) waveData.amplitude = value; }
	public void SetFrequency(float value) { if (!ignoreInput) waveData.frequency = value; }
	public void SetSpeed(float value) { if (!ignoreInput) waveData.speed = value; }
	public void SetOffset(float value) { if (!ignoreInput) waveData.offset = value; }

	// Wave data deletion
	public void Delete() 
	{ 
		if (!ignoreInput) 
		{
			editorManager.DeleteWave(waveData);
			Destroy(gameObject);
		}
	}
}
