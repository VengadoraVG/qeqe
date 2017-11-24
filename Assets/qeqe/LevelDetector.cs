using UnityEngine;
using System.Collections;

namespace Qeqe{
    public class LevelDetector : MonoBehaviour {
        public Matrix.Controller level;
        public delegate void LevelSwitchDelegate (Matrix.Controller oldLevel,
                                                  Matrix.Controller newLevel);
        public event LevelSwitchDelegate OnLevelSwitch;

        void OnTriggerEnter2D (Collider2D c) {
            Matrix.Controller oldLevel = level;
            level = Util.FindComponent<Matrix.Controller>(c.transform);

            if (OnLevelSwitch != null) {
                OnLevelSwitch(oldLevel, level);
            }
        }
    }
}
