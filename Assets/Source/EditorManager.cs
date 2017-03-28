using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorManager : MonoBehaviour
{
	[SerializeField]
	private Sphere sphere;

	[SerializeField]
	private WireframeRenderer wireframe;

	private WaveGenerator waveGenerator;

	void Start()
	{
		waveGenerator = sphere.GetComponent<WaveGenerator>();
	}

	// Render mode
	public void SetDefaultRenderMode() { sphere.SetRenderMode (Sphere.RenderMode.Default); wireframe.enabled = false; }
	public void SetWireframeRenderMode() { sphere.SetRenderMode (Sphere.RenderMode.Solid); wireframe.enabled = true; }
	public void SetSolidRenderMode() { sphere.SetRenderMode (Sphere.RenderMode.Solid); wireframe.enabled = false; }
	public void SetGradientRenderMode() { sphere.SetRenderMode (Sphere.RenderMode.Gradient); wireframe.enabled = false; }

	public void UseRecalculatedNormals(bool value) { waveGenerator.UseRecalculatedNormals(value); }
}
