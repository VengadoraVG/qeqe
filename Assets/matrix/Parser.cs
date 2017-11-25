using UnityEngine;
using System.Collections;

namespace Matrix {
    public class Parser {
        private static string _mapChar = "x10hbqn.";
        private static string _voidChar = "0bqn.";

        public static string GetSanitizedRaw (TextAsset raw) {
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

        public static Status Digest (TextAsset raw) {
            // FUCKING TEXTASSET!!! replaces \n with two chars >__>
            string sanitized = GetSanitizedRaw(raw);
            int width = raw.text.IndexOf('\n') - 1;
            int height = sanitized.Length/(width);

            Status status = new Status(height, width);

            for (int i=0; i<height; i++) {
                for (int j=0; j<width; j++) {
                    char symbol = '0';
                    try {
                        symbol = sanitized[i*width + j];
                    } catch (System.IndexOutOfRangeException) {}
                    status.W[i,j] = !_IsVoid(symbol);
                    status.hp[i,j] = _IsIndestructible(symbol)? (!_IsVoid(symbol)? Mathf.Infinity: 0): 1;
                    status.B[i,j] = _IsBone(symbol);
                    status.V[i,j] = (symbol == '.');
                }
            }

            return status;
        }

        private static bool _IsVoid (char symbol) {
            return _voidChar.Contains(symbol + "");
        }

        private static bool _IsIndestructible (char symbol) {
            return symbol == _mapChar[0];
        }

        private static bool _IsBone (char symbol) {
            return symbol == _mapChar[3] || symbol == _mapChar[4];
        }

        private static bool _IsNextLvl (char symbol) {
            return symbol == _mapChar[6];
        }

        private static bool _IsQeqe (char symbol) {
            return symbol == _mapChar[5];
        }
    }
}
