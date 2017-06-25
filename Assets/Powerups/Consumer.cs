using UnityEngine;
using System.Collections;

public class Consumer : MonoBehaviour {
    public static Consumer instance;
    public int bones;

    void Start () {
        instance = this;
    }
    
    void Update () {
        
    }
}
