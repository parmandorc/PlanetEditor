using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorManager : MonoBehaviour
{
	[SerializeField]
	private Sphere sphere;

	// Render mode
	public void SetDefaultRenderMode() { sphere.SetRenderMode (Sphere.RenderMode.Default); }
	public void SetSolidRenderMode() { sphere.SetRenderMode (Sphere.RenderMode.Solid); }
	public void SetGradientRenderMode() { sphere.SetRenderMode (Sphere.RenderMode.Gradient); }
}
