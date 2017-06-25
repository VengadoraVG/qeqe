using UnityEngine;
using System.Collections;

namespace Powerup {
    public class Bone : Consumable {
        public GameObject theSprite;
        public GameObject theArrow;

        public override void GetConsumed () {
            theSprite.GetComponent<Animator>().SetTrigger("GetConsumed");
            Consumer.instance.bones++;
            Destroy(theArrow);
        }
    }
}
