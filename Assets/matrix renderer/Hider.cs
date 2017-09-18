using UnityEngine;
using System.Collections;

public class Hider : MonoBehaviour {
    public GameObject matrixRenderer;

    void Start () {
        matrixRenderer.SetActive(false);
    }

    void OnTriggerExit2D (Collider2D c) {
        matrixRenderer.SetActive(false);
    }

    void OnTriggerEnter2D (Collider2D c) {
        matrixRenderer.SetActive(true);
    }
}
