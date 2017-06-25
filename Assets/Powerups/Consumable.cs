using UnityEngine;
using System.Collections;

namespace Powerup {
    public abstract class Consumable : MonoBehaviour {
        public int row;
        public int column;

        void OnTriggerEnter2D (Collider2D c) {
            GetComponent<Animator>().SetTrigger("Explode");
            GetComponent<Collider2D>().enabled = false;
            GetConsumed();
        }

        public void Initialize (int row, int column) {
            this.row = row;
            this.column = column;
        }

        public abstract void GetConsumed ();
    }
}
