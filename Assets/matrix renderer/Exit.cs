using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using QeqeInput;

namespace MatrixRenderer {
    public class Exit : MonoBehaviour {
        public string linkedScene;
        public bool active = false;

        void OnTriggerEnter2D (Collider2D c) {
            active = true;
        }

        void OnTriggerExit2D (Collider2D c) {
            active = false;
        }

        void Update () {
            if (active && Verbs.Exit) {
                SceneManager.LoadScene(linkedScene);
            }
        }
    }
}
