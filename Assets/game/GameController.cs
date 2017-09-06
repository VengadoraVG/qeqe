using UnityEngine;
using System.Collections;
using Lvl;

public class GameController : MonoBehaviour {
    public GameObject undoScreen;
    private LevelController _levelController;

    void Start () {
        _levelController = GetComponent<LevelController>();
    }

    public void ToggleMenu () {
        undoScreen.SetActive(!undoScreen.activeSelf);
        if (undoScreen.activeSelf)
            undoScreen.GetComponent<UndoScreen>().Initialize(_levelController.chain);
    }

    public void CloseGame () {
        Application.Quit();
    }
}
