using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class WinOnBonesClear : MonoBehaviour {
    public string victorySceneName = "credits";
    public Lvl.LevelController lvlController;
    
    void Update () {
        if (lvlController.boneCount <= 0) {
            SceneManager.LoadScene(victorySceneName);
        }
    }
}
