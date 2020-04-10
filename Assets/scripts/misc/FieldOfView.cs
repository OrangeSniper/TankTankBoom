using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class FieldOfView : MonoBehaviour
{
    [SerializeField]private LayerMask layerMask;
    private Mesh mesh;
    float fov;
    Vector3 origin;
    float startingAngle;
    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        fov = 90f;
        origin = Vector3.zero;
    }


    
    private void LateUpdate()
    {
        mesh.bounds = new Bounds(origin, Vector3.one * 10000000f);

        int raycount = 50;
        float angle = startingAngle;
        float angleIncrease = fov / raycount;
        float veiwDistance = 10f;


        Vector3[] vertices = new Vector3[raycount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[raycount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int TriangleIndex = 0;
        for (int i = 0; i <= raycount; i++)
        {
            

            Vector3 vertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, AngleToVector(angle), veiwDistance, layerMask);
            if (raycastHit2D.collider == null)
            {
                vertex = origin + AngleToVector(angle) * veiwDistance;
            }
            else
            {
                vertex = raycastHit2D.point;
            }
            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[TriangleIndex + 0] = 0;
                triangles[TriangleIndex + 1] = vertexIndex - 1;
                triangles[TriangleIndex + 2] = vertexIndex;

                TriangleIndex += 3;
            }
            vertexIndex++;

            angle -= angleIncrease;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        
        
    }

    public Vector3 AngleToVector(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    public float VectorToAngle(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if(n < 0)
        {
            n += 360;
        }
        return n;
    }

    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }
    public void SetAimDirection(Vector3 aimDir)
    {
        startingAngle = (VectorToAngle(aimDir) - fov / 2f) +90f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
    }
}
