using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Matrix {
    public class Digger : MonoBehaviour {
        public Status status {
            get {
                if (_status == null) {
                    _status = GetComponent<Controller>().status;
                }
                return _status;
            }
        }

        private Dictionary<Vector2, DigAction> _diggingCoroutine =
            new Dictionary<Vector2, DigAction>();
        private Status _status;

        public Qeqe.Digger GetDigger (int row, int col) {
            try {
                return _diggingCoroutine[new Vector2(row, col)].digger;
            } catch (KeyNotFoundException) {
                return null;
            }
        }

        public bool StartGettingDigged (int i, int j, Qeqe.Digger digger) {
            Vector2 pos = new Vector2(i, j);
            if (!_diggingCoroutine.ContainsKey(pos)) {
                _diggingCoroutine[pos] = new DigAction(digger, StartCoroutine(_GetDigged(i, j, digger)));
                return true;
            }

            return false;
        }

        public bool StopGettingDigged (int i, int j, Qeqe.Digger digger) {
            Vector2 pos = new Vector2(i, j);
            if (_diggingCoroutine.ContainsKey(pos) && _diggingCoroutine[pos].digger == digger) {
                StopCoroutine(_diggingCoroutine[pos].coroutine);
                _diggingCoroutine.Remove(pos);
                return true;
            }

            return false;
        }

        public void Undig (int i, int j) {
            status.W[i, j] = true;
            status.hp[i, j] = 1;
        }

        private IEnumerator _GetDigged (int i, int j, Qeqe.Digger digger) {
            float elapsedTime = 0;

            do {
                yield return null;
                elapsedTime += Time.deltaTime;
            } while (elapsedTime < status.hp[i, j]);

            status.W[i, j] = false;
            status.hp[i, j] = 0;
            GetComponent<Matrix.Controller>().TriggerTileDigged(i, j, digger);
        }
    }
}
