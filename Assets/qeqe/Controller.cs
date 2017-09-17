using UnityEngine;
using System.Collections;

namespace Qeqe {
    public class Controller : MonoBehaviour {
        public int energy;
        public Matrix.Controller matrixController;
        public bool CanDig {
            get {
                return GetComponent<Powerup.Consumer>().CanDig;
            }
        }
    }
}
