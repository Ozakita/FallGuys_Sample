using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushScript : StateMachineBehaviour
{
    private CharacterControlScript controller;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // CharacterControlScript‚ğæ“¾
        controller = animator.GetComponent<CharacterControlScript>();
        // ˆÚ“®—Ê‚ğ‰Šú‰»
        controller.velocity.x = 0.0f;
        controller.velocity.z = 0.0f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // ˆÚ“®—Ê‚ğ‰Šú‰»
        controller.velocity.x = 0.0f;
        controller.velocity.z = 0.0f;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Push");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
