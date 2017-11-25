using UnityEngine;
using System.Collections;

namespace MatrixRenderer {
    [ExecuteInEditMode]
    public class Snapper : MonoBehaviour {
        private MapRenderer _mapRenderer;

        void Update () {
            if (!Application.isPlaying) {
                if (_mapRenderer == null)
                    _mapRenderer =
                        transform.GetChild(0).GetComponent<MapRenderer>();

                Vector3 p = transform.position;
                Vector2 size = _mapRenderer.TileSize;
                transform.position =
                    new Vector3(Mathf.Round(p.x/size.x) * size.x,
                                Mathf.Round(p.y/size.y) * size.y,
                                p.z);
            }
        }
    }
}
