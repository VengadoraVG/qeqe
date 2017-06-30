using UnityEngine;
using System.Collections;

public class NextLevelPortal : MonoBehaviour {
    void OnTriggerEnter2D (Collider2D c) {
        Level.LevelController.instance.NextLevel();
    }
}
