using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireframeRenderer : MonoBehaviour
{
	// Use this for initialization
	void Start () 
	{
		// Must exist to access 'enabled' in the Editor
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Must exist to access 'enabled' in the Editor
	}

	void OnPreRender()
	{
		GL.wireframe = true;
	}

	void OnPostRender()
	{
		GL.wireframe = false;
	}
}
