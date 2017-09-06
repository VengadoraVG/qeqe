using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DestroyOnEnd : StateMachineBehaviour {
    override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Debug.Log("o_O");
        animator.gameObject.SetActive(false);
    }

}
