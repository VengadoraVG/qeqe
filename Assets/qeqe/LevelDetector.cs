using UnityEngine;
using System.Collections;

namespace Qeqe{
    public class LevelDetector : MonoBehaviour {
        void OnTriggerEnter2D (Collider2D c) {
            Matrix.Controller matrix = Util.FindComponent<Matrix.Controller>(c.transform);
            if (matrix != null) {
                Util.FindComponent<Qeqe.Controller>(transform).matrixController = matrix;
            }
        }
    }
}
