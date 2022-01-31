using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <inheritdoc />
/// <summary>
/// A 2D physics triangle that is drawn by specifying the positions of 
/// its three vertices.
/// </summary>
public class DrawSeg3 : MonoBehaviour 
{
	public Color FillColor = Color.white;
	
	private MeshFilter _meshFilter;

	// Triangle vertices (in absolute coordinates)
	private readonly List<Vector2> _vertices = new List<Vector2>(3);
	
	private void Awake()
	{
		// Vector3 pos = this.transform.position;
		// for(int i = 0; i < vertices2D.Length; i++)
		// {
		//     vertices2D[i].x += pos.x;
		//     vertices2D[i].y += pos.y;
		// }
		// _meshFilter = GetComponent<MeshFilter>();
		// _meshFilter.mesh = PolygonMesh(vertices2D, FillColor);
	}

	/// <summary>
	/// Creates and returns a polygon mesh given a list of its vertices.
	/// </summary>
	private static Mesh PolygonMesh(Vector2[] vertices, Color fillColor)
	{
		// Find all the triangles in the shape
		var triangles = new Triangulator(vertices).Triangulate();
		
		// Assign each vertex the fill color
		var colors = Enumerable.Repeat(fillColor, vertices.Length).ToArray();

		var mesh = new Mesh {
			name = "Seg3",
			vertices = vertices.ToVector3(),
			triangles = triangles,
			colors = colors
		};
		
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
		mesh.RecalculateTangents();

		return mesh;
	}
	void UpdateColor(Color newColor)
	{
		FillColor = newColor;
		var vertices2D = new Vector2[] {
			new Vector2(21.77f,	-11.43f),
			new Vector2(22.11f,	-10.83f),
			new Vector2(32.52f,	-2.39f),
			new Vector2(34.42f,	-2.51f),
			new Vector2(40.65f,	-8.92f),
			new Vector2(40.91f,	-9.7f),
			new Vector2(34.37f,	-59.38f),
			new Vector2(34.03f,	-59.99f),
			new Vector2(30.7f,	-62.68f),
			new Vector2(28.81f,	-62.57f),
			new Vector2(16.99f,	-50.41f),
			new Vector2(16.74f,	-49.63f),
		};
		_meshFilter = GetComponent<MeshFilter>();
		_meshFilter.mesh = PolygonMesh(vertices2D, newColor);
	}
}