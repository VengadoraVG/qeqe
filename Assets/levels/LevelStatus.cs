using UnityEngine;
using System.Collections;

namespace Lvl {
    public class LevelStatus {
        public int lvlIndex;
        public int[,] W;
        public int[,] B;
        public int energy;

        public LevelStatus (Level lvl) {
            this.W = Util.Clone(lvl.W);
            this.B = Util.Clone(lvl.B);
            energy = Qeqe.Controller.Energy;
        }
    }
}
