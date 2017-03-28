﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorManager : MonoBehaviour
{
	[SerializeField]
	private Sphere sphere;

	private WaveGenerator waveGenerator;

	void Start()
	{
		waveGenerator = sphere.GetComponent<WaveGenerator>();
	}

	// Render mode
	public void SetDefaultRenderMode() { sphere.SetRenderMode (Sphere.RenderMode.Default); }
	public void SetWireframeRenderMode() { sphere.SetRenderMode (Sphere.RenderMode.Wireframe); }
	public void SetSolidRenderMode() { sphere.SetRenderMode (Sphere.RenderMode.Solid); }
	public void SetGradientRenderMode() { sphere.SetRenderMode (Sphere.RenderMode.Gradient); }

	public void UseRecalculatedNormals(bool value) { waveGenerator.UseRecalculatedNormals(value); }
}
