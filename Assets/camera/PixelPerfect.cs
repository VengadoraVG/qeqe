using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PixelPerfect : MonoBehaviour {
    public const float FACTOR = 1080 / 17f;
    private Camera _camera;

    void Update () {
        _camera = GetComponent<Camera>();
        _camera.orthographicSize = Mathf.Min(Screen.height, Screen.width) / FACTOR;
    }
}
