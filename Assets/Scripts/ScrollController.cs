using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class ScrollController : MonoBehaviour
{
    public Vector3 size;
    public Vector3 density;
    public Vector3 position;

    public float radius;
    public int loops;
    public int count;

    public int temp;
    int prevTemp;

    public bool debug;

    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    Vector3[] spiral;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        GenerateVertices();
        ConnectTriangles();
        UpdateMesh();

        GenerateSpiral();
        ApplySpiral(temp);

        prevTemp = temp;
    }

    void Update()
    {
        if (temp != prevTemp)
        {
            GenerateSpiral();
            ApplySpiral(temp);

            prevTemp = temp;
        }
    }

    void GenerateVertices()
    {
        density.y = 2;

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

        int numVertices = (int) density.x * (int) density.z * (int) density.y;

        vertices = new Vector3[numVertices];

        int i = 0;

        for (int zi = 0; zi < density.z; zi++)
        {
            for (int xi = 0; xi < density.x; xi++)
            {
                    vertices[i] = position + new Vector3(xi * size.x / (density.x - 1), size.y / 2, zi * size.z / (density.z - 1));
                        
                    if (debug)
                    {
                        Debug.Log("Vertex at " + i + ": " + vertices[i]);
                    }

                i++;
            }
        }

        for (int zi = 0; zi < density.z; zi++)
        {
            for (int xi = 0; xi < density.x; xi++)
            {
                vertices[i] = position + new Vector3(xi * size.x / (density.x - 1), -size.y / 2, zi * size.z / (density.z - 1));

                if (debug)
                {
                    Debug.Log("Vertex at " + i + ": " + vertices[i]);
                }

                i++;
            }
        }
    }

    void ConnectTriangles()
    {
        if (vertices.Length == 0)
        {
            return;
        }

        int numTriangles = 4 * ((int) density.x - 1) * ((int) density.z - 1) + 4 * ((int) density.x - 1) + 4 * ((int)density.z - 1);

        triangles = new int[numTriangles * 3];

        int index = 0;

        //front surface
        for (int i = 0; i < (density.z - 1) * density.x; i++)
        {
            if ((i + 1) % (int) density.x > 0)
            {
                triangles[index++] = i + (int) density.x;
                triangles[index++] = i + 1;
                triangles[index++] = i;

                if (debug)
                {
                    Debug.Log("Triangle at (" + (index - 3) + "-" + (index - 1) + "): " + triangles[index - 3] + "," + triangles[index - 2] + "," + triangles[index - 1]);
                }

                triangles[index++] = i + (int) density.x;
                triangles[index++] = i + 1 + (int) density.x;
                triangles[index++] = i + 1;

                if (debug)
                {
                    Debug.Log("Triangle at (" + (index - 3) + "-" + (index - 1) + "): " + triangles[index - 3] + "," + triangles[index - 2] + "," + triangles[index - 1]);
                }
            }
        }

        //bottom surface
        for (int i = (int)density.x * (int)density.z; i < (int)density.x * (int)density.z + (density.z - 1) * density.x; i++)
        {
            if ((i + 1) % (int)density.x > 0)
            {
                triangles[index++] = i;
                triangles[index++] = i + 1;
                triangles[index++] = i + (int)density.x;

                if (debug)
                {
                    Debug.Log("Triangle at (" + (index - 3) + "-" + (index - 1) + "): " + triangles[index - 3] + "," + triangles[index - 2] + "," + triangles[index - 1]);
                }

                triangles[index++] = i + 1;
                triangles[index++] = i + 1 + (int)density.x;
                triangles[index++] = i + (int)density.x;

                if (debug)
                {
                    Debug.Log("Triangle at (" + (index - 3) + "-" + (index - 1) + "): " + triangles[index - 3] + "," + triangles[index - 2] + "," + triangles[index - 1]);
                }
            }
        }

        //sides- top
        for (int i = 0; i < (density.x - 1); i++)
        {
            triangles[index++] = i;
            triangles[index++] = i + 1;
            triangles[index++] = i + (int)density.x * (int)density.z;

            if (debug)
            {
                Debug.Log("Triangle at (" + (index - 3) + "-" + (index - 1) + "): " + triangles[index - 3] + "," + triangles[index - 2] + "," + triangles[index - 1]);
            }

            triangles[index++] = i + 1;
            triangles[index++] = i + 1 + (int)density.x * (int)density.z;
            triangles[index++] = i + (int)density.x * (int)density.z;

            if (debug)
            {
                Debug.Log("Triangle at (" + (index - 3) + "-" + (index - 1) + "): " + triangles[index - 3] + "," + triangles[index - 2] + "," + triangles[index - 1]);
            }
        }

        //sides- bottom
        for (int i = (int) density.x * ((int) density.z - 1); i < (int) density.x * ((int) density.z - 1) + (density.x - 1); i++)
        {
            triangles[index++] = i + (int)density.x * (int)density.z;
            triangles[index++] = i + 1;
            triangles[index++] = i;

            if (debug)
            {
                Debug.Log("Triangle at (" + (index - 3) + "-" + (index - 1) + "): " + triangles[index - 3] + "," + triangles[index - 2] + "," + triangles[index - 1]);
            }

            triangles[index++] = i + (int)density.x * (int)density.z;
            triangles[index++] = i + 1 + (int)density.x * (int)density.z;
            triangles[index++] = i + 1;

            if (debug)
            {
                Debug.Log("Triangle at (" + (index - 3) + "-" + (index - 1) + "): " + triangles[index - 3] + "," + triangles[index - 2] + "," + triangles[index - 1]);
            }
        }

        //sides- left
        for (int i = 0; i < (density.z - 1) * density.x; i += (int)density.x)
        {
            triangles[index++] = i + (int)density.x * (int)density.z;
            triangles[index++] = i + (int)density.x;
            triangles[index++] = i;

            if (debug)
            {
                Debug.Log("Triangle at (" + (index - 3) + "-" + (index - 1) + "): " + triangles[index - 3] + "," + triangles[index - 2] + "," + triangles[index - 1]);
            }

            triangles[index++] = i + (int)density.x * (int)density.z;
            triangles[index++] = i + (int)density.x + (int)density.x * (int)density.z;
            triangles[index++] = i + (int)density.x;

            if (debug)
            {
                Debug.Log("Triangle at (" + (index - 3) + "-" + (index - 1) + "): " + triangles[index - 3] + "," + triangles[index - 2] + "," + triangles[index - 1]);
            }
        }

        for (int i = (int)density.x - 1; i < (density.z - 1) * density.x + (int)density.x - 1; i += (int)density.x)
        {
            triangles[index++] = i;
            triangles[index++] = i + (int)density.x;
            triangles[index++] = i + (int)density.x * (int)density.z;

            if (debug)
            {
                Debug.Log("Triangle at (" + (index - 3) + "-" + (index - 1) + "): " + triangles[index - 3] + "," + triangles[index - 2] + "," + triangles[index - 1]);
            }

            triangles[index++] = i + (int)density.x;
            triangles[index++] = i + (int)density.x + (int)density.x * (int)density.z;
            triangles[index++] = i + (int)density.x * (int)density.z;

            if (debug)
            {
                Debug.Log("Triangle at (" + (index - 3) + "-" + (index - 1) + "): " + triangles[index - 3] + "," + triangles[index - 2] + "," + triangles[index - 1]);
            }
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }

    void GenerateSpiral()
    {
        float rotation = -Mathf.PI / 2;
        float offset = radius;
        int flip = 1;

        float length = loops * 2 * Mathf.PI;
        float b = radius / length;

        spiral = new Vector3[count];
        
        for (int i = 0; i < count; i++)
        {
            float theta = length * i / (count - 1) + rotation;
            float r = b * (theta - rotation);
            spiral[count - 1 - i] = new Vector3(0, Mathf.Sin(theta) * r + offset, flip * Mathf.Cos(theta) * r);
        }
    }

    void ApplySpiral(int section)
    {
        for (int i = 0; i < section; i++)
        {
            for (int xi = 0; xi < density.x; xi++)
            {
                Vector3 point = vertices[(section - 1 - i) * (int)density.x + xi];
                vertices[(section - 1 - i) * (int)density.x + xi] = new Vector3(point.x, position.y + size.y / 2 + spiral[i].y, position.z + spiral[i].z);

                point = vertices[(section - 1 - i) * (int)density.x + xi + (int)density.x * (int)density.z];
                vertices[(section - 1 - i) * (int)density.x + xi + (int)density.x * (int)density.z] = new Vector3(point.x, position.y - size.y / 2 + spiral[i].y, position.z + spiral[i].z);
            }
        }

        UpdateMesh();
    }
}
