using UnityEngine;
using System.Collections;

public class CopyPosition : MonoBehaviour {
    public GameObject target;
    
    void Update () {
        transform.position = target.transform.position;
        transform.localScale = target.transform.localScale;
    }
}
