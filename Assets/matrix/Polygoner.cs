using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Polygoner {
    public bool[,] V;
    public List<Vector2> corners = new List<Vector2>();

    private int[,] _matrixOfPows = {{1 , 2  , 4},
                                    {8 , 256, 16},
                                    {32, 64 , 128}};

    public Polygoner (Matrix.Controller matrix) {
        V = Util.Clone(matrix.status.V);
    }

    private int _GetHash (int row, int column) {
        int hash = 0;
        for (int i=-1; i<2; i++) {
            for (int j=-1; j<2; j++) {
                if (IsVoid(row + i, column + j)) {
                    hash += _matrixOfPows[i+1, j+1];
                }
            }
        }

        return hash;
    }

    private bool _EvaluateCorner (int concaveHash, int convexHash, int cellHash) {
        return ((cellHash & concaveHash) != 0) ||
            ((cellHash & convexHash) != 0 && (cellHash & concaveHash) == 0);
    }

    public void FindBorders () {
        Vector2 currentVertex = new Vector2(0,0);
        bool foundCorner = false;

        for (int i=0; i<V.GetLength(0) && !foundCorner; i++) {
            for (int j=0; j<V.GetLength(1) && !foundCorner; j++) {
                if (HasCorner(i, j)) {
                    currentVertex = FindCorner(i, j);
                    foundCorner = true;
                }
            }
        }

        Vector2 firstVertex = currentVertex;
        corners.Add(firstVertex);
        // x is i, y is j
        List<Vector2> d = PossibleDirections(currentVertex);
        Vector2 lastDirection = d[0];
        currentVertex += lastDirection;

        while (currentVertex != firstVertex) {
            Vector2 newDirection;
            d = PossibleDirections(currentVertex);
            
            if (d[0] != -lastDirection) {
                newDirection = d[0];
            } else {
                newDirection = d[1];
            }

            if (newDirection != lastDirection) {
                corners.Add(currentVertex);
            }

            lastDirection = newDirection;
            currentVertex += lastDirection;
        }
    }

    public List<Vector2> PossibleDirections (Vector2 vertex) {
        return PossibleDirections((int) vertex.x, (int) vertex.y);
    }

    public List<Vector2> PossibleDirections (int ci, int cj) {
        List<Vector2> directions = new List<Vector2>();
        if (IsVoid(ci-1, cj-1) != IsVoid(ci-1, cj)) {
            directions.Add(new Vector2(-1, 0));
        }
        if (IsVoid(ci, cj) != IsVoid(ci-1, cj)) {
            directions.Add(new Vector2(0, 1));
        }
        if (IsVoid(ci, cj-1) != IsVoid(ci, cj)) {
            directions.Add(new Vector2(1, 0));
        }
        if (IsVoid(ci, cj-1) != IsVoid(ci-1, cj-1)) {
            directions.Add(new Vector2(0, -1));
        }

        return directions;
    }

    public Vector2 FindCorner (int row, int column) {
        int hash = _GetHash(row, column);

        if (_EvaluateCorner(10, 1, hash)) // w
            return (new Vector2(row, column));
        if (_EvaluateCorner(18, 4, hash)) // x
            return (new Vector2(row, column+1));
        if (_EvaluateCorner(80, 128, hash)) // y
            return (new Vector2(row+1, column+1));
        if (_EvaluateCorner(72, 32, hash)) // z
            return (new Vector2(row+1, column));

        return new Vector2(-Mathf.Infinity, -Mathf.Infinity);
    }

    public bool HasCorner (int row, int column) {
        int voidCount = 0;

        if (IsVoid(row, column)) return false;

        for (int i=-1; i<2; i++) {
            for (int j=-1; j<2; j++) {
                if (IsVoid(row + i, column + j)) {
                    voidCount++;
                }
            }
        }

        return voidCount != 3;
    }

    public bool IsOutOfBounds (int row, int column) {
        return 0 > row || row >= V.GetLength(0) ||
            0 > column || column >= V.GetLength(1);
    }

    public bool IsVoid (int row, int column) {
        if (IsOutOfBounds(row, column)) return true;
        return V[row, column];
    }
}
