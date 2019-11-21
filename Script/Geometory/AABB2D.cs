using UnityEngine;

namespace kmty.gist {

    [System.Serializable]
    public class AABB2D {

        public Vector2 Min => min;
        public Vector2 Max => max;
        public Vector2 Center => center;
        public Vector2 Size => new Vector2(max.x - min.x, max.y - min.y);
        [SerializeField] protected Vector2 min, max, center;

        public AABB2D(Vector2 min, Vector2 max) {
            this.min = new Vector2(Mathf.Min(min.x, max.x), Mathf.Min(min.y, max.y));
            this.max = new Vector2(Mathf.Max(min.x, max.x), Mathf.Max(min.y, max.y));
            this.center = (this.min + this.max) * 0.5f;
        }

        public AABB2D(Vector3 min, Vector3 max) : this(new Vector2(min.x, min.y), new Vector2(max.x, max.y)) { }

        public bool Contains(Vector2 p, float offset = 0f) {
            return 
                min.x + offset <= p.x && p.x <= max.x - offset &&
                min.y + offset <= p.y && p.y <= max.y - offset;
        }

        public bool ContainsX(float x, float offset = 0f) => min.x + offset <= x && x <= max.x - offset;
        public bool ContainsY(float y, float offset = 0f) => min.y + offset <= y && y <= max.y - offset;

        public Vector2 SampleRandom() => new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));

        public Vector2 SampleRandom(Vector2 offset) {
            //this previously uses hoffset instead of offset var hoffset = offset * 0.5f;
            return new Vector2(
                Random.Range(min.x + offset.x, max.x - offset.x),
                Random.Range(min.y + offset.y, max.y - offset.y)
            );
        }
    }
}
