using UnityEngine;
using System.Collections;
using Lvl;

namespace Powerup {
    public abstract class Consumable : MonoBehaviour {
        public int row;
        public int column;
        public Level level;

        private float consumedTime = -1;

        void OnTriggerEnter2D (Collider2D c) {
            if (consumedTime != Time.time) {
                GetComponent<Animator>().SetTrigger("Explode");
                GetComponent<Collider2D>().enabled = false;
                GetConsumed();
                consumedTime = Time.time;

                GameObject.FindWithTag("GameController").
                    GetComponent<LevelController>().RegisterChange();
            }
        }

        public void Initialize (int row, int column, Level level) {
            this.row = row;
            this.column = column;
            this.level = level;
        }

        public abstract void GetConsumed ();
    }
}
