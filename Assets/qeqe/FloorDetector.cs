using UnityEngine;
using System.Collections;

namespace Qeqe {
    public class FloorDetector : MonoBehaviour {
        public bool isInFloor = false;

        void OnCollisionStay2D (Collision2D c) {
            isInFloor = true;
        }

        void OnCollisionExit2D (Collision2D c) {
            isInFloor = false;
        }
    }
}
