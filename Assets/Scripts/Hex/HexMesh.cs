using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HexMesh : MonoBehaviour
{

    public const float outerRadius = 10f;
    public const float innerRadius = outerRadius * 0.866025404f;
    public static Vector3[] corners = {
        new Vector3(0f, 0f, outerRadius),
        new Vector3(innerRadius, 0f, 0.5f * outerRadius),
        new Vector3(innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(0f, 0f, -outerRadius),
        new Vector3(-innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(-innerRadius, 0f, 0.5f * outerRadius),
        new Vector3(0f, 0f, outerRadius)
    };

    Mesh hexMesh;
    List<Vector3> vertices;
    List<int> triangles;

    void Awake()
    {
        GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
        GetComponent<MeshCollider>().sharedMesh = hexMesh;
        hexMesh.name = "Hex Mesh";
        vertices = new List<Vector3>();
        triangles = new List<int>();
    }

    // Use this for initialization
    void Start()
    {
        SetUp();
    }

    public void SetUp()
    {
        hexMesh.Clear();
        vertices.Clear();
        triangles.Clear();
        Triangulate();
        hexMesh.vertices = vertices.ToArray();
        hexMesh.triangles = triangles.ToArray();
        hexMesh.RecalculateNormals();
    }

    void Triangulate()
    {
        Vector3 center = this.transform.localPosition;
        for (int i = 0; i < 6; i++)
        {
            AddTriangle(center,
                center + corners[i],
                center + corners[i + 1]);
        }
    }

    void AddTriangle(params Vector3[] vector)
    {
        int vertexIndex = vertices.Count;
        for (int i = 0; i < vector.Length; i++)
        {
            vertices.Add(vector[i]);
            triangles.Add(vertexIndex + i);
        }
    }
}
