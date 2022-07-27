using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class ScrollController : MonoBehaviour
{
    public Vector3 size;
    public Vector3 density;
    public Vector3 position;

    public bool debug;

    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        GenerateVertices();
        ConnectTriangles();
        UpdateMesh();
    }

    void GenerateVertices()
    {
        if (density.x < 2)
        {
            Debug.Log("density.x needs to be at least 2.");
            return;
        }

        if (density.z < 2)
        {
            Debug.Log("density.z needs to be at least 2.");
            return;
        }

        int numVertices = 0;

        if (density.y < 1)
        {
            Debug.Log("density.y needs to be at least 1.");
            return;
        } 
        else if (density.y < 3)
        {
            numVertices = (int) density.x * (int) density.z * (int) density.y;
        } 
        else
        {
            numVertices = (int)density.x * (int)density.z * 2 + ((int)density.y - 2) * (2 * (int)density.x + 2 * (int)density.z - 4);
        }

        vertices = new Vector3[numVertices];

        int i = 0;

        for (int yi = 0; yi < density.y; yi++)
        { 
            for (int zi = 0; zi < density.z; zi++)
            {
                for (int xi = 0; xi < density.x; xi++)
                {
                    if (yi == 0 || yi == density.y - 1)
                    {
                        vertices[i] = position + new Vector3(xi * size.x / (density.x - 1), yi * size.y / (density.y), zi * size.z / (density.z - 1));
                        
                        if (debug)
                        {
                            Debug.Log("Vertex at " + i + ": " + vertices[i]);
                        }

                        i++;
                    }
                }
            }
        }
        
    }

    void ConnectTriangles()
    {
        if (vertices.Length == 0)
        {
            return;
        }

        int numTriangles = 2 * ((int) density.x - 1) * ((int) density.z - 1);

        triangles = new int[numTriangles * 3];

        int index = 0;

        for (int i = 0; i < (density.z - 1) * density.x; i++)
        {
            if ((i + 1) % (int) density.x > 0)
            {
                triangles[index++] = i + (int)density.x;
                triangles[index++] = i + 1;
                triangles[index++] = i;

                if (debug)
                {
                    Debug.Log("Triangle at (" + (index - 3) + "-" + (index - 1) + "): " + triangles[index - 3] + "," + triangles[index - 2] + "," + triangles[index - 1]);
                }

                triangles[index++] = i + (int)density.x;
                triangles[index++] = i + 1 + (int)density.x;
                triangles[index++] = i + 1;

                if (debug)
                {
                    Debug.Log("Triangle at (" + (index - 3) + "-" + (index - 1) + "): " + triangles[index - 3] + "," + triangles[index - 2] + "," + triangles[index - 1]);
                }
            }
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }
}
