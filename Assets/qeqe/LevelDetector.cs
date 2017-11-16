using UnityEngine;
using System.Collections;

namespace Qeqe{
    public class LevelDetector : MonoBehaviour {
        public Matrix.Controller level;

        void OnTriggerEnter2D (Collider2D c) {
            level = Util.FindComponent<Matrix.Controller>(c.transform);
        }
    }
}
