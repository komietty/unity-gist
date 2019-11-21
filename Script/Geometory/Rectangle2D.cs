using UnityEngine;

namespace kmty.gist {

    public class Rectangle2D : AABB2D {

        public Segment2D Left => l;
        public Segment2D Top => t;
        public Segment2D Right => r;
        public Segment2D Bottom => b;
        protected Segment2D l, t, r, b;

        public Rectangle2D(Vector2 min, Vector2 max) : base(min, max) {
            l = new Segment2D(new Vector2(this.min.x, this.max.y), new Vector2(this.min.x, this.min.y));
            t = new Segment2D(new Vector2(this.min.x, this.max.y), new Vector2(this.max.x, this.max.y));
            r = new Segment2D(new Vector2(this.max.x, this.max.y), new Vector2(this.max.x, this.min.y));
            b = new Segment2D(new Vector2(this.min.x, this.min.y), new Vector2(this.max.x, this.min.y));
        }

        public void DrawGizmos() {
            l.DrawGizmos();
            t.DrawGizmos();
            r.DrawGizmos();
            b.DrawGizmos();
        }

    }
}
