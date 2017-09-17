using UnityEngine;
using System.Collections;
using Powerup;
using MatrixRenderer;

namespace Matrix {
    public delegate void TileDigged (int row, int column, Matrix.Controller matrix, Qeqe.Digger digger);
    public delegate void BoneEaten (int row, int column, Matrix.Controller matrix, Consumer consumer);

    [ExecuteInEditMode]
    public class Controller : MonoBehaviour{
        public TextAsset testLvl;
        public Status status;
        public bool update = false;
        public MapRenderer _renderer;

        public event TileDigged OnTileDigged;
        public event BoneEaten OnBoneEaten;

        void Awake () {
            GameObject.FindWithTag("world controller").
                GetComponent<World.Controller>().Subscribe(this);
        }

        void Start () {
            status = Parser.Digest(testLvl);
            _renderer.Initialize(this);
        }

        void Update () {
            if (update) {
                Start();
            }
        }

        public void TriggerTileDigged (int i, int j, Qeqe.Digger digger) {
            if (OnTileDigged != null) {
                OnTileDigged(i, j, this, digger);
            }
        }

        public bool StartGettingDigged (int i, int j, Qeqe.Digger digger) {
            return GetComponent<Matrix.Digger>().StartGettingDigged(i, j, digger);
        }

        public bool StopGettingDigged (int i, int j, Qeqe.Digger digger) {
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
    }
}
