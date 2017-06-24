using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
    public int row;
    public int column;

    public void SetIndexes (int row, int column) {
        this.row = row;
        this.column = column;
    }
}
