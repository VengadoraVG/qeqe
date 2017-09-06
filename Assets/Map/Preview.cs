using UnityEngine;
using System.Collections;
using Lvl;

public class Preview : MonoBehaviour {
    public PreviewController controller;
    public PreviewMark mark;
    public bool hasMouseFocus;

    private SpriteRenderer _outerBorder;
    private GameObject _deletionMark;
    private GameObject _confirmationMark;
    private BoxCollider _mouseDetector;

    void OnTriggerEnter2D (Collider2D c) {
        hasMouseFocus = true;
    }

    void OnTriggerExit2D (Collider2D c) {
        hasMouseFocus = false;
    }

    void Start () {
        _outerBorder = transform.GetChild(0).Find("outer border")
            .GetComponent<SpriteRenderer>();
        _deletionMark = transform.GetChild(0).Find("delete mark").gameObject;
        _confirmationMark = transform.GetChild(0).Find("confirmation mark").gameObject;

        Unmark();
    }

    public void SetMark (Color color, PreviewMark mark) {
        _confirmationMark.SetActive(mark == PreviewMark.toSelect);
        _deletionMark.SetActive(mark == PreviewMark.toDelete);
        _outerBorder.color = color;
        this.mark = mark;
    }

    public void MarkForDeletion () {
        SetMark(controller.danger, PreviewMark.toDelete);
    }

    public void MarkForSelection () {
        SetMark(controller.selected, PreviewMark.toSelect);
    }

    public void Unmark () {
        SetMark(controller.normal, PreviewMark.normal);
    }

    public void Delete () {
        GetComponent<Animator>().SetTrigger("delete");
    }

    public void Resolve () {
        if (mark == PreviewMark.toDelete) {
            Delete();
        } else if (mark == PreviewMark.toSelect) {
            GetComponent<Animator>().SetTrigger("select");
        }
    }

    public void Initialize (LevelLink link) {
        // transform.GetChild(0).Find("preview renderer")
        //     .GetComponent<Map.PreviewMapRenderer>().SetPreviewSource(new Level(link.initial));
    }
}
