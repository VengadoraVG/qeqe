using UnityEngine;

namespace QeqeInput {
    
    public class Verbs {
        public static bool Jump {
            get { return Input.GetKeyDown(KeyCode.UpArrow); } }
        public static bool JumpHigher {
            get { return Input.GetKey(KeyCode.UpArrow); } }
        public static bool BelowDig {
            get { return Input.GetKey(KeyCode.DownArrow); } }
        public static bool FrontalDig {
            get { return Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow); } }
        public static bool Dig {
            get { return BelowDig || FrontalDig; } }
        public static bool Smell {
            get { return Input.GetKey(KeyCode.S); } }
        public static bool Reset {
            get { return Input.GetKey(KeyCode.Escape); } }
    }
}
