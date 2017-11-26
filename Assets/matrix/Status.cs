using UnityEngine;
using System.Collections;

namespace Matrix {
    [System.Serializable]
    public class Status {
        public bool[,] V; // true <=> cell is perfect void (for nonsquared matrices)
        public bool[,] W; // true <=> floor
        public float[,] hp; // hp[i,j] says how much hp W[i,j] has (for digging)
        public bool[,] B; // true <=> bone
        public Qeqe.Status qeqe;
        public Matrix.Controller owner;

        public Status (int height, int width) {
            W = new bool[height, width];
            hp = new float[height, width];
            B = new bool[height, width];
            V = new bool[height, width];
        }

        public Status (Matrix.Controller owner, Qeqe.Status qeqe = null) {
            Get(owner.status);
            this.owner = owner;

            if (qeqe != null) {
                this.qeqe = qeqe;
            }
        }

        public void Set () {
            owner.status.Get(this);
            qeqe.Set();
            owner.RequestRenderUpdate();
        }

        public void Get (Status original) {
            owner = original.owner;
            V = Util.Clone(original.V);
            W = Util.Clone(original.W);
            hp = Util.Clone(original.hp);
            B = Util.Clone(original.B);
            qeqe = original.qeqe;
        }

        public void UndoChange (int row, int column, LittleChange.Type change) {
            if (change == LittleChange.Type.tile) {
                W[row, column] = true;
                hp[row, column] = 1;
            } else if (change == LittleChange.Type.bone) {
                B[row, column] = true;
            }
        }
    }
}
