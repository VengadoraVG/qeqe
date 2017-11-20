using UnityEngine;
using System.Collections;

namespace Matrix {
    [System.Serializable]
    public class Status {
        public bool[,] W; // true <=> floor
        public float[,] hp; // hp[i,j] says how much hp W[i,j] has (for digging)
        public bool[,] B; // true <=> bone
        public Qeqe.Status qeqe;

        public Status () {}

        public Status (Matrix.Controller father, GameObject qeqe) {
            W = Util.Clone(father.W);
            hp = Util.Clone(father.hp);
            B = Util.Clone(father.B);
            this.qeqe = new Qeqe.Status(qeqe);
        }

        public void Get (Status original) {
            W = Util.Clone(original.W);
            hp = Util.Clone(original.hp);
            B = Util.Clone(original.B);
            qeqe = original.qeqe;
        }
    }
}
