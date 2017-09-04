using UnityEngine;

using System;
using System.Collections;

using QeqeInput;

namespace Qeqe {
    public class Movement : MonoBehaviour {
        public Vector2 maxTilesJumped = new Vector2(4, 3);
        public SpriteRenderer tileSample;
        public Rigidbody2D body;
        public FloorDetector floorDetector;

        public float maxSpeed = 2;
        public float jumpVelocity = 10;
        public float maxYVelocity = 10;

        public Animator animator;

        private float _higherJumpTimeVerification;
        private bool _isBlocked;

        void Start () {
            body = GetComponent<Rigidbody2D>();
            CalculateVelocities();
        }

        void FixedUpdate () {
            if (!_isBlocked) {
                XAxisMovementUpdate();
            }
            // trim y axis velocity
            body.velocity = new Vector3(body.velocity.x, Mathf.Max(-maxYVelocity, body.velocity.y));
        }

        void Update () {
            if (!_isBlocked) {
                _MecanimUpdate();
                JumpUpdate();
            }

            ViewOrientationUpdate();
        }

        private IEnumerator _HigherJumpVerifier () {
            yield return new WaitForSeconds(_higherJumpTimeVerification);

            if (!Verbs.JumpHigher) {
                body.velocity = Vector3.Scale(body.velocity, new Vector2(1,0.5f));
            } else {
                StartCoroutine(_HigherJumpVerifier());
            }
        }

        private void _MecanimUpdate () {
            animator.SetFloat("SpeedX", Mathf.Abs(body.velocity.x));
            animator.SetFloat("VelocityY", !floorDetector.IsInFloor()? body.velocity.y: 0);
        }

        public float CalculateJumpVelocity (float tiles) {
            return Mathf.Sqrt(-2 * Physics2D.gravity.y * tiles * tileSample.bounds.size.x);
        }

        public void JumpUpdate () {
            if (Verbs.Jump && floorDetector.IsInFloor()) {
                body.velocity = new Vector2(body.velocity.x, jumpVelocity);
                animator.SetTrigger("Jump");
                StopAllCoroutines();
                StartCoroutine(_HigherJumpVerifier());
            }
        }

        public void XAxisMovementUpdate () {
            float x = Input.GetAxis("Horizontal") * maxSpeed;
            body.velocity = new Vector2(x, body.velocity.y);
        }

        public void ViewOrientationUpdate () {
            if (Input.GetAxis("Horizontal") == 0)
                return;

            float y = 0;
            if (Input.GetAxis("Horizontal") < 0) {
                y = 180;
            }

            transform.rotation = Quaternion.Euler(0, y, 0);
        }

        public void CalculateVelocities () {
            float tileSize = tileSample.bounds.size.x;

            jumpVelocity = CalculateJumpVelocity(maxTilesJumped.y);
            float timeOnAir = 4 * maxTilesJumped.y * tileSize / jumpVelocity;
            maxSpeed = maxTilesJumped.x * tileSize / timeOnAir;

            _higherJumpTimeVerification = (timeOnAir/4) * 0.5f;
        }

        public void Block () {
            _isBlocked = true;
        }

        public void Unblock () {
            _isBlocked = false;
        }

        public bool IsStandingStill () {
            return floorDetector.IsInFloor() &&
                Mathf.Abs(body.velocity.x) < 0.3f;
        }
    }
}
