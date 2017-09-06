using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Lvl;

public class UndoScreen : MonoBehaviour {
    public float previewSize = 5;
    public float marginToBorder = 1;
    public float marginBetween = 0.5f;
    public GameObject previewPrototype;
    public GameObject previewPosition;

    private List<Preview> _chain = new List<Preview>();

    void FixedUpdate () {
        Vector3 middleRightmost =
            Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height/2, 0));
        transform.position =
            Vector3.Scale(middleRightmost + new Vector3(-previewSize - marginToBorder, -previewSize/2, 0),
                          new Vector3(1, 1, 0));
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
            created.Initialize(link);
            created.transform.parent = previewPosition.transform;
            created.transform.localPosition = Vector3.zero;
            created.transform.Translate(-(marginBetween + previewSize) * i, 0, 0);
            i++;
        }
    }

    public void Clear () {
        if (previewPosition != null) {
            Destroy(previewPosition);
        }
        previewPosition = new GameObject("previews");
        previewPosition.transform.parent = transform;
        previewPosition.transform.localPosition = Vector3.zero;

        _chain = new List<Preview>();
    }
}
