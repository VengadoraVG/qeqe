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

        public Stack< Stack<Matrix.Status> > trivialChanges =
            new Stack< Stack<Matrix.Status> >();

        private Controller _qeqe;

        void Start () {
            _qeqe = GetComponent<Controller>();
            _qeqe.levelDetector.OnLevelSwitch += LevelSwitchHandler;
        }

        void Update () {
            if (Verbs.Undo) {
                while(trivialChanges.Peek().Count > 0) {
                    trivialChanges.Peek().Pop().Set();
                }

                history.Peek().Set();
                if (history.Count > 1) {
                    history.Pop();
                    trivialChanges.Pop();
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
            if (change == LittleChange.Type.tile) {
                PushState(qeqe);
                history.Peek().UndoChange(row, column, change);
            } else if (change == LittleChange.Type.bone) {
                trivialChanges.Peek().
                    Push(new Matrix.Status(_qeqe.levelDetector.level, qeqe));
                trivialChanges.Peek().Peek().UndoChange(row, column, change);
            }
        }

        public void PushState (Qeqe.Status qeqe) {
            history.Push(new Matrix.Status(_qeqe.levelDetector.level, qeqe));
            trivialChanges.Push(new Stack<Matrix.Status>());
        }
    }
}
