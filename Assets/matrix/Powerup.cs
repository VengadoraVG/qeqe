using UnityEngine;
using System.Collections;

namespace Matrix {
    public class Powerup : MonoBehaviour {
        public GameObject bonePrototype;

        public bool RemoveBone (int row, int col) {
            Matrix.Status status = GetComponent<Controller>().status;
            if (!status.B[row, col]) {
                return false;
            }

            status.B[row, col] = false;
            return true;
        }
    }
}
