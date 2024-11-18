using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private MeshCollider meshCollider;



    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        
    }

    private void Start()
    {

       meshCollider = gameObject.AddComponent<MeshCollider>();

       
    }
    private void Update()
    {
        Mesh mesh = new Mesh();
        lineRenderer.BakeMesh(mesh, true);

        //recalculate vertices
        Vector3[] vertices = mesh.vertices;
        
        for(int i = 0; i<vertices.Length; i++)
        {
            vertices[i] *= -1f;
        }

        mesh.vertices = vertices;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        meshCollider.sharedMesh = mesh;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Is collided");
        }
       
    }
}
