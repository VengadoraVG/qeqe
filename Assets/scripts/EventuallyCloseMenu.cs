using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventuallyCloseMenu : StateMachineBehaviour {
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        // animator.gameObject.GetComponent<UndoScreen>().Close();
    }
}
