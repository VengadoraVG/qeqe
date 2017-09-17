using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Powerup {
    public class Consumer : MonoBehaviour {
        public int multiplier = 3;
        public int energy = 0;
        public Text energyIndicator;

        public bool CanDig {
            get {
                return energy > 0;
            }
        }

        void Start () {
            World.Controller world = GameObject.FindWithTag("world controller").
                GetComponent<World.Controller>();
            world.OnBoneEaten += ConsumeBone;
            world.OnTileDigged += ConsumeEnergy;
        }

        void Update () {
            energyIndicator.text = energy + "";
        }

        public void ConsumeBone (int row, int column, Matrix.Controller matrix, Powerup.Consumer eater) {
            Debug.Log("me: " + this + ", he: " + eater);
            if (eater == this) {
                energy += multiplier;
            }
        }

        public void ConsumeEnergy (int row, int column, Matrix.Controller matrix, Qeqe.Digger consumer) {
            if (consumer == GetComponent<Qeqe.Digger>()) {
                energy--;
            }
        }

        public void SetEnergy (int energy) {
            this.energy = energy;
        }
    }
}
