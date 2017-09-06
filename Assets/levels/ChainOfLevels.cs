using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Lvl {
    public class ChainOfLevels {
        public Stack<LevelStatus> chain = new Stack<LevelStatus>();

        public void AddBeginning () {
            chain.Push(new LevelLink());
        }

        public void SetFinal (Level lvl) {
            chain.Peek().SetFinal(lvl);
        }

        public LevelLink Last () {
            return chain.Peek();
        }

        public LevelLink Pop () {
            return chain.Pop();
        }

        public bool CanUndo () {
            return chain.Count > 1;
        }
    }
}
