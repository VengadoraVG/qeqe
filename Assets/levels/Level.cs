using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Lvl {
    public class Level {
        public TextAsset floorInfo;

        public int[,] W;
        public int[,] I; // mask of indestructible tiles
        public int[,] B; // mask of consumables
        public int[,] N; // mask of next level portals

        public int width;
        public int height;

        public TextAsset raw;

        public Vector2 position;
        public int lvlIndex;

        private string _mapChar = "x10hbqn";
        private string _voidChar = "0bqn";

        private int[,] mapOfPows = {{0,1,0},
                                    {8,0,2},
                                    {0,4,0}};

        private static int[] _hashesByFloorSprite = {0, 6, 14, 12, 7, 15, 13, 3, 11, 9, 4, 8, 1, 2, 10, 5, 0};
        private Dictionary<int, int> _hashedFloorIndex;

        public Level (TextAsset raw, int lvlIndex) {
            this.raw = raw;
            Initialize();
            this.lvlIndex = lvlIndex;
        }

        private bool _IsVoid (char symbol) {
            return _voidChar.Contains(symbol + "");
        }

        private bool _IsIndestructible (char symbol) {
            return symbol == _mapChar[0];
        }

        private bool _IsBone (char symbol) {
            return symbol == _mapChar[3] || symbol == _mapChar[4];
        }

        private bool _IsNextLvl (char symbol) {
            return symbol == _mapChar[6];
        }

        private bool _IsQeqe (char symbol) {
            return symbol == _mapChar[5];
        }

        public void Load (LevelStatus status) {
            Util.CloneInto(status.W, W);
            Util.CloneInto(status.B, B);
            MakeEverythingCoherent(); // Justin Case
        }

        public void Initialize () {
            _hashedFloorIndex = new Dictionary<int, int>();

            for (int i=0; i<_hashesByFloorSprite.Length; i++) {
                _hashedFloorIndex[_hashesByFloorSprite[i]] = i;
            }

            Digest();
        }

        public string GetSanitizedRaw () { // raw should be retrieved from own properties
            string sanitized = "";

            for (int i=0; i<raw.text.Length; i++) {
                bool safe = false;

                for (int j=0; j<_mapChar.Length; j++) {
                    if (raw.text[i] == _mapChar[j]) {
                        safe = true;
                        break;
                    }
                }

                if (safe) {
                    sanitized += raw.text[i];
                }
            }

            return sanitized;
        }

        public void Digest () {
            width = raw.text.IndexOf('\n') - 1; // new line at end of file
            height = raw.text.Length/(width + 1);

            W = new int[height, width];
            I = new int[height, width];
            B = new int[height, width];
            N = new int[height, width];
            string sanitized = GetSanitizedRaw(); // FUCKING TEXTASSET!!! replaces \n with two chars >__>

            for (int i=0; i<height; i++) {
                for (int j=0; j<width; j++) {
                    char symbol = '0';
                    try {
                        symbol = sanitized[i*width + j];
                    } catch (IndexOutOfRangeException) {}
                    W[i,j] = _IsVoid(symbol)? 0 : 1;
                    I[i,j] = _IsIndestructible(symbol)? 1 : 0;
                    B[i,j] = _IsBone(symbol)? 1 : 0;
                    N[i,j] = _IsNextLvl(symbol)? 1 : 0;
                    if (_IsQeqe(symbol)) {
                        position = new Vector2(j, i);
                    }
                }
            }

            MakeEverythingCoherent();
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

        public void DestroyTile (int row, int col) {
            W[row, col] = 0;

            for (int i=-1; i<2; i++) {
                for (int j=-1; j<2; j++) {
                    try {
                        MakeCoherent(row + i, col + j);
                    } catch (IndexOutOfRangeException) {}
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

        public void RemoveBone (int row, int column) {
            B[row, column] = 0;
        }
    }
}
