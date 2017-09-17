using UnityEngine;
using System.Collections;

namespace MatrixRenderer {
    public class Tile : MonoBehaviour {
        public int row, column;

        private int _tilesMask;

        public void Initialize (int row, int column, int tilesMask, float hp) {
            this.row = row;
            this.column = column;
            MakeCoherent(tilesMask);
            if (hp > 1) MakeIndestructible();
        }

        public void MakeCoherent (int tilesMask) {
            _tilesMask = tilesMask;
            for (int i=0; i<4; i++) DrawBorder((NMask.Direction) i);
        }

        public void DrawBorder (NMask.Direction direction) {
            GameObject border = transform.Find(direction.ToString()).gameObject;
            if (NMask.Is(_tilesMask, direction)) {
                border.SetActive(false);
            } else {
                border.SetActive(true);
            }
        }

        public void MakeIndestructible () {
            transform.Find("undestructible").gameObject.SetActive(true);
        }
    }
}
