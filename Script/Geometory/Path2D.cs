using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace kmty.gist {

    public class Path2D {

        List<Vector2> points;
        List<Segment2D> segments;

        public Path2D(List<Vector2> corners) {
            // just clone
            points = corners.ToList();
            Build(points);
        }

        public Path2D(Vector2[] corners) {
            points = corners.ToList();
            Build(points);
        }

        public bool Intersects(Circle2D circle, float lineThickness, out Segment2D segment) {
            segment = default(Segment2D);
            for (int i = 0, n = segments.Count; i < n; i++) {
                if (segments[i].Intersects(circle, lineThickness)) {
                    segment = segments[i];
                    return true;
                }
            }
            return false;
        }

        void Build(List<Vector2> points) {
            segments = new List<Segment2D>();
            for (int i = 0, n = points.Count - 1; i < n; i++) {
                var p0 = points[i];
                var p1 = points[i + 1];
                var s = new Segment2D(p0, p1);
                segments.Add(s);
            }
        }

        public void DrawGizmos() {
            Gizmos.color = Color.green;
            if (segments != null) { segments.ForEach(s => s.DrawGizmos()); }
        }

    }
}
