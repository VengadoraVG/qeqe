using UnityEngine;
using System;
using System.Collections;
using QeqeInput;

namespace Qeqe {
    public class Digger : MonoBehaviour {
        public Raycaster bellowDigger;
        public Raycaster frontalDigger;
        public bool isDigging = false;

        public ParticleSystem dust;

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

                    if (newDiggedTile != null && Consumer.instance.CanDig()) {
                        isDigging = true;
                        newDiggedTile.StartGettingDigged();
                        dust.Play();
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
            if (!isDigging) {
                dust.Stop();
            }
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
