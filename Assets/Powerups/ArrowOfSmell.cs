using UnityEngine;
using System.Collections;

public class ArrowOfSmell : MonoBehaviour {
    public GameObject target;
    public CircleCollider2D smellAreaDelimiter;
    public float maxDistance;
    public GameObject smeller;

    public SpriteRenderer rend;

    void Start () {
        maxDistance = smellAreaDelimiter.radius;
        smeller = GameObject.FindWithTag(Tags.arrowsSource);
    }

    void Update () {
        transform.position = smeller.transform.position;
        Vector3 d = target.transform.position - transform.position;
        transform.right = d;
        rend.color = new Color(rend.color.r, rend.color.g, rend.color.b,
                               Mathf.Max(1 - d.magnitude / maxDistance, 0));
    }
}
