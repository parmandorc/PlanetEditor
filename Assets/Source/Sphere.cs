using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Sphere : MonoBehaviour 
{
	[SerializeField]
	private uint RecursionLevel;

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

		// Create sphere via recursive subdivision
		for (int i = 0; i < RecursionLevel; i++)
		{
			SubdivideMesh();
		}
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

	// Subdivides the current mesh by dividing each triangle into 4 triangles from each edge's middlepoint.
	void SubdivideMesh()
	{
		List<Vector3> newVertices = new List<Vector3>(mesh.vertices);
		List<int> newTriangles = new List<int>();

		// Create a cache for middle points, so no duplicate vertices are created
		Dictionary<Vector2, int> cache = new Dictionary<Vector2, int>();

		for (int i = 0; i < mesh.triangles.Length; i += 3)
		{
			// Create triangle of middle points
			for (int j = 0; j < 3; j++)
			{
				Vector2 key = new Vector2(mesh.triangles[i + j], mesh.triangles[i + (j + 1) % 3]);
				if (key.x > key.y)
					key = new Vector2 (key.y, key.x); // Make key canonic: always smaller index first

				if (!cache.ContainsKey(key))
				{
					// Calculate middle point
					newVertices.Add(((newVertices[mesh.triangles[i + j]] + newVertices[mesh.triangles[i + (j + 1) % 3]]) * 0.5f).normalized);
					cache.Add(key, newVertices.Count - 1);
				}

				newTriangles.Add(cache[key]);
			}

			// Create the other three triangles
			int a = newTriangles[newTriangles.Count - 3];
			int b = newTriangles[newTriangles.Count - 2];
			int c = newTriangles[newTriangles.Count - 1];
			newTriangles.AddRange(new int[] {
				mesh.triangles[i], a, c,
				mesh.triangles[i + 1], b, a,
				mesh.triangles[i + 2], c, b
			});
		}

		// Assign new data to mesh
		mesh.vertices = newVertices.ToArray();
		mesh.triangles = newTriangles.ToArray();
		mesh.normals = newVertices.ToArray();
	}
}
