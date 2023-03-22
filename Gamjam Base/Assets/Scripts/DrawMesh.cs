using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawMesh : MonoBehaviour
{
    private Mesh mesh;
    public MeshFilter meshFilter;

    public ComputeShader computeShader;
    private ComputeBuffer computeBuffer;
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        meshFilter.mesh = mesh;

        int kernel = computeShader.FindKernel("CSMain");
        computeBuffer = new ComputeBuffer(99, 12);

        // Set the ComputeBuffer as a buffer parameter for the "CSMain" kernel
        computeShader.SetBuffer(kernel, "vertices", computeBuffer);
        computeShader.SetBuffer(kernel, "pos", computeBuffer);

        // Dispatch the "CSMain" kernel with a thread group size of 10x10x1
        computeShader.Dispatch(kernel, 10, 10, 1);
        Vector3[] vertecies = new Vector3[99];
        computeBuffer.GetData(vertecies);

        for (int i = 0; i < 99; i++)
        {
            print(vertecies[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {

        Vector3[] vertecies = new Vector3[99];
        int[] tries = new int[99];
        for (int i = 0; i < 99; i++)
        {
            vertecies[i] = Random.insideUnitSphere * 2;
        }
        for (int i = 0; i < 99; i++)
        {
            tries[i] = i;
        }
        mesh.vertices = vertecies;
        mesh.triangles = tries;
    }
}
