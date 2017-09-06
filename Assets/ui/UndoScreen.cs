using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Lvl;

public class UndoScreen : MonoBehaviour {
    public float previewSize = 5;
    public float marginToBorder = 1;
    public float marginBetween = 0.5f;
    public GameObject previewPrototype;

    private List<Preview> _chain = new List<Preview>();

    public void Initialize (ChainOfLevels chain) {
        Vector3 middleRightmost =
            Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height/2, 0));

        transform.position =
            Vector3.Scale(middleRightmost + new Vector3(- previewSize - marginToBorder, 0, 0),
                          new Vector3(1, 1, 0));

        foreach (LevelLink link in chain.chain) {
            Preview created = Instantiate(previewPrototype).GetComponent<Preview>();
            _chain.Add(created);
            created.Initialize(link);
            created.transform.parent = transform;
            created.transform.localPosition = Vector3.zero;
            created.transform.Translate(-marginBetween - previewSize, 0, 0);
        }
    }
}
