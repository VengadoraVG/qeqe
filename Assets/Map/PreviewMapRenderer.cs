using UnityEngine;
using System.Collections;
using Lvl;

namespace Map {
    [ExecuteInEditMode]
    public class PreviewMapRenderer : MonoBehaviour {
        public MapRenderer mapRenderer;
        public Level previewSource;
        public Sprite tileSample;
        public float desiredSize = 4;

        private Vector2 _tileSize;

        void Start () {
            if (mapRenderer == null) {
                mapRenderer = GetComponent<MapRenderer>();
            }

            _tileSize = (Vector2) tileSample.bounds.size;
        }

        public void SetPreviewSource (Level lvl) {
            mapRenderer.SetLvlStatus(lvl);
            previewSource = lvl;
            float fw = desiredSize / (_tileSize.x * Mathf.Max(lvl.width, lvl.height));
            transform.localScale = fw * new Vector3(1, 1, 1);;
        }
    }
}
