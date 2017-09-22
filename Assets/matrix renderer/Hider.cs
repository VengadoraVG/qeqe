using UnityEngine;
using System.Collections;
using Powerup;

namespace MatrixRenderer {
    public class Hider : MonoBehaviour {
        public float hidingAlpha = 1;
        public bool active;
        public SpriteRenderer curtain;

        public Color hidedColor;
        public Color shownColor;
        public float opacityTransitionTime = 1.5f;

        void Start () {
            OnTriggerExit2D(null);
        }

        void OnTriggerExit2D (Collider2D c) {
            active = false;
            StartHiding();
        }

        void OnTriggerEnter2D (Collider2D c) {
            active = true;
            StartUnhiding();
        }

        public void RefreshSize () {
            BoxCollider2D collider = GetComponent<BoxCollider2D>();
            Vector2 tileSize = transform.parent.GetComponent<TileController>().size;

            curtain.transform.position = transform.position -
                Vector3.Scale((Vector3) tileSize/2, new Vector3(1,-1,1));
            collider.size = curtain.size = GetComponent<MapRenderer>().Size;
            collider.offset = Vector2.Scale(collider.size/2, new Vector2(1, -1)) -
                Vector2.Scale(tileSize/2, new Vector2(1, -1));
        }

        public void StartUnhiding () {
            StopAllCoroutines();
            StartCoroutine(_StartOpacityTransition(false));
        }

        public void StartHiding () {
            StopAllCoroutines();
            StartCoroutine(_StartOpacityTransition(true));
        }

        private IEnumerator _StartOpacityTransition (bool hide) {
            float elapsedTime = 0;
            Color initial = curtain.color;
            Color desired = hide? hidedColor : shownColor;

            while (elapsedTime < opacityTransitionTime) {
                yield return null;
                elapsedTime += Time.deltaTime;
                curtain.color = Color.Lerp(initial, desired,
                                           elapsedTime / opacityTransitionTime);
            }
        }
    }
}
