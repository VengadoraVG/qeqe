using UnityEngine;
using System.Collections;
using Lvl;

public class Preview : MonoBehaviour {
    public UIController controller;
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

        controller = GameObject.FindWithTag("GameController").
            GetComponent<UIController>();
        SetMark(PreviewMark.normal);
    }

    public void SetMark (PreviewMark mark) {
        _outerBorder.color = controller.GetColor(mark);
        _confirmationMark.SetActive(mark == PreviewMark.toSelect);
        _deletionMark.SetActive(mark == PreviewMark.toDelete);
        this.mark = mark;
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
        transform.GetChild(0).Find("map renderer")
            .GetComponent<Map.MapPreviewer>().SetPreviewSource(link.status);
    }
}
