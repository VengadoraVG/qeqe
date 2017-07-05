using UnityEngine;
using System;
using System.Collections;

namespace Qeqe {
    public class Controller : MonoBehaviour {
        public static Controller instance;
        public static int Energy {
            get {
                try {
                    return instance.GetComponent<Consumer>().energy;
                } catch (NullReferenceException) {
                    return 0; // TODO: modify with perma-persistence
                }
            }
            set {
                try {
                    instance.GetComponent<Consumer>().SetEnergy(value);
                } catch (NullReferenceException) {}
            }
        }

        void Start () {
            instance = this;
        }
    }
}
