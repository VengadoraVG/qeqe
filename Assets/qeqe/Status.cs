using UnityEngine;
using System.Collections;

namespace Qeqe {
    [System.Serializable]
    public class Status {
        public Vector3 position;
        public int energy;
        public Vector3 velocity;
        public GameObject owner;

        public Status (GameObject qeqe) {
            position = qeqe.transform.position;
            energy = qeqe.GetComponent<Controller>().energy;
            velocity = qeqe.GetComponent<Movement>().body.velocity;

            this.owner = qeqe;
        }

        public void Set () {
            owner.transform.position = position;
            owner.GetComponent<Controller>().energy = energy;
            owner.GetComponent<Movement>().body.velocity = velocity;
        }
    }
}
