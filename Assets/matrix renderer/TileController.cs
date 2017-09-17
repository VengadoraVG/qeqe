using UnityEngine;
using System.Collections;

namespace MatrixRenderer {
    public class TileController : MonoBehaviour{
        public Tile tilePrototype;

        public Sprite border;
        public Sprite solid;

        public Vector2 size {
            get {
                SpriteRenderer r = tilePrototype.GetComponent<SpriteRenderer>();
                _size = r.bounds.size * 0.95f;

                return _size;
            }
        }

        private Vector2 _size = new Vector2(10, 10);
    }
}
