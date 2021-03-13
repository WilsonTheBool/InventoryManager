using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnEnd : StateMachineBehaviour
{


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        MovementController.movementController.RemoveObject(animator.gameObject);
        Destroy(animator.gameObject);
    }



}
