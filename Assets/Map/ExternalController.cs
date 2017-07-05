using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Map {
    [ExecuteInEditMode]
    public class ExternalController : MonoBehaviour {
        public Lvl.LevelController lvlController;

        void Start () {
            Initialize();
        }

        void Update () {
            if (!Application.isPlaying) {
                Initialize();
            }
        }

        public void Initialize () {
            ActivateExternalOfCurrentLevel();
        }

        public void ActivateExternalOfCurrentLevel () {
            string nameOfCurrent = lvlController.GetNameOfCurrentLevel();

            for (int i=0; i<transform.childCount; i++) {
                transform.GetChild(i).gameObject.SetActive(nameOfCurrent == transform.GetChild(i).name);
            }            
        }
    }
}
