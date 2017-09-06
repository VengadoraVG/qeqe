using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Util {
    public static string Prettify (int[,] M) {
        string pretty = "";

        for (int i=0; i<M.GetLength(0); i++) {
            for (int j=0; j<M.GetLength(1); j++) {
                pretty += M[i,j];
            }
            pretty += "\n";
        }

        return pretty;
    }

    public static int[,] Clone (int[,] M) {
        return CloneInto(M, new int[M.GetLength(0), M.GetLength(1)]);
    }

    public static int[,] CloneInto (int[,] M, int[,] c) {
        for (int i=0; i<c.GetLength(0); i++) {
            for (int j=0; j<c.GetLength(1); j++) {
                c[i, j] = M[i, j];
            }
        }

        return c;
    }

    public static T LastOf<T> (List<T> list) {
        return list[list.Count - 1];
    }

    public static void CopyTransform (Transform original, Transform copy) {
        copy.position = original.position;
        copy.localScale = original.localScale;
        copy.rotation = original.rotation;
        copy.parent = original.parent;
    }
}
