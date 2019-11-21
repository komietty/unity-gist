using UnityEngine;

namespace kmty.gist {

    public class Circle2D {

        public Vector2 Center => center;
        public float Radius => radius;

        Vector2 center;
        float radius;

        public Circle2D(Vector2 c, float r) {
            center = c;
            radius = r;
        }
    }
}
