using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour {
    public Color danger;
    public Color selected;
    public Color normal;

    public Color GetColor (PreviewMark mark) {
        switch (mark) {
            case PreviewMark.toSelect:
                return selected;
            case PreviewMark.toDelete:
                return danger;
            default:
                return normal;
        }
    }
}
