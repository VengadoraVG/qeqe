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

    public void StartGettingDigged () {
        StartCoroutine(_GetDigged());
    }

    private IEnumerator _GetDigged () {
        float elapsedTime = 0;

        do {
            yield return null;
            elapsedTime += Time.deltaTime;
        } while (elapsedTime < hp);

        Map.instance.Destroy(this);
    }

    public void StopGettingDigged () {
        StopAllCoroutines();
    }
}
