using UnityEngine;
using System.Collections;
using Lvl;

namespace Map {
    public class Previewer : MonoBehaviour {
        public MapRenderer mapRenderer;
        public Level previewSource;

        void Start () {
            if (mapRenderer == null) {
                mapRenderer = GetComponent<MapRenderer>();
            }
        }

        public void SetPreviewSource (Level lvl) {
            mapRenderer.SetLvlStatus(lvl);
            previewSource = lvl;
        }
    }
}
