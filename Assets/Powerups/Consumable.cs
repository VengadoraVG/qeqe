using UnityEngine;
using System.Collections;

using Matrix;

namespace Powerup {
    public abstract class Consumable : MonoBehaviour {
        public int row;
        public int column;
        public Matrix.Controller owner;
        public Consumer lastConsumer;

        private bool _isConsumed = false;

        void OnTriggerEnter2D (Collider2D c) {
            if (!_isConsumed) {
                _isConsumed = true;
                lastConsumer = Util.FindComponent<Consumer>(c.transform);

                GetComponent<Animator>().SetTrigger("Explode");
                GetComponent<Collider2D>().enabled = false;
                GetConsumed();
            }
        }

        public void Initialize (int row, int column, Matrix.Controller owner) {
            this.row = row;
            this.column = column;
            this.owner = owner;
        }

        public abstract void GetConsumed ();
    }
}
