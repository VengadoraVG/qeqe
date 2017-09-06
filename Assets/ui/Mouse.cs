using UnityEngine;
using System.Collections;

public class Mouse : MonoBehaviour {
    void Update () {
        transform.position = Vector3.Scale(new Vector3(1, 1, 0), Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}
