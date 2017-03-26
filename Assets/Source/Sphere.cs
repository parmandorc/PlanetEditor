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
		CreateIcosahedron();
	}

	void CreateIcosahedron()
	{
		// Create mesh vertices
		float t = (1.0f + Mathf.Sqrt(5.0f)) * 0.5f;
		Vector3[] vertices = {
			new Vector3(-1.0f,     t,  0.0f).normalized,
			new Vector3( 1.0f,     t,  0.0f).normalized,
			new Vector3(-1.0f,    -t,  0.0f).normalized,
			new Vector3( 1.0f,    -t,  0.0f).normalized,

			new Vector3( 0.0f, -1.0f,     t).normalized,
			new Vector3( 0.0f,  1.0f,     t).normalized,
			new Vector3( 0.0f, -1.0f,    -t).normalized,
			new Vector3( 0.0f,  1.0f,    -t).normalized,

			new Vector3(    t,  0.0f, -1.0f).normalized,
			new Vector3(    t,  0.0f,  1.0f).normalized,
			new Vector3(   -t,  0.0f, -1.0f).normalized,
			new Vector3(   -t,  0.0f,  1.0f).normalized,
		};
		mesh.vertices = vertices;

		// Create mesh triangles
		int[] triangles = {
			0, 11, 5,
			0, 5, 1,
			0, 1, 7,
			0, 7, 10,
			0, 10, 11,

			1, 5, 9,
			5, 11, 4,
			11, 10, 2,
			10, 7, 6,
			7, 1, 8,

			3, 9, 4,
			3, 4, 2,
			3, 2, 6,
			3, 6, 8,
			3, 8, 9,

			4, 9, 5,
			2, 4, 11,
			6, 2, 10,
			8, 6, 7,
			9, 8, 1
		};
		mesh.triangles = triangles;

		// Since the mesh is centered in the origin, the normals are equal to the vectors that are used to create the vertices.
		mesh.normals = vertices;
	}
}
