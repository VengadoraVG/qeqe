using UnityEngine;
using System.Collections;
using Lvl;

namespace Map {
    public class MapPreviewer : MonoBehaviour {
        public GameObject tilePrototype;
        public int width;
        public int height;
        public Vector3 tileSize;
        public Sprite[] floorSprites;
        public LevelStatus lvl;
        public GameObject mapPosition;
        public GameObject[,] tiles;

        void Start () {
            tileSize = tilePrototype.GetComponent<SpriteRenderer>().bounds.size;
            Clear();
        }

        public void SetLvlStatus (LevelStatus lvl) {
            Initialize(lvl);
            PaintMap();
        }

        public void Initialize (LevelStatus lvl) {
            this.lvl = lvl;
            Initialize();
        }

        public void Initialize () {
            height = lvl.W.GetLength(0);
            width = lvl.W.GetLength(1);
        }

        public void Clear () {
            GameObject reseted = new GameObject(mapPosition.name);
            Util.CopyTransform(mapPosition.transform, reseted.transform);
            Destroy(mapPosition);
            mapPosition = reseted;
        }

        public void PaintMap () {
            Clear();

            for (int i=0; i<height; i++) {
                for (int j=0; j<width; j++) {
                    tiles[i, j] = Instantiate(tilePrototype);
                    tiles[i, j].transform.parent = mapPosition.transform;
                    tiles[i, j].transform.localPosition = GetLocalPosition(i, j);
                    tiles[i, j].GetComponent<SpriteRenderer>().sprite = floorSprites[lvl.W[i, j]];
                }
            }
        }

        public Vector3 GetLocalPosition (int row, int column) {
            return new Vector3(column * tileSize.x, (height -1 -row) * tileSize.y);
        }
    }
}
