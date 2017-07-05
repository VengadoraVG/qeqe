using UnityEngine;
using System.Collections;

public class ActivateOnTouch : MonoBehaviour {
    public GameObject activatedThing;

    void OnTriggerEnter2D (Collider2D c) {
        activatedThing.SetActive(true);
    }

    void OnDisable () {
        activatedThing.SetActive(false);
    }
}
