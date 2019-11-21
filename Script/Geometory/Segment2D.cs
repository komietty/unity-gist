using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace kmty.gist {
    public class Segment2D {

        public Vector2 A => a;
        public Vector2 B => b;

        Vector2 a, b;

        public Segment2D(Vector2 a, Vector2 b) {
            this.a = a;
            this.b = b;
        }

        public Line2D GetLine() {
            return new Line2D(a, b);
        }

        public bool Intersects(Line2D l) {
            float t1 = l.a * a.x + l.b * a.y + l.c;
            float t2 = l.a * b.x + l.b * b.y + l.c;
            return t1 * t2 <= 0;
        }

        public bool Intersects(Circle2D c, float lineThickness = 0f) {
            var v1 = b - a;
            var v2 = c.Center - a;
            var v3 = v1 * (Vector2.Dot(v1, v2) / Mathf.Pow(v1.magnitude, 2f));
            var proj = a + v3;
            var pc = proj - a;
            var ph = proj - b;
            var d = Vector2.Distance(proj, c.Center);
            return (d < (c.Radius + lineThickness)) && (Vector2.Dot(pc, ph) < 0f);
        }

        public bool Intersects(Segment2D s) {
            return Intersects(s.GetLine()) && s.Intersects(GetLine());
        }

        public bool Intersects(Bounds b) {
            var min = new Vector2(b.min.x, b.min.y);
            var max = new Vector2(b.max.x, b.max.y);

            var s = new Segment2D(new Vector2(min.x, max.y), new Vector2(min.x, min.y));
            if (Intersects(s)) return true;

            s = new Segment2D(new Vector2(min.x, max.y), new Vector2(max.x, max.y));
            if (Intersects(s)) return true;

            s = new Segment2D(new Vector2(max.x, max.y), new Vector2(max.x, min.y));
            if (Intersects(s)) return true;

            s = new Segment2D(new Vector2(min.x, min.y), new Vector2(max.x, min.y));
            if (Intersects(s)) return true;

            return false;
        }

        public bool GetIntersection(Line2D l, out Vector2 point) {
            if (!Intersects(l)) {
                point = default(Vector2);
                return false;
            }
            return l.GetIntersection(GetLine(), out point);
        }

        public bool GetIntersection(Segment2D s, out Vector2 point) {
            if (!Intersects(s)) {
                point = default(Vector2);
                return false;
            }
            return s.GetLine().GetIntersection(GetLine(), out point);
        }

        /*
         * Segment2Dが表す線分上で、点Pと最も近い点を返す
         */
        public Vector2 GetClosestPoint(Vector2 p) {
            var a1 = b.y - a.y;
            var b1 = a.x - b.x;
            var c1 = a1 * a.x + b1 * a.y;
            var c2 = -b1 * p.x + a1 * p.y;
            var det = a1 * a1 - -b1 * b1;
            if (!Mathf.Approximately(det, 0f)) {
                return new Vector2(
                    (a1 * c1 - b1 * c2) / det,
                    (a1 * c2 - -b1 * c1) / det
                );
            } else {
                return p;
            }
        }

        /*
         * 点pから線分abとの距離を返す
         */
        public float GetDistance(Vector2 p) {
            var closest = GetClosestPoint(p);
            return Vector2.Distance(closest, p);
        }

        public bool Contains(Vector2 p) {
            var closest = GetClosestPoint(p);
            var d0 = b - closest;
            var d1 = b - a;
            return (Vector2.Dot(d0, d1) >= 0f) && (d0.sqrMagnitude <= d1.sqrMagnitude);
        }

        /*
         * 線分ab上の、aから距離distance離れている点を返す
         */
        public Vector2 GetPoint(float distance) {
            var dir = (b - a).normalized;
            return a + dir * distance;
        }

        public void DrawGizmos() {
            Gizmos.DrawLine(A, B);
        }

    }
}
