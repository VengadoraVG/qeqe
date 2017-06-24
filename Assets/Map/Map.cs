using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Map : MonoBehaviour {
    public Sprite[] floorSprites;
    public int width;
    public int height;
    public GameObject mapPosition;
    public GameObject tilePrototype;

    public GameObject[,] tiles;
    public TextAsset floorInfo;

    public char floorChar = '1';
    public char voidChar = '0';

    public static Map instance;

    [HideInInspector]
    public int[,] W;
    [HideInInspector]
    public Vector2 tileSize;

    private int[,] mapOfPows = {{0,1,0},
                                {8,0,2},
                                {0,4,0}};

    private string _mapChar = "";
    private Dictionary<int, int> _hashedFloorIndex;
    private static int[] _hashesByFloorSprite = {0, 6, 14, 12, 7, 15, 13, 3, 11, 9, 4, 8, 1, 2, 10, 5, 0};

    void Start () {
        instance = this;
        Initialize();
    }

    void Update () {
        if (!Application.isPlaying) {
            Initialize();
        }
    }

    private string _GetSanitizedRaw () {
        string raw = floorInfo.text;
        string sanitized = "";

        for (int i=0; i<raw.Length; i++) {
            bool safe = false;

            for (int j=0; j<_mapChar.Length; j++) {
                if (raw[i] == _mapChar[j]) {
                    safe = true;
                    break;
                }
            }

            if (safe) {
                sanitized += raw[i];
            }
        }

        return sanitized;
    }


    public void Initialize () {
        _mapChar = "" + floorChar + voidChar;
        tileSize = tilePrototype.GetComponent<SpriteRenderer>().bounds.size;
        _hashedFloorIndex = new Dictionary<int, int>();

        for (int i=0; i<_hashesByFloorSprite.Length; i++) {
            _hashedFloorIndex[_hashesByFloorSprite[i]] = i;
        }

        DigestFloorInfo();
        MakeEverythingCoherent();
        PaintMap();
    }

    public void MakeCoherent (int row, int column) {
        int hash = 0;

        if (W[row, column] == 0) {
            return;
        }

        for (int i=-1; i<2; i++) {
            for (int j=-1; j<2; j++) {
                try {
                    if (W[row + i, column + j] != 0) {
                        hash += mapOfPows[i+1, j+1];
                    }
                } catch (IndexOutOfRangeException) {}
            }
        }

        W[row, column] = _hashedFloorIndex[hash];
    }

    public void PaintMap () {
        ClearTileMap();

        for (int i=0; i<height; i++) {
            for (int j=0; j<width; j++) {
                if (W[i,j] != 0) {
                    tiles[i,j] = Instantiate(tilePrototype);
                    tiles[i,j].transform.parent = mapPosition.transform;
                    tiles[i,j].transform.localPosition = new Vector3(j*tileSize.x, (height-1-i)*tileSize.y);
                    tiles[i,j].GetComponent<SpriteRenderer>().sprite = floorSprites[W[i,j]];
                    tiles[i,j].GetComponent<Tile>().SetIndexes(i,j);
                }
            }
        }
    }

    public void ClearTileMap () {
        GameObject newMapPosition = new GameObject(mapPosition.name);
        newMapPosition.transform.position = mapPosition.transform.position;
        newMapPosition.transform.localScale = mapPosition.transform.localScale;
        newMapPosition.transform.parent = mapPosition.transform.parent;

        DestroyImmediate(mapPosition);
        mapPosition = newMapPosition;

        tiles = new GameObject[height, width];
    }

    public void DigestFloorInfo () {
        string raw = floorInfo.text;

        width = raw.IndexOf('\n') - 1; // o__O ¡¿-1?! FUCKING TEXTASSET!!! replaces \n with two chars >__>
        height = raw.Length/(width + 1);

        W = new int[height, width];
        raw = _GetSanitizedRaw();

        for (int i=0; i<height; i++) {
            for (int j=0; j<width; j++) {
                W[i,j] = raw[i*width + j] == voidChar? 0 : 1; // this should be a switch case o.O 
            }
        }
    }

    public void MakeEverythingCoherent () {
        for (int i=0; i<height; i++) {
            for (int j=0; j<width; j++) {
                MakeCoherent(i, j);
            }
        }
    }

    public void Destroy (int row, int col) {
        W[row,col] = 0;

        for (int i=-1; i<2; i++) {
            for (int j=-1; j<2; j++) {
                try { MakeCoherent(row + i, col + j); } catch {};
            }
        }
        
    }

    public void Destroy (Tile tile) {
        Destroy(tile.row, tile.column);
    }
}
