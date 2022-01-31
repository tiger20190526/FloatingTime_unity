using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <inheritdoc />
/// <summary>
/// A 2D physics triangle that is drawn by specifying the positions of 
/// its three vertices.
/// </summary>
public class DrawSeg5 : MonoBehaviour 
{
	public Color FillColor = Color.white;
	
	private MeshFilter _meshFilter;

	// Triangle vertices (in absolute coordinates)
	private readonly List<Vector2> _vertices = new List<Vector2>(3);
	
	private void Awake()
	{
		// var vertices2D = new Vector2[] {
		//     new Vector2(-35f,-41.8f),
		//     new Vector2(-32.52f,-44.37f),
		//     new Vector2(-21.18f,-35.1f),
		//     new Vector2(-17.5f,-7.95f),
		//     new Vector2(-24.07f,-1.25f),
		//     new Vector2(-30.39f,-6.56f),
		// };
		// // Vector3 pos = this.transform.position;
		// // for(int i = 0; i < vertices2D.Length; i++)
		// // {
		// //     vertices2D[i].x += pos.x;
		// //     vertices2D[i].y += pos.y;
		// // }
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
			name = "Seg5",
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
			new Vector2(-43.48f,	-9.83f),
			new Vector2(-43.31f,	-9.22f),
			new Vector2(-34.71f,	-2.39f),
			new Vector2(-32.81f,	-2.51f),
			new Vector2(-25.02f,	-10.53f),
			new Vector2(-24.76f,	-11.31f),
			new Vector2(-29.79f,	-49.51f),
			new Vector2(-30.13f,	-50.11f),
			new Vector2(-45.52f,	-62.58f),
			new Vector2(-47.42f,	-62.46f),
			new Vector2(-49.73f,	-60.68f),
			new Vector2(-49.99f,	-59.3f),
		};
		_meshFilter = GetComponent<MeshFilter>();
		_meshFilter.mesh = PolygonMesh(vertices2D, newColor);
	}
}