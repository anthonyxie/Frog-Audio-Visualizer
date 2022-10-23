using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Ocean : MonoBehaviour
{
    public int xSize, ySize;
    public int xOffset, yOffset;
    public float xScale, yScale;
    // Start is called before the first frame update
    private void Start()
    {
        Generate2();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private Vector3[] vertices;
    private Mesh mesh;
    private Vector3[] fixedVertices;

    private void Generate2()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";
        vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        fixedVertices = new Vector3[(xSize + 1) * (ySize + 1)];
        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                vertices[i] = new Vector3(xScale * (x + xOffset), 0, yScale * (y + yOffset));
                fixedVertices[i] = new Vector3(xScale * (x + xOffset), 0, yScale * (y + yOffset));
            }
        }

        mesh.vertices = vertices;
        vertices.CopyTo(fixedVertices, 0);

        int[] triangles = new int[xSize * ySize * 6];
        for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++)
        {
            for (int x = 0; x < xSize; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
                triangles[ti + 5] = vi + xSize + 2;
            }
        }
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        GetComponent<MeshCollider>().sharedMesh = null;
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

}

