using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Matrix;
using Powerup;

namespace MatrixRenderer {
    public class MapRenderer : MonoBehaviour {
        public Tile[,] tiles;
        public Bone[,] bones;
        public GameObject fatherOfTiles;

        public Vector2 TileSize {
            get { return _TileController.size; }
        }
        public Vector2 Size {
            get {
                return new Vector2(_controller.status.W.GetLength(1) * _TileController.size.x,
                                   _controller.status.W.GetLength(0) * _TileController.size.y);
            }
        }

        public Matrix.Controller Controller {
            get {
                if (_controller == null) {
                    _controller = Util.FindTag(transform, "matrix controller")
                        .GetComponent<Matrix.Controller>();
                }

                return _controller;
            }
        }
        private TileController _TileController {
            get { return Controller.GetComponent<TileController>(); }
        }
        private Matrix.Powerup _PowerupController {
            get { return Controller.GetComponent<Matrix.Powerup>(); }
        }
        private Matrix.Controller _controller;

        public void Render () {
            tiles = new Tile[Controller.status.W.GetLength(0), Controller.status.W.GetLength(1)];
            bones = new Bone[Controller.status.B.GetLength(0), Controller.status.B.GetLength(1)];

            GameObject f = new GameObject(fatherOfTiles.name);
            Util.CopyTransform(fatherOfTiles.transform, f.transform);
            DestroyImmediate(fatherOfTiles);
            fatherOfTiles = f;

            for (int i=0; i<Controller.status.W.GetLength(0); i++) {
                for (int j=0; j<Controller.status.W.GetLength(1); j++) {
                    ConditionallySetTile(i, j);
                    ConditionallySetBone(i, j);
                }
            }
        }

        public int GetNTilesMask (int row, int column) {
            int mask = 0;

            for (int i=-1; i<=1; i++) {
                for (int j=-1; j<=1; j++) {
                    try {
                        if (Controller.status.W[row + i, column + j]) {
                            mask += NMask.mapOfPows[i+1, j+1];
                        }
                    } catch (System.IndexOutOfRangeException) {};
                }
            }

            return mask;
        }

        public void Dig (int row, int col, Matrix.Controller matrix, Qeqe.Digger digger) {
            if (tiles[row, col] != null) {
                Destroy(tiles[row, col].gameObject);
                tiles[row, col] = null;
            }

            MakeCoherent(row, col);
        }

        public void MakeCoherent (int row, int col) {
            for (int i=-1; i<2; i++){
                for (int j=-1; j<2; j++){
                    try {
                        if (Controller.status.W[row + i, col + j]) {
                            tiles[row + i, col + j].MakeCoherent(GetNTilesMask(row + i, col + j));
                        }
                    } catch (System.IndexOutOfRangeException) {};
                }
            }
        }

        public void Initialize (Matrix.Controller controller) {
            Controller.OnTileDigged += Dig;
            GetComponent<Hider>().RefreshSize();
            GetComponent<Hider>().Enable();
            Render();
        }

        public void GeneratePolygonalHider (List<Vector2> logicalPoints) {
            PolygonalHider hider = transform.Find("polygonal hider").gameObject
                .GetComponent<PolygonalHider>();
            if (Application.isPlaying)
                hider.gameObject.SetActive(true);
            GetComponent<Hider>().Disable();
            hider.GeneratePolygon(logicalPoints);
        }

        public Vector3 LogicalToReal (int row, int column) {
            return new Vector3(column * _TileController.size.x, -row * _TileController.size.y, 0);
        }

        public void SetThingAt (int row, int col, GameObject thing) {
            Util.CopyTransform(this.transform, thing.transform);
            thing.transform.parent = fatherOfTiles.transform;
            thing.transform.Translate(LogicalToReal(row, col));
        }

        public void ConditionallySetTile (int row, int column) {
            if (Controller.status.W[row, column]) {
                tiles[row, column] = Instantiate(_TileController.tilePrototype)
                    .GetComponent<Tile>();
                tiles[row, column].Initialize(row, column, GetNTilesMask(row, column),
                                              Controller.status.hp[row, column]);
                SetThingAt(row, column, tiles[row, column].gameObject);
            }
        }

        public void ConditionallySetBone (int row, int col) {
            if (Controller.status.B[row, col]) {
                bones[row, col] = Instantiate(_PowerupController.bonePrototype)
                    .GetComponent<Bone>();
                bones[row, col].Initialize(row, col, Controller);

                SetThingAt(row, col, bones[row, col].gameObject);
            }
        }

    }
}
