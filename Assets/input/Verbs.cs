using UnityEngine;

namespace QeqeInput {
    
    public class Verbs {
        public static bool Jump {
            get { return Input.GetKeyDown(KeyCode.UpArrow); } }
        public static bool JumpHigher {
            get { return Input.GetKey(KeyCode.UpArrow); } }
        public static bool BellowDig {
            get { return Input.GetKey(KeyCode.S); } }
        public static bool FrontalDig {
            get { return Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A); } }
        public static bool Dig {
            get { return BellowDig || FrontalDig; } }
        public static bool Smell {
            get { return Input.GetKey(KeyCode.Space); } }
        public static bool Reset {
            get { return Input.GetKeyDown(KeyCode.Escape); } }
        public static bool Undo {
            get { return Input.GetKeyDown(KeyCode.Delete); } }
    }
}
