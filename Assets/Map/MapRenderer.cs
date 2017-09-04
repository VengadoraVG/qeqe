using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Powerup;
using Lvl;

namespace Map {
    [ExecuteInEditMode]
    public class MapRenderer : MonoBehaviour {
        public bool isPreview;

        public Sprite[] floorSprites;
        public int width;
        public int height;
        public GameObject mapPosition;
        public GameObject tilePrototype;
        public GameObject bonePrototype;
        public GameObject nextPrototype;
        public GameObject qeqe;
        [HideInInspector]
        public Vector2 tileSize;

        public GameObject[,] tiles;
        public GameObject[,] bones;
        public ExternalController external;

        public Level lvl;

        void Start () {
            Initialize();
            // Cursor.visible = false;
        }

        void Update () {
            if (!Application.isPlaying) {
                Initialize();
            }
        }

        public void Initialize () {
            tileSize = tilePrototype.GetComponent<SpriteRenderer>().bounds.size;
        }

        public void SetLvlStatus (Level lvl) {
            Vector3 scale = transform.localScale;
            transform.localScale = new Vector3(1, 1, 1);;

            this.lvl = lvl;

            if (external != null) {
                external.ActivateExternalOfCurrentLevel();
            }

            width = lvl.width;
            height = lvl.height;

            if (!isPreview) {
                qeqe.transform.position =
                    GetLocalPosition((int) lvl.position.y, (int) lvl.position.x)
                    + mapPosition.transform.position;
            }

            PaintMap();
            transform.localScale = scale;
        }

        public void Repaint (int row, int column) {
            if (lvl.W[row, column] == 0) {
                Destroy(tiles[row, column]);
            } else {
                tiles[row, column].GetComponent<SpriteRenderer>().sprite = floorSprites[lvl.W[row, column]];
            }
        }

        public void ResetTo (Level level) {
            SetLvlStatus(level);
        }

        public void PaintMap () {
            ClearTileMap();

            for (int i=0; i<height; i++) {
                for (int j=0; j<width; j++) {
                    TileRender(i, j);
                    BoneRender(i, j);
                    PortalRender(i, j);
                }
            }
        }

        public void PortalRender (int row, int col) {
            if (lvl.N[row, col] == 1 && !isPreview) {
                RenderAt(row, col, nextPrototype).GetComponent<NextLevelPortal>().Initialize(row, col, width, height);
            }
        }

        public void BoneRender (int row, int col) {
            if (lvl.B[row, col] == 1) {
                bones[row, col] = RenderAt(row, col, bonePrototype);
                if (!isPreview) {
                    bones[row, col].GetComponent<Bone>().Initialize(row, col, this.lvl);
                }
            }
        }

        public void TileRender (int row, int col) {
            if (lvl.W[row,col] != 0) {
                tiles[row,col] = RenderAt(row, col, tilePrototype);
                tiles[row,col].GetComponent<SpriteRenderer>().sprite = floorSprites[lvl.W[row,col]];
                if (!isPreview) {
                    Tile tile = tiles[row,col].GetComponent<Tile>();
                    tile.SetIndexes(row,col);
                    if (lvl.I[row, col] == 1) {
                        tile.SetIndestructible();
                    }
                    tile.mapRenderer = this;
                }
            }
        }

        public GameObject RenderAt (int row, int col, GameObject prototype) {
            GameObject r = Instantiate(prototype);
            r.transform.parent = mapPosition.transform;
            r.transform.localPosition = GetLocalPosition(row, col);

            return r;
        }

        public Vector3 GetLocalPosition (int row, int column) {
            return new Vector3(column*tileSize.x, (height-1-row) * tileSize.y);
        }

        public void ClearTileMap () {
            GameObject newMapPosition = new GameObject(mapPosition.name);
            newMapPosition.transform.position = mapPosition.transform.position;
            newMapPosition.transform.localScale = mapPosition.transform.localScale;
            newMapPosition.transform.parent = mapPosition.transform.parent;

            if (Application.isPlaying) {
                Destroy(mapPosition);
            } else {
                DestroyImmediate(mapPosition);
            }
            mapPosition = newMapPosition;

            tiles = new GameObject[height, width];
            bones = new GameObject[height, width];
        }

        public void Destroy (int row, int col) {
            lvl.DestroyTile(row, col);

            Repaint(row, col);

            for (int i=-1; i<2; i++) {
                for (int j=-1; j<2; j++) {
                    try {
                        Repaint(row+i, col+j);
                    } catch (IndexOutOfRangeException) {};
                }
            }
        }

        public void Destroy (Tile tile) {
            Destroy(tile.row, tile.column);
        }
    }
}
