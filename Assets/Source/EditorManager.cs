using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorManager : MonoBehaviour
{
	[SerializeField]
	private Sphere sphere;

	[SerializeField]
	private WaveEditorUIManager WaveDataEditorPrefab;

	[SerializeField]
	private Transform WaveDataEditorPanel;

	private WaveGenerator waveGenerator;

	void Awake()
	{
		WaveGenerator.OnLoadWaveData += OnLoadWaveData;
	}

	void Start()
	{
		waveGenerator = sphere.GetComponent<WaveGenerator>();
	}

	// Animation duration
	public void SetAnimationDuration(float value) { waveGenerator.SetDuration(value); }

	// Render mode
	public void SetDefaultRenderMode() { sphere.SetRenderMode (Sphere.RenderMode.Default); }
	public void SetWireframeRenderMode() { sphere.SetRenderMode (Sphere.RenderMode.Wireframe); }
	public void SetSolidRenderMode() { sphere.SetRenderMode (Sphere.RenderMode.Solid); }
	public void SetGradientRenderMode() { sphere.SetRenderMode (Sphere.RenderMode.Gradient); }

	// Normals
	public void UseRecalculatedNormals(bool value) { waveGenerator.UseRecalculatedNormals(value); }


	// --- Wave data edition ----

	private void NewWave(WaveData waveData)
	{
		WaveEditorUIManager element = GameObject.Instantiate<WaveEditorUIManager>(WaveDataEditorPrefab, WaveDataEditorPanel);
		element.SetWaveData(waveData);
	}

	public void OnLoadWaveData(WaveDataContainer waveData)
	{
		foreach(WaveData wave in waveData)
		{
			NewWave(wave);
		}
	}

	public void NewWave()
	{
		NewWave(waveGenerator.NewWave());
	}
}
