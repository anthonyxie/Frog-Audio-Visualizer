using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Grid : MonoBehaviour
{
    public int xSize, ySize;
    public int xOffset, yOffset;
    public float xScale, yScale;
    // Start is called before the first frame update
    private void Start()
    {
        Generate();
        Debug.Log("started");
    }


    // Update is called once per frame
    private float freqScaling;
    void Update()
    {
        float[] wf = AudioInput.waveform;
        float volume = wf.Max() - wf.Min();
        if (volume < 0.1)
        {
            volume = 0f;
        }

        freqScaling = 0;
        if (this.transform.parent.gameObject.CompareTag("Highs"))
        {
            freqScaling = Mathf.Sqrt(MouthRotation.higher * 2.2f);
        }
        if (this.transform.parent.gameObject.CompareTag("Mids"))
        {
            freqScaling = Mathf.Sqrt(MouthRotation.midder * 1.2f);
        }
        if (this.transform.parent.gameObject.CompareTag("Lows"))
        {
            freqScaling = Mathf.Sqrt(MouthRotation.lower * 1.1f);
        }
        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                float n = 0.5f + Mathf.Sqrt(volume / 2f) + (freqScaling / 6);
                vertices[i] = new Vector3(vertices[i].x, xScale * wf[i] * 8, n * fixedVertices[i].z);
            }

            mesh.vertices = vertices;
        }
        GetComponent<MeshCollider>().sharedMesh = null;
        GetComponent<MeshCollider>().sharedMesh = mesh;
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
        GetComponent<MeshCollider>().sharedMesh = null;
        GetComponent<MeshCollider>().sharedMesh = mesh;
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
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }
}
