using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Tetrahedron : MonoBehaviour
{
    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        // Create and assign a simple material
        meshRenderer.material = new Material(Shader.Find("Standard"));

        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;

        // Define vertices
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(1, 1, 1),  // Vertex 0
            new Vector3(-1, -1, 1), // Vertex 1
            new Vector3(-1, 1, -1), // Vertex 2
            new Vector3(1, -1, -1)  // Vertex 3
        };

        // Define triangles
        int[] triangles = new int[]
        {
            0, 1, 2,  // Triangle 1
            0, 2, 3,  // Triangle 2
            0, 3, 1,  // Triangle 3
            1, 3, 2   // Triangle 4 (base)
        };

        // Assign vertices and triangles to the mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        // Optional: Calculate normals for proper lighting
        mesh.RecalculateNormals();
    }
}
