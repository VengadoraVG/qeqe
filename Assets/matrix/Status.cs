using UnityEngine;
using System.Collections;

namespace Matrix {
    [System.Serializable]
    public class Status {
        public bool[,] W; // true <=> floor
        public float[,] hp; // hp[i,j] says how much hp W[i,j] has (for digging)
        public bool[,] B; // true <=> bone
        public Qeqe.Status qeqe;
        public Matrix.Controller owner;

        public Status () {}

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
            W = Util.Clone(original.W);
            hp = Util.Clone(original.hp);
            B = Util.Clone(original.B);
            qeqe = original.qeqe;
        }

        public void UndoChange (int row, int column, LittleChange.Type change) {
            if (change == LittleChange.Type.tile) {
                W[row, column] = true;
                hp[row, column] = 1;
            }
        }
    }
}
