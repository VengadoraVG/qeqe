using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Powerup;

namespace Matrix {
    public class Undoer : MonoBehaviour {
        public List<LittleChange> t;
        public Stack<LittleChange> history = new Stack<LittleChange>();
        public World.Controller world;

        private GameObject _owner;

        void Start () {
            world.OnTileDigged += TileChange;
            _owner = this.gameObject;
        }

        void Update () {
            if (QeqeInput.Verbs.Undo) {
                Undo();
            }
        }

        public void Undo () {
            if (history.Count > 0) {
                history.Pop().Set();
                t.RemoveAt(t.Count - 1);
            }
        }

        public void TileChange (int i, int j, Matrix.Controller matrix, Qeqe.Digger digger) {
            if (digger.gameObject == _owner) {
                history.Push(new LittleChange(LittleChange.Type.tile, digger.gameObject,
                                              new Vector2(i, j), matrix));
                t.Add(history.Peek());
            }
        }
    }
}
