using UnityEngine;
using System;
using System.Collections;
using MatrixRenderer;
using QeqeInput;

namespace Qeqe {
    public class Digger : MonoBehaviour {
        public Raycaster bellowDigger;
        public Raycaster frontalDigger;
        public bool isDigging = false;
        public ParticleSystem dust;
        public Movement movement;
        public Tile digged;

        private Qeqe.Controller _controller;

        void Start () {
            movement = GetComponent<Movement>();
            _controller = GetComponent<Qeqe.Controller>();
        }

        void Update () {
            if (_controller.CanDig && movement.IsStandingStill()) {
                Tile currentlyDiggedTile = GetDiggedTile();

                if (digged != currentlyDiggedTile) {
                    if (digged != null)  {
                        _controller.matrixController.StopGettingDigged(digged.row, digged.column, this);
                    }

                    if (currentlyDiggedTile != null &&
                        _controller.matrixController.CanDig(this, currentlyDiggedTile)) {

                        isDigging = true;
                        _controller.matrixController.
                            StartGettingDigged(currentlyDiggedTile.row, currentlyDiggedTile.column, this);
                        dust.Play();
                    }

                    digged = currentlyDiggedTile;
                }
            } else {
                if (digged != null) {
                    _controller.matrixController.StopGettingDigged(digged.row, digged.column, this);
                    digged = null;
                }
            }

            isDigging = (digged != null);
            if (isDigging) {
                movement.Block();
            } else {
                movement.Unblock();
                dust.Stop();
            }

            movement.animator.SetBool("IsDigging", isDigging);
        }

        public Tile GetDiggedTile () {
            try {
                if (Verbs.BellowDig) {
                    return bellowDigger.GetImpacted().GetComponent<Tile>();
                } else if (Verbs.FrontalDig) {
                    return frontalDigger.GetImpacted().GetComponent<Tile>();
                }
            } catch (NullReferenceException) {}

            return null;
        }
    }
}
