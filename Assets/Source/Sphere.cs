using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Sphere : MonoBehaviour 
{
	public enum RenderMode {Default, Wireframe, Solid, Gradient};

	[SerializeField]
	private uint RecursionLevel;

	[SerializeField]
	private Material DefaultMaterial;

	[SerializeField]
	private Material VertexColorMaterial;

	[SerializeField]
	private Color SolidColor;

	[SerializeField]
	private Gradient ColorGradient;

	// The mesh of the sphere
	private Mesh mesh;

	private RenderMode renderMode;

	new private Renderer renderer;

	void Awake() 
	{
		// Create the mesh component
		mesh = new Mesh();
		mesh.name = gameObject.name + "_mesh";
		GetComponent<MeshFilter>().mesh = mesh;
		renderer = GetComponent<Renderer> ();
		renderer.material = DefaultMaterial;
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

	public Vector3[] GetPoints()
	{
		return mesh.vertices;
	}
		
	public void SetRadiuses(float[] radiuses)
	{
		Vector3[] vertices = mesh.vertices;
		Color[] colors = new Color[vertices.Length];

		if (radiuses.Length != vertices.Length) 
		{
			Debug.LogError("The array of radiuses provided doesn't match the length of the array of vertices of the mesh.");
		}

		for (int i = 0; i < vertices.Length; i++) 
		{
			vertices[i] = vertices[i].normalized * radiuses[i];

			// Set vertex color
			if (renderMode == RenderMode.Gradient)
			{
				colors [i] = ColorGradient.Evaluate (radiuses [i] * 0.5f); // radiuses[i] is in range [0.0f, 2.0f], need in [0.0f, 1.0f]
			} 
			else 
			{
				colors [i] = SolidColor;
			}
		}

		mesh.vertices = vertices;
		mesh.colors = colors;
	}

	public void SetNormals(Vector3[] normals)
	{
		mesh.normals = normals;
	}

	public void SetRenderMode(RenderMode mode)
	{
		// Set appropriate material
		if (renderMode == RenderMode.Default && mode != RenderMode.Default) 
		{
			renderer.material = VertexColorMaterial;
		} 
		else if (renderMode != RenderMode.Default && mode == RenderMode.Default) 
		{
			renderer.material = DefaultMaterial;
		}

		// Set appropriate mesh topology (wireframe or not)
		if (renderMode == RenderMode.Wireframe && mode != RenderMode.Wireframe) 
		{
			mesh.SetIndices(mesh.GetIndices(0), MeshTopology.Triangles, 0);
			renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
		} 
		else if (renderMode != RenderMode.Wireframe && mode == RenderMode.Wireframe) 
		{
			mesh.SetIndices(mesh.GetIndices(0), MeshTopology.Lines, 0);
			renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
		}

		renderMode = mode;
	}
}
