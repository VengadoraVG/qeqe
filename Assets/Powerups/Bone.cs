using UnityEngine;
using System.Collections;

namespace Powerup {
    public class Bone : Consumable {
        public GameObject theSprite;
        public GameObject theArrow;

        public override void GetConsumed () {
            theSprite.GetComponent<Animator>().SetTrigger("GetConsumed");
            theSprite.GetComponent<SpriteRenderer>().sortingLayerName = "Indicators";
            Destroy(theArrow);

            this.owner.TriggerBoneEaten(this.row, this.column, this.lastConsumer);
        }

        public void HideWithMap () {
            this.gameObject.SetActive(!this.owner.status.W[this.row, this.column]);
        }

        public void ShowWithMap () {
            this.gameObject.SetActive(true);
        }
    }
}
