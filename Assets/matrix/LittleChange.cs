using UnityEngine;
namespace Matrix {
    public class LittleChange {
        public enum Type {
            bone,
            tile,
            position
        }

        public Type type;
        public Qeqe.Status qeqe;
        public Vector2 tile; // logical position
        public Controller owner;

        public LittleChange (Type type, GameObject qeqe, Vector2 tile, Controller owner) {
            this.type = type;
            this.qeqe = new Qeqe.Status(qeqe);
            this.tile = tile;
            this.owner = owner;
        }

        public void Set () {
            this.qeqe.Set();
            owner.TriggerLittleChange((int)tile.y, (int)tile.x, type);
        }
    }
}
