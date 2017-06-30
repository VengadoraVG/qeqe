using UnityEngine;
using System.Collections;
using QeqeInput;

namespace Level {

    [ExecuteInEditMode]
    public class LevelController : MonoBehaviour {
        public TextAsset[] rawLevels;
        public LevelStatus[] levels; 
        public int CurrentLevel = 0;
        public Map mapController;

        public static LevelController instance;

        void Start () {
            Initialize();
        }

        void Update () {
            if (!Application.isPlaying) {
                Initialize();
            } else if (Verbs.Reset) {
                Reset(CurrentLevel);
            }
        }

        public void Initialize () {
            instance = this;
            levels = new LevelStatus[rawLevels.Length];
            mapController.Initialize();

            for (int i=0; i<rawLevels.Length; i++) {
                Reset(i);
            }

            Load(CurrentLevel);
        }

        public void Load (int lvlIndex) {
            mapController.SetLvlStatus(levels[lvlIndex]);
            mapController.Initialize();
        }

        public void Reset (int lvlIndex) {
            levels[lvlIndex] = new LevelStatus();
            levels[lvlIndex].Digest(rawLevels[lvlIndex].text, mapController);
            Load(lvlIndex);
        }

        public void NextLevel () {
            CurrentLevel = (CurrentLevel + 1) % levels.Length;
            Load(CurrentLevel);
        }
    }
}
