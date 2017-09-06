using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Lvl;

public class UndoScreen : MonoBehaviour {
    public float xOffset = 0;

    public float previewSize = 5;
    public float marginToBorder = 1;
    public float marginBetween = 0.5f;
    public float carousellSpeed = 10;
    public GameObject previewPrototype;
    public GameObject carousell;

    private List<Preview> _chain = new List<Preview>();
    private Vector3 _originalCarousellPosition;

    public float _minOffset;
    public float _maxOffset;

    void Start () {
        _originalCarousellPosition = carousell.transform.localPosition;
    }

    void FixedUpdate () {
        if (Input.GetKey("right")) {
            xOffset += (carousellSpeed * Time.deltaTime);
        } else if (Input.GetKey("left")) {
            xOffset -= (carousellSpeed * Time.deltaTime);
        }

        xOffset = Mathf.Max(_minOffset, Mathf.Min(_maxOffset, xOffset));

        Vector3 middleRightmost =
            Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height/2, 0));
        transform.position =
            Vector3.Scale(middleRightmost + new Vector3(-previewSize - marginToBorder, -previewSize/2, 0),
                          new Vector3(1, 1, 0));
        carousell.transform.localPosition =
            _originalCarousellPosition + new Vector3(xOffset, 0, 0);
    }

    public void Initialize (ChainOfLevels chain) {
        Clear();
        Vector3 middleRightmost =
            Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height/2, 0));

        transform.position =
            Vector3.Scale(middleRightmost + new Vector3(- previewSize - marginToBorder, 0, 0),
                          new Vector3(1, 1, 0));

        int i=0;
        foreach (LevelLink link in chain.chain) {
            Preview created = Instantiate(previewPrototype).GetComponent<Preview>();
            _chain.Add(created);
            created.undoScreen = this;
            created.Initialize(link, i);
            created.transform.parent = carousell.transform;
            created.transform.localPosition = Vector3.zero;
            created.transform.Translate(-(marginBetween + previewSize) * i, 0, 0);
            i++;
        }

        _minOffset = 0;
        _maxOffset = (_chain.Count - 2) * (previewSize + marginBetween);
    }

    public void Clear () {
        if (carousell != null) {
            Destroy(carousell);
        }
        carousell = new GameObject("previews");
        carousell.transform.parent = transform;
        carousell.transform.localPosition = Vector3.zero;

        _chain = new List<Preview>();
    }

    public void Select (int index) {
        for (int i=0; i<index; i++) {
            _chain[i].SetMark(PreviewMark.toDelete);
        }

        _chain[index].SetMark(PreviewMark.toSelect);

        for (int i=index+1; i<_chain.Count; i++) {
            _chain[i].SetMark(PreviewMark.normal);
        }
    }
}
