using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : StateMachineBehaviour
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

        // CharacterControlScript���擾
        controller = animator.GetComponent<CharacterControlScript>();
        // �ړ��ʂ�������
        controller.velocity = Vector3.zero;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // ���g�łȂ��ꍇ
        if (!myPV.isMine)
            return;

        // �v�b�V����ԂɑJ�ڂ���
        if (Input.GetButtonDown("Push") && !controller.IsPush())
        {
            animator.SetTrigger("Push");
            // Push����
            controller.PushControl();
            return;
        }
        // �X���C�f�B���O��ԂɑJ�ڂ���
        if (Input.GetButtonDown("Slide"))
        {
            animator.SetTrigger("Slide");
            return;
        }
        // �W�����v��ԂɑJ�ڂ���
        if (Input.GetButtonDown("Jump") && controller.IsGrounded())
        {
            // Jump�t���O���I��
            animator.SetBool("Jump", true);
            return;
        }

        // ���s�A�j���[�V����
        animator.SetFloat("Speed", controller.MoveInput().magnitude);

        // �ړ��̃x�N�g�����v�Z
        Vector3 velocity = controller.TargetDirection().normalized * controller.MoveInput().magnitude * controller.speed;
        controller.velocity.x = velocity.x;
        controller.velocity.z = velocity.z;
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
