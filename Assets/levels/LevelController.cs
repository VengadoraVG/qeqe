using UnityEngine;
using System.Collections;
using QeqeInput;

namespace Lvl {

    [ExecuteInEditMode]
    public class LevelController : MonoBehaviour {
        public TextAsset[] rawLevels;
        public Level[] levels;
        public int currentLevel = 0;
        public Map.MapRenderer mapRenderer;
        public ChainOfLevels chain;
        public int boneCount;

        public UndoScreen uundoScreen;

        public bool changed = false;

        public static LevelController instance;

        private LevelStatus initialState;

        void Start () {
            chain = new ChainOfLevels();
            Initialize();
            EnterLevel(currentLevel);
        }

        void Update () {
            if (!Application.isPlaying) {
                Initialize();
            }

            if (Verbs.Reset) {
                ResetLevel();
            }

            if (Verbs.Undo) {
                UndoLevel();
            }
        }

        void OnDestroy () {
            if (instance == this) {
                instance = null;
            }
        }

        public void Initialize () {
            instance = this;
            levels = new Level[rawLevels.Length];

            for (int i=0; i<rawLevels.Length; i++) {
                Initialize(i);
            }

            Load(currentLevel);
        }

        public void Initialize (int lvlIndex) {
            levels[lvlIndex] = new Level(rawLevels[lvlIndex], lvlIndex);
        }

        public void Load (int lvlIndex) {
            initialState = new LevelLink().initial;
            mapRenderer.SetLvlStatus(levels[lvlIndex]);
            mapRenderer.Initialize();
            CountBones();
        }

        public void CountBones () {
            boneCount = 0;
            for (int i=0; i<levels[currentLevel].height; i++) {
                for (int j=0; j<levels[currentLevel].width; j++) {
                    if (levels[currentLevel].B[i,j] == 1) {
                        boneCount++;
                    }
                }
            }
        }

        public void ExitLevel () {
            
        }

        public void EnterLevel (int lvlIndex) {
            initialState = new LevelLink().initial;
        }

        public void NextLevel () {
            currentLevel = (currentLevel + 1) % levels.Length;
            Load(currentLevel);
            EnterLevel(currentLevel);
        }

        public static LevelStatus GetCurrentLevelStatus () {
            return new LevelStatus(instance.levels[instance.currentLevel]);
        }

        public void ResetLevel (LevelStatus status, int index) {
            levels[index].Load(status);
            mapRenderer.ResetTo(levels[currentLevel]);
            Qeqe.Controller.Energy = status.energy;
            Load(index);
        }

        public void ResetLevel () {
            changed = false;
            ResetLevel(initialState, currentLevel);
        }

        public void UndoLevel () {
            // if (chain.CanUndo()) {
            //     ResetLevel();
            //     LevelStatus status = chain.Pop().initial;
            //     ResetLevel(status, status.lvlIndex);
            // } else {
            //     Debug.Log("Can't undo :(" + Time.time);
            // }
        }

        public string GetNameOfCurrentLevel () {
            return rawLevels[currentLevel].name;
        }

        public void RegisterChange () {
            changed = true;
        }
    }
}
