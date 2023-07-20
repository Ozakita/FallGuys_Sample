using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAirScript : StateMachineBehaviour
{
    private CharacterControlScript controller;
    private PhotonView myPV;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // PhotonViewを取得
        myPV = animator.GetComponent<PhotonView>();
        // 自身でない場合
        if (!myPV.isMine)
            return;

        controller = animator.GetComponent<CharacterControlScript>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 自身でない場合
        if (!myPV.isMine)
            return;

        // スライディングに遷移する
        if (Input.GetButtonDown("Slide"))
        {
            animator.SetTrigger("Slide");
            return;
        }

        // 着地したら遷移する
        if (controller.IsGrounded())
        {
            // Jumpフラグをオフ
            animator.SetBool("Jump", false);
            return;
        }

        // 空中移動処理
        controller.AirMoveControl();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
