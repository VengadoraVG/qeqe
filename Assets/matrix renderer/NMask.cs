namespace MatrixRenderer {
    public class NMask {
        public enum Direction {
            up = 0,
            right = 1,
            down = 2,
            left = 3
        };

        public static int[,] mapOfPows = { {0, 1, 0},
                                           {8, 0, 2},
                                           {0, 4, 0} };

        public static bool Is (int value, Direction d) {
            return (value & (1 << (int) d)) != 0;
        }

        public static int Complement (int value) {
            int r = 0,
                i = 0;
            while ((1<<i) < value) {
                if ((value & (1<<i)) == 0) {
                    r += (1<<i);
                }
                i++;
            }

            return r;
        }
    }
}
