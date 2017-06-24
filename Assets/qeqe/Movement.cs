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

        public bool isDigging = false;
        public ParticleSystem leftDust;
        public ParticleSystem rightDust;

        public Animator animator;
        private float _higherJumpTimeVerification;

        void Start () {
            body = GetComponent<Rigidbody2D>();
            CalculateVelocities();
        }
    
        void FixedUpdate () {
            XAxisMovementUpdate();
            body.velocity = new Vector3(body.velocity.x, Mathf.Max(-maxYVelocity, body.velocity.y));
        }

        void Update () {
            JumpUpdate();
            _MecanimUpdate();
            _DigUpdate();
        }

        private IEnumerator _HigherJumpVerifier () {
            yield return new WaitForSeconds(_higherJumpTimeVerification);

            if (!Input.GetKey(Verbs.Jump)) {
                body.velocity = Vector3.Scale(body.velocity, new Vector2(1,0.5f));
            } else {
                StartCoroutine(_HigherJumpVerifier());
            }
        }

        private void _DigUpdate () {
            if (Input.GetKey(Verbs.Dig) && floorDetector.IsInFloor() && body.velocity.x == 0 && !isDigging) {
                StartDigging();
                StopAllCoroutines();
                StartCoroutine(floorDetector.dig.GetImpacted().GetComponent<Tile>().GetDigged(this.gameObject));
            } else if (!Input.GetKey(Verbs.Dig)) {
                StopDigging();
            }
            animator.SetBool("IsDigging", isDigging);
        }

        private void _MecanimUpdate () {
            animator.SetFloat("SpeedX", Mathf.Abs(body.velocity.x));
            animator.SetFloat("VelocityY", body.velocity.y);
        }

        public float CalculateJumpVelocity (float tiles) {
            return Mathf.Sqrt(-2 * Physics2D.gravity.y * tiles * tileSample.bounds.size.x);
        }

        public void JumpUpdate () {
            if (Input.GetKeyDown(Verbs.Jump) && floorDetector.IsInFloor()) {
                body.velocity = new Vector2(body.velocity.x, jumpVelocity);
                animator.SetTrigger("Jump");
                StopAllCoroutines();
                StartCoroutine(_HigherJumpVerifier());
            }
        }

        public void StartDigging () {
            isDigging = true;
            if (transform.localScale.x < 0) {
                leftDust.Play();
            } else {
                rightDust.Play();
            }
        }

        public void StopDigging () {
            isDigging = false;
            leftDust.Stop();
            rightDust.Stop();
        }

        public void XAxisMovementUpdate () {
            if (!isDigging) {
                float x = Input.GetAxis("Horizontal") * maxSpeed;
                body.velocity = new Vector2(x, body.velocity.y);
                if (body.velocity.x != 0) {
                    transform.localScale =
                        new Vector2(Mathf.Abs(transform.localScale.x) * Mathf.Sign(body.velocity.x),
                                    transform.localScale.y);
                }
            }
        }

        public void CalculateVelocities () {
            float tileSize = tileSample.bounds.size.x;

            jumpVelocity = CalculateJumpVelocity(maxTilesJumped.y);
            float timeOnAir = 4 * maxTilesJumped.y * tileSize / jumpVelocity;
            maxSpeed = maxTilesJumped.x * tileSize / timeOnAir;

            _higherJumpTimeVerification = (timeOnAir/4) * 0.5f;
        }
    }
}
