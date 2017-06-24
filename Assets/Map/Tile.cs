using UnityEngine;
using System.Collections;
using QeqeInput;
using Qeqe;

public class Tile : MonoBehaviour {
    public int row;
    public int column;
    public int hp = 1;

    public void SetIndexes (int row, int column) {
        this.row = row;
        this.column = column;
    }

    public IEnumerator GetDigged (GameObject digger) {
        float elapsedTime = 0;

        do {
            yield return null;
            elapsedTime += Time.deltaTime;
        } while (elapsedTime < hp && Input.GetKey(Verbs.Dig));

        digger.GetComponent<Movement>().StopDigging();

        if (Input.GetKey(Verbs.Dig)) {
            Map.instance.Destroy(this);
        }
    }
}
