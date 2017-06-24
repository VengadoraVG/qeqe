using UnityEngine;
using System;
using System.Collections;
using QeqeInput;

namespace Qeqe {
    public class Digger : MonoBehaviour {
        public Raycaster bellowDigger;
        public Raycaster frontalDigger;
        public bool isDigging = false;

        public ParticleSystem leftDust;
        public ParticleSystem rightDust;

        public Movement movement;

        public Tile digged;

        void Start () {
            movement = GetComponent<Movement>();
        }
        
        void Update () {
            if (movement.IsStandingStill()) {
                Tile newDiggedTile = GetDiggedTile();

                if (digged != newDiggedTile) {
                    if (digged != null) {
                        digged.StopGettingDigged();
                    }

                    if (newDiggedTile != null) {
                        isDigging = true;
                        newDiggedTile.StartGettingDigged();
                    }
                    digged = newDiggedTile;
                }
            } else {
                if (digged != null) {
                    digged.StopGettingDigged();
                    digged = null;
                }
            }

            if (digged == null) {
                isDigging = false;
            }

            if (!isDigging) {
                movement.Unblock();
            } else {
                movement.Block();
            }

            movement.animator.SetBool("IsDigging", isDigging);
        }

        public Tile GetDiggedTile () {
            try {
                if (Verbs.BelowDig) {
                    return bellowDigger.GetImpacted().GetComponent<Tile>();
                }

                if (Verbs.FrontalDig) {
                    return frontalDigger.GetImpacted().GetComponent<Tile>();
                }
            } catch (NullReferenceException) {}

            return null;
        }
    }
}
