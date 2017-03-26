using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Sphere : MonoBehaviour 
{
	// The mesh of the sphere
	private Mesh mesh;

	void Awake() 
	{
		// Create the mesh component
		mesh = new Mesh();
		mesh.name = gameObject.name + "_mesh";
		GetComponent<MeshFilter>().mesh = mesh;
	}

	void Start()
	{
		// Create mesh vertices
		Vector3[] vertices = {
			new Vector3( 1.0f, 0.0f,  1.0f),
			new Vector3( 1.0f, 0.0f, -1.0f),
			new Vector3(-1.0f, 0.0f, -1.0f),
			new Vector3(-1.0f, 0.0f,  1.0f)
		};
		mesh.vertices = vertices;

		// Create mesh triangles
		int[] triangles = {
			0, 1, 2,
			2, 3, 0
		};
		mesh.triangles = triangles;
	}
}
