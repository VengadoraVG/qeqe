using UnityEngine;
using System.Collections;

public class LevelStatus {
    public int[,] W;
    public int[,] I; // mask of indestructible tiles
    public int[,] B; // mask of consumables
    public int[,] N; // mask of next level portals

    public int width;
    public int height;

    public int energy;
    public Vector2 position;

    public void Digest (string raw, Map map) {
        width = raw.IndexOf('\n') - 1; // new line at end of file
        height = raw.Length/(width + 1);

        W = new int[height, width];
        I = new int[height, width];
        B = new int[height, width];
        N = new int[height, width];
        raw = map.GetSanitizedRaw(raw); // FUCKING TEXTASSET!!! replaces \n with two chars >__>

        for (int i=0; i<height; i++) {
            for (int j=0; j<width; j++) {
                char symbol = raw[i*width + j];
                W[i,j] = map.IsVoid(symbol)? 0 : 1;
                I[i,j] = symbol == map.indestructibleChar? 1 : 0;
                B[i,j] = (symbol == map.hiddenBoneChar || symbol == map.boneChar)? 1 : 0;
                N[i,j] = symbol == map.nextChar? 1 : 0;
                if (symbol == map.qeqeChar) {
                    position = new Vector2(j, i);
                }
            }
        }
    }
}
