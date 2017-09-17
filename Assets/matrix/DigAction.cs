using UnityEngine;

namespace Matrix {
    public class DigAction {
        public Qeqe.Digger digger;
        public Coroutine coroutine;

        public DigAction (Qeqe.Digger digger, Coroutine coroutine) {
            this.digger = digger;
            this.coroutine = coroutine;
        }
    }
}
