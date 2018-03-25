using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Study
{
    public class Cylinder : ProceduralModelingBase
    {
        [SerializeField, Range(0.1f, 10f)]
        protected float height = 3f, radius = 1f;

        [SerializeField, Range(3, 32)]
        protected int segments = 16;

        [SerializeField]
        bool openEnded = true;

        const float PI2 = Mathf.PI * 2f;

        void OnValidate()
        {
            Rebuild();
        }

        protected override Mesh Build()
        {
            var mesh = new Mesh();

            var vertices = new List<Vector3>();
            var normals = new List<Vector3>();
            var uvs = new List<Vector2>();
            var triangles = new List<int>();

            float top = height * 0.5f, bottom = -height * 0.5f;

            GenerateCap(segments + 1, top, bottom, radius, vertices, uvs, normals, true);

            var len = (segments + 1) * 2;

            for (int i = 0; i < segments + 1; i++)
            {
                int idx = i * 2;
                int a = idx, b = idx + 1, c = (idx + 2) % len, d = (idx + 3) % len;
                triangles.Add(a);
                triangles.Add(c);
                triangles.Add(b);

                triangles.Add(d);
                triangles.Add(b);
                triangles.Add(c);
            }

            if (openEnded)
            {
                GenerateCap(segments + 1, top, bottom, radius, vertices, uvs, normals, false);

                vertices.Add(new Vector3(0f, top, 0f));
                uvs.Add(new Vector2(0.5f, 1f));
                normals.Add(new Vector3(0f, 1f, 0f));

                vertices.Add(new Vector3(0f, bottom, 0f));
                uvs.Add(new Vector2(0.5f, 0f));
                normals.Add(new Vector3(0f, -1f, 0f));

                var it = vertices.Count - 2;
                var ib = vertices.Count - 1;

                var offset = len;

                for (int i = 0; i < len; i += 2)
                {
                    triangles.Add(it);
                    triangles.Add((i + 2) % len + offset);
                    triangles.Add(i + offset);
                }

                for (int i = 1; i < len; i += 2)
                {
                    triangles.Add(ib);
                    triangles.Add(i + offset);
                    triangles.Add((i + 2) % len + offset);
                }
            }

            mesh.vertices = vertices.ToArray();
            mesh.uv = uvs.ToArray();
            mesh.normals = normals.ToArray();
            mesh.triangles = triangles.ToArray();
            mesh.RecalculateBounds();

            return mesh;
        }

        void GenerateCap(int segments, float top, float bottom, float radius, List<Vector3> vertices, List<Vector2> uvs, List<Vector3> normals, bool side)
        {
            for (int i = 0; i < segments; i++)
            {
                float ratio = (float)i / (segments - 1);

                float rad = ratio * PI2;

                float cos = Mathf.Cos(rad), sin = Mathf.Sin(rad);
                float x = cos * radius, z = sin * radius;
                Vector3 tp = new Vector3(x, top, z), bp = new Vector3(x, bottom, z);

                vertices.Add(tp);
                uvs.Add(new Vector2(ratio, 1f));

                vertices.Add(bp);
                uvs.Add(new Vector2(ratio, 0f));

                if (side)
                {
                    var normal = new Vector3(cos, 0f, sin);
                    normals.Add(normal);
                    normals.Add(normal);
                }
                else
                {
                    normals.Add(new Vector3(0f, 1f, 0f));
                    normals.Add(new Vector3(0f, -1f, 0f));
                }
            }
        }
    }
}