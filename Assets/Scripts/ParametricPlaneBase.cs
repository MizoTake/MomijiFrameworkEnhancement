using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Study
{
    public abstract class ParametricPlaneBase : Plane
    {

        [SerializeField] protected float depth = 1f;

        protected override Mesh Build()
        {
            var mesh = base.Build();

            var vertices = mesh.vertices;

            var winv = 1f / (widthSegments - 1);
            var hinv = 1f / (heightSegments - 1);

            for (int y = 0; y < heightSegments; y++)
            {
                var ry = y * hinv;
                for (int x = 0; x < widthSegments; x++)
                {
                    var rx = x * winv;

                    int index = y * widthSegments + x;
                    vertices[index].y = Depth(rx, ry);
                }
            }

            mesh.vertices = vertices;
            mesh.RecalculateBounds();

            mesh.RecalculateNormals();

            return mesh;
        }

        protected abstract float Depth(float u, float v);

    }
}