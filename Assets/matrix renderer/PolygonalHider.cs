using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MatrixRenderer {
    [RequireComponent(typeof(PolygonCollider2D))]
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class PolygonalHider : MonoBehaviour {
        public Color hidedColor;
        public Color shownColor;
        public float opacityTransitionTime = 1.5f;
        public bool active;

        private PolygonCollider2D _collider;
        private MeshFilter _meshFilter;
        private MapRenderer _mapRenderer;
        private Renderer _matRenderer;

        void Start () {
            _collider = GetComponent<PolygonCollider2D>();
            _meshFilter = GetComponent<MeshFilter>();
            _mapRenderer = transform.parent.GetComponent<MapRenderer>();

            _matRenderer = GetComponent<Renderer>();
            Material mat = new Material(_matRenderer.sharedMaterial);
            _matRenderer.sharedMaterial = mat;

            if (Application.isPlaying)
                OnTriggerExit2D(null);
        }

        void OnTriggerExit2D (Collider2D c) {
            if (c == null || c.gameObject.CompareTag("level focus detector")) {
                active = false;
                StartHiding();
            }
        }

        void OnTriggerEnter2D (Collider2D c) {
            if (c.gameObject.CompareTag("level focus detector")) {
                active = true;
                StartUnhiding();
            }
        }


        public void GeneratePolygon (List<Vector2> logicalPoints) {
            Start();
            Vector2 tileSize = _mapRenderer.TileSize;
            Vector2[] graphicalPoints = new Vector2[logicalPoints.Count];
            for (int i=0; i<graphicalPoints.Length; i++) {
                graphicalPoints[i] = Vector2.Scale(logicalPoints[i], tileSize);
                graphicalPoints[i] =
                    new Vector2(graphicalPoints[i].y - tileSize.y/2,
                                -graphicalPoints[i].x + tileSize.x/2);
            }

            _collider.points = graphicalPoints;
            DrawPolygon();
        }

        public void DrawPolygon () {
            Mesh mesh = new Mesh();
            _meshFilter.mesh = mesh;

            Vector3[] vertices = new Vector3[_collider.points.Length];
            for (int i=0; i<_collider.points.Length; i++) {
                vertices[i] = new Vector3(_collider.points[i].x, _collider.points[i].y,
                                          transform.localPosition.z);
            }

            Triangulator t = new Triangulator(_collider.points);
            int[] tris = t.Triangulate();

            mesh.vertices = vertices;
            mesh.triangles = tris;
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
            Color initial = _matRenderer.sharedMaterial.GetColor("_Color");
            Color desired = hide? hidedColor : shownColor;

            while (elapsedTime < opacityTransitionTime) {
                yield return null;
                elapsedTime += Time.deltaTime;
                _matRenderer.sharedMaterial.
                    SetColor("_Color", Color.Lerp(initial, desired,
                                                  elapsedTime / opacityTransitionTime));
            }
        }
    }
}
