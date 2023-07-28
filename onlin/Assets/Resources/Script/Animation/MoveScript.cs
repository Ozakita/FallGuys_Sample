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
        // PhotonViewを取得
        myPV = animator.GetComponent<PhotonView>();
        // 自身でない場合
        if (!myPV.isMine)
            return;

        // CharacterControlScriptを取得
        controller = animator.GetComponent<CharacterControlScript>();
        // 移動量を初期化
        controller.velocity = Vector3.zero;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 自身でない場合
        if (!myPV.isMine)
            return;

        // プッシュ状態に遷移する
        if (Input.GetButtonDown("Push") && !controller.IsPush())
        {
            animator.SetTrigger("Push");
            // Push処理
            controller.PushControl();
            return;
        }
        // スライディング状態に遷移する
        if (Input.GetButtonDown("Slide"))
        {
            animator.SetTrigger("Slide");
            return;
        }
        // ジャンプ状態に遷移する
        if (Input.GetButtonDown("Jump") && controller.IsGrounded())
        {
            // Jumpフラグをオン
            animator.SetBool("Jump", true);
            return;
        }

        // 走行アニメーション
        animator.SetFloat("Speed", controller.MoveInput().magnitude);

        // 移動のベクトルを計算
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
