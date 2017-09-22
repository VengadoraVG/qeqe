using UnityEngine;
using System.Collections;
using Powerup;

public class Hider : MonoBehaviour {
    public GameObject matrixRenderer;
    public float hidingAlpha = 0.2f;

    void Start () {
        OnTriggerExit2D(null);
    }

    void OnTriggerExit2D (Collider2D c) {
        Util.ForEachChildren(this.transform, HideChildrenAction, this);
    }

    void OnTriggerEnter2D (Collider2D c) {
        Util.ForEachChildren(this.transform, ShowChildrenAction, this);
    }

    public void HideChildrenAction (Object caller, Transform child) {
        try {
            child.gameObject.GetComponent<Bone>().HideWithMap();
        } catch (System.NullReferenceException) {}

        try {
            SpriteRenderer sr = child.gameObject.GetComponent<SpriteRenderer>();
            sr.color = sr.color * new Color(1,1,1, 0) + new Color(0,0,0, hidingAlpha);
        } catch (MissingComponentException) {}
    }

    public void ShowChildrenAction (Object caller, Transform child) {
        try {
            SpriteRenderer sr = child.gameObject.GetComponent<SpriteRenderer>();
            sr.color = sr.color * new Color(1,1,1, 0) + new Color(0,0,0, 1);
        } catch (MissingComponentException) {}

        try {
            child.gameObject.GetComponent<Bone>().ShowWithMap();
        } catch (System.NullReferenceException) {}
    }
}
