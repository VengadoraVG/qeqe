using UnityEngine;
using System.Collections;
using Powerup;
using MatrixRenderer;

namespace Matrix {
    public delegate void TileDigged (int row, int column, Matrix.Controller matrix, Qeqe.Digger digger);
    public delegate void BoneEaten (int row, int column, Matrix.Controller matrix, Consumer consumer);
    public delegate void LittleChangeDelegate (int row, int column, LittleChange.Type change, Qeqe.Status cachedQeqe);

    [ExecuteInEditMode]
    public class Controller : MonoBehaviour{
        public TextAsset testLvl;
        [HideInInspector]
        public Status status;
        public bool update = false;
        public MapRenderer _renderer;

        public int Width { get { return status.W.GetLength(0); } }
        public int Height { get { return status.W.GetLength(1); } }

        public int debugWidth;
        public int debugHeight;

        public event TileDigged OnTileDigged;
        public event BoneEaten OnBoneEaten;
        public event LittleChangeDelegate OnLittleChange;

        private Qeqe.Status _tileDiggedCachedQeqe;

        void Awake () {
            GameObject.FindWithTag("world controller").
                GetComponent<World.Controller>().Subscribe(this);
        }

        void Start () {
            status = Parser.Digest(testLvl);
            status.owner = this;
            _renderer.Initialize(this);
        }

        void Update () {
            if (update) {
                Start();
            }
        }

        public void TriggerTileDigged (int i, int j, Qeqe.Digger digger) {
            if (OnTileDigged != null)
                OnTileDigged(i, j, this, digger);
            if (OnLittleChange != null)
                OnLittleChange(i, j, LittleChange.Type.tile, _tileDiggedCachedQeqe);
        }

        public bool StartGettingDigged (int i, int j, Qeqe.Digger digger) {
            _tileDiggedCachedQeqe = new Qeqe.Status(digger.gameObject);
            return GetComponent<Matrix.Digger>().StartGettingDigged(i, j, digger);
        }

        public bool StopGettingDigged (int i, int j, Qeqe.Digger digger) {
            _tileDiggedCachedQeqe = null;
            return GetComponent<Matrix.Digger>().StopGettingDigged(i, j, digger);
        }

        public void TriggerBoneEaten (int row, int col, Consumer eater) {
            GetComponent<Matrix.Powerup>().RemoveBone(row, col);
            if (OnBoneEaten != null) {
                OnBoneEaten(row, col, this, eater);
            }
        }

        public bool CanDig (Qeqe.Digger digger, Tile tile) {
            Qeqe.Digger tileDigger = GetComponent<Matrix.Digger>().GetDigger(tile.row, tile.column);

            if (tileDigger == null || tileDigger == digger)
                return true;
            return false;
        }

        public void RequestRenderUpdate () {
            _renderer.Render();
        }
    }
}
