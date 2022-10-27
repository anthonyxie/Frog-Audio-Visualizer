using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class OceanGrid : MonoBehaviour
{
    public int xSize, ySize;
    public int xOffset, yOffset;
    public float xScale, yScale;
    private float[][] specStorage;
    public static float lowvolumeHeight;
    public static float midvolumeHeight;
    public static float highvolumeHeight;
    // Start is called before the first frame update
    private void Start()
    {
        Generate();
        specStorage = new float[ySize - 1][];
        for (int i = 0; i < specStorage.Length; i++)
        {
            specStorage[i] = new float[xSize - 1];
        }

    }

    // Update is called once per frame
    void Update()
    {
        float[] wf = ChunityAudioInput.the_waveform;
        float volume = wf.Max() - wf.Min();
        if (volume < 0.1)
        {
            volume = 0f;
        }
        float[] spectrum = ChunityAudioInput.the_spectrum;


        
        for (int i = specStorage.Length - 1; i > 0; i--)
        {
            specStorage[i - 1].CopyTo(specStorage[i], 0);
        }
        spectrum.CopyTo(specStorage[0], 0);


        floatHeightCalc();



        //store spectrum history in 

        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                if ((y == ySize || y == 0) || (x == xSize || x == 0))
                {
                    vertices[i] = new Vector3(fixedVertices[i].x, 0, fixedVertices[i].z);
                }
                else
                {
                    vertices[i] = new Vector3(fixedVertices[i].x, Mathf.Sqrt(Mathf.Sqrt(specStorage[y - 1][x - 1])) * 10, fixedVertices[i].z);
                }
            }

            mesh.vertices = vertices;
        }
    }

    private Vector3[] vertices;
    private Mesh mesh;
    private Vector3[] fixedVertices;

    private void Generate()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";
        vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        fixedVertices = new Vector3[(xSize + 1) * (ySize + 1)];
        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                vertices[i] = new Vector3(xScale*(x + xOffset), 0, yScale*(y + yOffset));
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
    }

    private void OnDrawGizmos()
    {
        //visualize vertex
        if (vertices == null)
        {
            return;
        }
        Gizmos.color = Color.black;
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.01f);
        }
    }

    private void floatHeightCalc()
    {
        float vH = 0;
        for (int i = 0; i <= 40; i++)
        {
            vH += Mathf.Sqrt(Mathf.Sqrt(specStorage[4][30 + i])) * 10;
        }
        lowvolumeHeight = vH / 40;
        vH = 0;
        for (int i = 0; i <= 50; i++)
        {
            vH += Mathf.Sqrt(Mathf.Sqrt(specStorage[4][220 + i])) * 10;
        }
        midvolumeHeight = vH / 50;
        vH = 0;
        for (int i = 0; i <= 30; i++)
        {
            vH += Mathf.Sqrt(Mathf.Sqrt(specStorage[4][320 + i])) * 10;
        }
        highvolumeHeight = vH / 30;
    }
}
