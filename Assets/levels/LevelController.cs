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
            ExitLevel();
            currentLevel = lvlIndex;
            mapRenderer.SetLvlStatus(levels[lvlIndex]);
            mapRenderer.Initialize();
            CountBones();
            EnterLevel(lvlIndex);
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
            if (changed) {
                chain.Add(initialState);
            }
            changed = false;
        }

        public void EnterLevel (int lvlIndex) {
            initialState = new LevelStatus(levels[currentLevel]);
        }

        public void NextLevel () {
            Load((currentLevel + 1) % levels.Length);
        }

        public static LevelStatus GetCurrentLevelStatus () {
            return new LevelStatus(instance.levels[instance.currentLevel]);
        }

        public void ResetLevel (LevelStatus status, int index) {
            changed = false;
            levels[index].Load(status);
            mapRenderer.ResetTo(levels[index]);
            Qeqe.Controller.Energy = status.energy;
            Load(index);
        }

        public void ResetLevel () {
            ResetLevel(initialState, currentLevel);
        }

        public void UndoLevel () {
            if (chain.CanUndo()) {
                ResetLevel();
                LevelLink popped = chain.Pop();
                levels[popped.status.lvlIndex].Load(popped.status);
                ResetLevel(popped.status, popped.status.lvlIndex);
            }
        }

        public string GetNameOfCurrentLevel () {
            return rawLevels[currentLevel].name;
        }

        public void RegisterChange () {
            changed = true;
        }
    }
}
