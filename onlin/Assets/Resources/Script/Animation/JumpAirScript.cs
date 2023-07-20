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
        // PhotonView���擾
        myPV = animator.GetComponent<PhotonView>();
        // ���g�łȂ��ꍇ
        if (!myPV.isMine)
            return;

        controller = animator.GetComponent<CharacterControlScript>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // ���g�łȂ��ꍇ
        if (!myPV.isMine)
            return;

        // �X���C�f�B���O�ɑJ�ڂ���
        if (Input.GetButtonDown("Slide"))
        {
            animator.SetTrigger("Slide");
            return;
        }

        // ���n������J�ڂ���
        if (controller.IsGrounded())
        {
            // Jump�t���O���I�t
            animator.SetBool("Jump", false);
            return;
        }

        // �󒆈ړ�����
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
