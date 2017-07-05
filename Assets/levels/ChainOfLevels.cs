using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Lvl {
    public class ChainOfLevels {
        public Stack<LevelLink> chain = new Stack<LevelLink>();

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
