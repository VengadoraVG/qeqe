using UnityEngine;
using System.Collections;
using QeqeInput;
using Qeqe;

public class Tile : MonoBehaviour {
    public int row;
    public int column;
    public float hp = 1;

    public GameObject indestructibleIndicator;

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

    public void SetIndestructible () {
        hp = Mathf.Infinity;
        indestructibleIndicator.SetActive(true);
    }
}
