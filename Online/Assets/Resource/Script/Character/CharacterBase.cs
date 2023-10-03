using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

// �L�����N�^�[���N���X
public abstract class CharacterBase : NetworkBehaviour
{
    // ��ԃN���X
    protected class State
    {
        // ���
        public const int Idle = 0;
        public const int Move = 1;

        // ���݂̏��
        public int current = Idle;
    }
    protected State state;

    // ��Ԃ̍X�V
    protected delegate void UpdateState();
    protected UpdateState updateState;
    // ��ԃ^�C�}�[
    protected float stateTimer = 0.0f;

    // �K�v�ȃR���|�[�l���g
    protected Animator animator;
    [SerializeField]
    protected NetworkMecanimAnimator networkAnim;
    [SerializeField]
    protected CapsuleCollider capsuleCollider;
    [SerializeField]
    protected Rigidbody rigid;
    [SerializeField]
    protected SphereCastCheck check;

    // �L�����N�^�[�̊J�n����
    public abstract void CharaStart();

    // �L�����N�^�[�̍X�V����
    public abstract void CharaUpdate();

    // �ŏ��̐ݒ�
    protected void SetUp()
    {
        animator = networkAnim.Animator;
    }

    // ���݂̏�Ԏ��Ԃ̒������擾
    protected float StateTimeLength()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length;
    }
}
