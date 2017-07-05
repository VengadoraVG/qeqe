using UnityEngine;
using System.Collections;

public class NextLevelPortal : MonoBehaviour {
    void OnTriggerEnter2D (Collider2D c) {
        Lvl.LevelController.instance.NextLevel();
    }

    public void Initialize (int row, int column, int width, int height) {
        int rowDistanceToBorder = (int) Mathf.Min(height - row -1, row);
        int columnDistanceToBorder = (int) Mathf.Min(width - column -1, column);

        // verticalmente
        if (rowDistanceToBorder < columnDistanceToBorder) {
            if (row < height/2) { 
                transform.Rotate(0, 0, 90);
            } else {
                transform.Rotate(0, 0, -90);
            }
        } else { // horizontalmente
            if (column < width/2) {
                transform.Rotate(0, 180, 0);
            }
        }
    }
}
