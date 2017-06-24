using UnityEngine;
using System.Collections;
using System;

namespace QeqeCamera {
    [ExecuteInEditMode]
    public class Focus : MonoBehaviour {
        public GameObject target;

        void Update () {
            float originalZ = transform.position.z;
            transform.position =
                Vector3.Scale(target.transform.position, new Vector3(1,1,0)) +
                new Vector3(0,0, originalZ);
        }
    }
}
