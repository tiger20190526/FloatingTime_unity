using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <inheritdoc />
/// <summary>
/// A 2D physics triangle that is drawn by specifying the positions of 
/// its three vertices.
/// </summary>
public class DrawSeg4 : MonoBehaviour 
{
	public Color FillColor = Color.white;
	
	private MeshFilter _meshFilter;

	// Triangle vertices (in absolute coordinates)
	private readonly List<Vector2> _vertices = new List<Vector2>(3);
	
	private void Awake()
	{
		// var vertices2D = new Vector2[] {
		// 	new Vector2(-43.81f,	-66.81f),
		// 	new Vector2(-43.68f,	-64.10f),
		// 	new Vector2(-28.46f,	-51.77f),
		// 	new Vector2(-27.87f,	-51.56f),
		// 	new Vector2(14.45f,	-51.56f),
		// 	new Vector2(15.13f,	-51.85f),
		// 	new Vector2(26.80f,	-63.86f),
		// 	new Vector2(26.68f,	-65.94f),
		// 	new Vector2(21.53f,	-70.11f),
		// 	new Vector2(20.94f,	-70.31f),
		// 	new Vector2(-39.39f,	-70.31f),
		// 	new Vector2(-40.06f,	-70.03f),
		// };
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
			name = "Seg4",
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
			new Vector2(-43.81f,	-66.81f),
			new Vector2(-43.68f,	-64.10f),
			new Vector2(-28.46f,	-51.77f),
			new Vector2(-27.87f,	-51.56f),
			new Vector2(14.45f,	-51.56f),
			new Vector2(15.13f,	-51.85f),
			new Vector2(26.80f,	-63.86f),
			new Vector2(26.68f,	-65.94f),
			new Vector2(21.53f,	-70.11f),
			new Vector2(20.94f,	-70.31f),
			new Vector2(-39.39f,	-70.31f),
			new Vector2(-40.06f,	-70.03f),
		};
		_meshFilter = GetComponent<MeshFilter>();
		_meshFilter.mesh = PolygonMesh(vertices2D, newColor);
	}
}