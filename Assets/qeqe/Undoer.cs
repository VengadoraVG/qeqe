using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Powerup;

namespace Matrix {
    public class Undoer : MonoBehaviour {
        public Stack<LittleChange> history = new Stack<LittleChange>();
        public World.Controller world;

        private GameObject _owner;

        void Start () {
            world.OnBoneEaten += BoneChange;
            world.OnTileDigged += TileChange;
            _owner = this.gameObject;
        }

        void Update () {
            if (QeqeInput.Verbs.Undo) {
                Debug.Log(Time.time);
                Undo();
            }
        }

        public void Undo () {
            history.Pop().Set();
        }

        public void BoneChange (int i, int j, Matrix.Controller matrix, Consumer eater) {
            if (eater.gameObject == _owner) {
                history.Push(new LittleChange(LittleChange.Type.bone, eater.gameObject,
                                              new Vector2(i, j), matrix));
            }
        }

        public void TileChange (int i, int j, Matrix.Controller matrix, Qeqe.Digger digger) {
            if (digger.gameObject == _owner) {
                history.Push(new LittleChange(LittleChange.Type.tile, digger.gameObject,
                                              new Vector2(i, j), matrix));
            }
        }
    }
}
