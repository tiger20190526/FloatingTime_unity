using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <inheritdoc />
/// <summary>
/// A 2D physics triangle that is drawn by specifying the positions of 
/// its three vertices.
/// </summary>
public class DrawSeg7 : MonoBehaviour 
{
	public Color FillColor = Color.white;
	
	private MeshFilter _meshFilter;

	// Triangle vertices (in absolute coordinates)
	private readonly List<Vector2> _vertices = new List<Vector2>(3);
	
	private void Awake()
	{
		// var vertices2D = new Vector2[] {
		//     new Vector2(-22.4f,-0.09f),
		//     new Vector2(-16.04f,-6.48f),
		//     new Vector2(14.28f,-6.48f),
		//     new Vector2(22.34f,0.09f),
		//     new Vector2(15.98f,6.48f),
		//     new Vector2(-14.34f,6.48f),
		// };
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
			name = "Seg7",
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
			new Vector2(-30.81f,	-1.21f),
			new Vector2(-30.68f,	0.86f),
			new Vector2(-20.43f,	9.17f),
			new Vector2(-19.84f,	9.38f),
			new Vector2(22.48f,	9.38f),
			new Vector2(23.15f,	9.09f),
			new Vector2(30.81f,	1.21f),
			new Vector2(30.68f,	-0.87f),
			new Vector2(20.43f,	-9.17f),
			new Vector2(19.84f,	-9.38f),
			new Vector2(-22.48f,	-9.38f),
			new Vector2(-23.15f,	-9.09f),
		};
		_meshFilter = GetComponent<MeshFilter>();
		_meshFilter.mesh = PolygonMesh(vertices2D, newColor);
	}
}