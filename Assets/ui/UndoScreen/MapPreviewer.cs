using UnityEngine;
using System.Collections;
using Lvl;

namespace Map {
    public class MapPreviewer : MonoBehaviour {
        public GameObject tilePrototype;
        public Sprite[] floorSprites;
        public GameObject mapPosition;
        public float desiredSize = 5;

        [HideInInspector]
        public int width;
        [HideInInspector]
        public int height;
        [HideInInspector]
        private Vector3 _tileSize;
        public LevelStatus lvl;
        [HideInInspector]
        public GameObject[,] tiles;

        public void SetPreviewSource (LevelStatus lvl) {
            Initialize(lvl);
            PaintMap();

            float fw = desiredSize / (_tileSize.x * Mathf.Max(width, height));
            transform.localScale = fw * new Vector3(1, 1, 1);
        }

        public void Initialize (LevelStatus lvl) {
            this.lvl = lvl;
            Initialize();
        }

        public void Initialize () {
            _tileSize = tilePrototype.GetComponent<SpriteRenderer>().bounds.size;
            height = lvl.W.GetLength(0);
            width = lvl.W.GetLength(1);
            tiles = new GameObject[height, width];
        }

        public void Clear () {
            GameObject reseted = new GameObject(mapPosition.name);
            Util.CopyTransform(mapPosition.transform, reseted.transform);
            Destroy(mapPosition);
            mapPosition = reseted;
        }

        public void PaintMap () {
            Vector3 scale = transform.localScale;
            transform.localScale = new Vector3(1, 1, 1);
            Clear();

            for (int i=0; i<height; i++) {
                for (int j=0; j<width; j++) {
                    if (lvl.W[i,j] > 0) {
                        tiles[i, j] = Instantiate(tilePrototype);
                        tiles[i, j].transform.parent = mapPosition.transform;
                        tiles[i, j].transform.localPosition = GetLocalPosition(i, j);
                        tiles[i, j].GetComponent<SpriteRenderer>().sprite = floorSprites[lvl.W[i, j]];
                    }
                }
            }
            transform.localScale = scale;
        }

        public Vector3 GetLocalPosition (int row, int column) {
            return new Vector3(column * _tileSize.x, (height -1 -row) * _tileSize.y);
        }
    }
}
