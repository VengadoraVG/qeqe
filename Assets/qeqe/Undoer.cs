using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Powerup;
using Matrix;
using QeqeInput;

namespace Qeqe {
    public class Undoer : MonoBehaviour {
        public Stack<Matrix.Status> history =
            new Stack<Matrix.Status>();

        private Controller _qeqe;

        void Start () {
            _qeqe = GetComponent<Controller>();
            _qeqe.levelDetector.OnLevelSwitch += LevelSwitchHandler;
        }

        void Update () {
            if (Verbs.Undo) {
                history.Peek().Set();
                if (history.Count > 1) {
                    history.Pop();
                }
            }
        }

        public void LevelSwitchHandler (Matrix.Controller oldLevel,
                                        Matrix.Controller newLevel) {
            if (history.Count == 0)
                PushState(new Qeqe.Status(_qeqe.gameObject));

            if (oldLevel != null) {
                oldLevel.OnLittleChange -= HandleLittleChange;
            }
            newLevel.OnLittleChange += HandleLittleChange;
        }

        public void HandleLittleChange (int row, int column, LittleChange.Type change,
                                        Qeqe.Status qeqe) {
            PushState(qeqe);
            history.Peek().UndoChange(row, column, change);
        }

        public void PushState (Qeqe.Status qeqe) {
            history.Push(new Matrix.Status(_qeqe.levelDetector.level, qeqe));
        }
    }
}
