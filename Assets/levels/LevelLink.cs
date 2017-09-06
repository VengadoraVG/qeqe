using UnityEngine;
using System.Collections;

namespace Lvl {
    public class LevelLink {
        public LevelStatus status;
        public int index; // index of the level the link comes from.

        public LevelLink (LevelStatus status) {
            this.status = status;
            index = LevelController.instance.currentLevel;
        }
    }
}
