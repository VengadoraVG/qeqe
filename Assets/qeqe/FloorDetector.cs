using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Qeqe {
    public class FloorDetector : MonoBehaviour {
        public List<Raycaster> raycasters;
        public Raycaster dig;
        public Raycaster frontalDig;

        public bool IsInFloor () {
            for (int i=0; i<raycasters.Count; i++) {
                if (raycasters[i].GetImpacted() != null) {
                    return true;
                }
            }

            return false;
        }

        public GameObject GetDiggedFloor () {
            return dig.GetImpacted();
        }

    }
}
