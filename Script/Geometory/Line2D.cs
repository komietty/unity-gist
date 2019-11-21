using UnityEngine;

namespace kmty.gist {
    public class Line2D {
        public float a, b, c;

        public Line2D(float a, float b, float c) {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        public Line2D(Vector2 p0, Vector2 p1) {
            var dx = p1.x - p0.x;
            var dy = p1.y - p0.y;
            this.a = dy;
            this.b = -dx;
            this.c = dx * p0.y - dy * p0.x;
        }

        public bool GetIntersection(Line2D l, out Vector2 point) {
            float d = a * l.b - l.a * b;
            if (d == 0.0) { point = default(Vector2); return false; }
            float x = (b * l.c - l.b * c) / d;
            float y = (l.a * c - a * l.c) / d;
            point = new Vector2(x, y);
            return true;
        }
    }
}
