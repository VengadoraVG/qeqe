using UnityEngine;
using System.Collections;
using System;

public class Raycaster : MonoBehaviour {
    public GameObject end;
    public string layer = "default";

    void Start () {
        if (end == null) {
            end = transform.GetChild(0).gameObject;
        }
    }

    public GameObject GetImpacted () {
        Vector2 d = end.transform.position - transform.position;
        try {
            return Physics2D.Raycast(transform.position, d, d.magnitude, 1 << (LayerMask.NameToLayer(layer) + 1))
                .collider.gameObject;
        } catch (NullReferenceException) {
            return null;
        }
    }
}
