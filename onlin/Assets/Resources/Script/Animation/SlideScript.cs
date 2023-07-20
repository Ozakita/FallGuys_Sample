using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideScript : StateMachineBehaviour
{
    private CharacterControlScript controller;
    private PhotonView myPV;
    private float timer;

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
        controller.velocity.x = 0.0f;
        controller.velocity.z = 0.0f;
        // �^�C�}�[�̏�����
        timer = 0.0f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // ���g�łȂ��ꍇ
        if (!myPV.isMine)
            return;

        // ���n�����ꍇ
        if (controller.IsGrounded())
        {
            // Jump�t���O���I�t
            animator.SetBool("Jump", false);
        }

        // �^�C�}�[�̍X�V
        timer += Time.deltaTime;

        // �A�j���[�V�����̎��Ԃɂ���Ĉړ��ʂ�ύX
        Vector3 velocity = (timer >= stateInfo.length / 2) 
            ? Vector3.zero : controller.Forward() * controller.slideSpeed;
        // �ړ��ʂ��X�V
        controller.velocity.x = velocity.x;
        controller.velocity.z = velocity.z;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Slide");
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
