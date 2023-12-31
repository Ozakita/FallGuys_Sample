using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

// キャラクター基底クラス
public abstract class CharacterBase : NetworkBehaviour
{
    // 状態クラス
    protected class State
    {
        // 状態
        public const int Idle = 0;
        public const int Move = 1;

        // 現在の状態
        public int current = Idle;
    }
    protected State state;

    // 状態の更新
    protected delegate void UpdateState();
    protected UpdateState updateState;
    // 状態タイマー
    protected float stateTimer = 0.0f;

    // 必要なコンポーネント
    protected Animator animator;
    [SerializeField]
    protected NetworkMecanimAnimator networkAnim;
    [SerializeField]
    protected CapsuleCollider capsuleCollider;
    [SerializeField]
    protected Rigidbody rigid;
    [SerializeField]
    protected SphereCastCheck check;

    // キャラクターの開始処理
    public abstract void CharaStart();

    // キャラクターの更新処理
    public abstract void CharaUpdate();

    // 最初の設定
    protected void SetUp()
    {
        animator = networkAnim.Animator;
    }

    // 現在の状態時間の長さを取得
    protected float StateTimeLength()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length;
    }
}
