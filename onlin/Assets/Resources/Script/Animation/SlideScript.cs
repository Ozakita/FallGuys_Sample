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
        // PhotonViewを取得
        myPV = animator.GetComponent<PhotonView>();
        // 自身でない場合
        if (!myPV.isMine)
            return;

        // CharacterControlScriptを取得
        controller = animator.GetComponent<CharacterControlScript>();
        // 移動量を初期化
        controller.velocity.x = 0.0f;
        controller.velocity.z = 0.0f;
        // タイマーの初期化
        timer = 0.0f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 自身でない場合
        if (!myPV.isMine)
            return;

        // 着地した場合
        if (controller.IsGrounded())
        {
            // Jumpフラグをオフ
            animator.SetBool("Jump", false);
        }

        // タイマーの更新
        timer += Time.deltaTime;

        // アニメーションの時間によって移動量を変更
        Vector3 velocity = (timer >= stateInfo.length / 2) 
            ? Vector3.zero : controller.Forward() * controller.slideSpeed;
        // 移動量を更新
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
