using UnityEngine;
using System.Collections;

namespace Matrix {
    [System.Serializable]
    public class Status {
        public bool[,] W; // true <=> floor
        public float[,] hp; // hp[i,j] says how much hp W[i,j] has (for digging)
        public bool[,] B; // true <=> bone

        // TODO: is this useless now?
        public Vector2 qeqe; // qeqe's position
    }
}
