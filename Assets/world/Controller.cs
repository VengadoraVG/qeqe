using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace World {
    public delegate void TileDigged (int row, int column, Matrix.Controller matrix, Qeqe.Digger digger);
    public delegate void BoneEaten (int row, int column, Matrix.Controller matrix, Powerup.Consumer eater);

    public class Controller : MonoBehaviour {
        public List<Matrix.Controller> matrix;

        public event TileDigged OnTileDigged;
        public event BoneEaten OnBoneEaten;

        public void Subscribe (Matrix.Controller matrix) {
            matrix.OnTileDigged += TriggerTileDigged;
            matrix.OnBoneEaten += TriggerBoneEaten;
        }

        public void TriggerTileDigged (int i, int j, Matrix.Controller matrix, Qeqe.Digger digger) {
            if (OnTileDigged != null) {
                OnTileDigged(i, j, matrix, digger);
            }
        }

        public void TriggerBoneEaten (int i, int j, Matrix.Controller matrix, Powerup.Consumer eater) {
            if (OnBoneEaten != null) {
                OnBoneEaten(i, j, matrix, eater);
            }
        }
   }
}
