using UnityEngine;
using System.Collections;
using QeqeInput;
using Qeqe;

public class ArrowOfSmell : MonoBehaviour {
    public GameObject target;
    public CircleCollider2D smellAreaDelimiter;
    public float maxDistance;
    public GameObject smeller;

    public SpriteRenderer rend;

    private Movement _movement;

    void Start () {
        maxDistance = smellAreaDelimiter.radius;
        smeller = GameObject.FindWithTag(Tags.arrowsSource);
        _movement = smeller.transform.parent.gameObject.GetComponent<Movement>();
    }

    void Update () {
        if (Verbs.Smell && _movement.IsStandingStill() && !_movement.animator.GetBool("IsDigging")) {
            _movement.animator.SetBool("IsSmelling", true);
            _movement.Block();
            transform.position = smeller.transform.position;
            Vector3 d = target.transform.position - transform.position;
            transform.right = d;
            rend.color = new Color(rend.color.r, rend.color.g, rend.color.b,
                                   Mathf.Max(1 - d.magnitude / maxDistance, 0));
        } else {
            _movement.animator.SetBool("IsSmelling", false);
            _movement.Unblock();
            rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, 0);
        }
    }
}
