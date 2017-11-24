using UnityEngine;
using System.Collections;

namespace Qeqe {
    public class Controller : MonoBehaviour {
        public int Energy {
            get { return GetComponent<Powerup.Consumer>().energy; }
            set { GetComponent<Powerup.Consumer>().SetEnergy(value); }
        }
        public Matrix.Controller matrixController {
            get {
                return levelDetector.level;
            }
        }
        public LevelDetector levelDetector;
        public bool CanDig {
            get {
                return GetComponent<Powerup.Consumer>().CanDig;
            }
        }
    }
}
