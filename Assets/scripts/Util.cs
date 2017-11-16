using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Util {
    public delegate void ForEachChildrenAction (Object caller, Transform child);

    public static string Prettify<T> (T[,] M) {
        string pretty = "";

        for (int i=0; i<M.GetLength(0); i++) {
            for (int j=0; j<M.GetLength(1); j++) {
                pretty += M[i,j].ToString();
            }
            pretty += "\n";
        }

        return pretty;
    }

    public static T[,] Clone<T> (T[,] M) {
        return CloneInto(M, new T[M.GetLength(0), M.GetLength(1)]);
    }

    public static T[,] CloneInto<T> (T[,] M, T[,] c) {
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

    public static T FindComponent<T> (Transform child) {
        if (child == null) return default(T);
        T found = child.GetComponent<T>();
        if (found != null)
            return found;

        return FindComponent<T>(child.parent);
    }

    public static GameObject FindTag (Transform child, string tag) {
        if (child == null) return null;
        if (child.gameObject.CompareTag(tag)) return child.gameObject;

        return FindTag(child.parent, tag);
    }

    public static void ForEachChildren (Transform parent, ForEachChildrenAction action, Object caller) {
        if (parent == null) return;

        action(caller, parent);
        foreach (Transform child in parent) {
            ForEachChildren(child, action, caller);
        }
    }
}
