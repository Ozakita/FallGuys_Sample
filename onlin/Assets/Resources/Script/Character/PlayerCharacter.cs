using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCharacter : CharacterBase
{
    [SerializeField, Header("オンラインにするかどうか")]
    //public bool isOnline = true;
    // オンライン化に必要なコンポーネントを設定
    public PhotonView myPV;
    public PhotonTransformView myPTV;

    // 状態クラス
    private new class State : CharacterBase.State
    {
        // 状態
        public const int JumpStart = 2;
        public const int JumpAir = 3;
        public const int JumpEnd = 4;
        public const int Push = 5;
        public const int Slide = 6;
        public const int Damage = 7;
    }

    // カメラ
    public Camera playerCamera;

    // パラメータ用変数
    [SerializeField, Header("移動速度")]
    private float moveSpeed;
    [SerializeField, Header("ジャンプ量")]
    private float jumpSpeed;
    [SerializeField, Header("スライディング量")]
    private float slideSpeed;
    [SerializeField, Header("旋回速度")]
    private float rotateSpeed;
    [SerializeField, Header("重力")]
    private float gravity;
    [SerializeField, Header("ノックバック量")]
    private float knockBack;

    // 移動処理用の変数
    [NonSerialized]
    public Vector3 velocity;
    private Vector3 input;

    // Push用の変数
    public GameObject pushObject;
    [SerializeField, Header("Pushオブジェクト生成位置のオフセット")]
    private float pushObjectOffset;
    private bool isPush = false;

    //吹っ飛び床用のGameObject
    public GameObject FlyingFloor;

    // Damage用(ダメージモーション中)
    public bool isDamage = false;

    // 一旦ここで開始処理
    private void Start()
    {
        CharaStart();
    }

    // 一旦ここで更新処理
    private void Update()
    {
        CharaUpdate();
    }

    // 開始処理
    override public void CharaStart()
    {
        // 自キャラではない場合   
        if (!myPV.isMine)
            return;

        // カメラの設定
        playerCamera = Camera.main;
        // カメラのターゲットに自身を設定
        playerCamera.GetComponent<CameraScript>().target = this.gameObject.transform;
        // 状態クラスを生成
        state = new State();

        // 移動量の初期化
        velocity = Vector3.zero;
        // 移動入力の初期化
        input = Vector3.zero;

        // アニメーションの再生 
        ChangeState(State.Idle, "Idle03");
    }

    // 更新処理
    override public void CharaUpdate()
    {
        // 自キャラではない場合   
        if (!myPV.isMine)
            return;

        // ジャンプ開始
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeState(State.JumpStart, "JumpStart");
            return;
        }
        // スライディング
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ChangeState(State.Slide, "Slide");
            // xz成分の移動量を初期化
            velocity.x = 0.0f;
            velocity.z = 0.0f;
            return;
        }
        // プッシュ
        if (Input.GetKeyDown(KeyCode.F) && check.isCollided() && !isPush)
        {
            ChangeState(State.Push, "Push");
            // Pushオブジェクトを生成
            PushGenerate();
            // xz成分の移動量を初期化
            velocity.x = 0.0f;
            velocity.z = 0.0f;
            return;
        }

        // 状態の更新
        if (updateState != null)
        {
            updateState();
            stateTimer += Time.deltaTime;
        }

        // 着地していない時は重力をかける
        if (!check.isCollided())
        {
            velocity.y -= gravity * Time.deltaTime;
        }

        // 移動する（オンライン用）
        myPTV.SetSynchronizedValues(velocity * Time.deltaTime, 0);
        // 移動する
        rigid.MovePosition(rigid.position + velocity * Time.deltaTime);
    }

    // 移動方向を取得
    Vector3 TargetDirection()
    {
        // カメラの前方向ベクトルを取得
        Vector3 forward = Camera.main.transform.forward;
        forward.y = 0.0f;
        // カメラの右方向ベクトルを取得
        Vector3 right = Camera.main.transform.right;
        right.y = 0.0f;

        // 移動の入力
        input.x = Input.GetAxisRaw("Horizontal");
        input.z = Input.GetAxisRaw("Vertical");

        // 移動方向を計算
        return input.x * right + input.z * forward;
    }

    // 旋回制御
    void RotationControl()
    {
        // 移動方向が一定量変化しない場合はスキップ
        if (TargetDirection().sqrMagnitude <= 0.01f)
            return;

        // 緩やかに移動方向を変える
        float step = rotateSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.Slerp(transform.forward, TargetDirection(), step);
        Quaternion rot = Quaternion.LookRotation(newDirection);
        transform.rotation = rot;
    }

    // 状態を変更する
    private void ChangeState(int nextState, string animationName)
    {
        // タイマーの初期化
        stateTimer = 0.0f;
        // 状態の変更
        state.current = nextState;

        // 状態更新を設定
        switch (state.current)
        {
            case State.Idle: updateState = Idle; break;
            case State.Move: updateState = Move; break;
            case State.JumpStart: updateState = JumpStart; break;
            case State.JumpAir: updateState = JumpAir; break;
            case State.JumpEnd: updateState = JumpEnd; break;
            case State.Push: updateState = Push; break;
            case State.Slide: updateState = Slide; break;
            case State.Damage: updateState = Damage; break;
        }

        // アニメーションの再生 
        animator.Play(animationName, 0, 0.0f);
    }

    // 待機
    private void Idle()
    {
        // 入力なしの場合
        if (TargetDirection().sqrMagnitude <= 0.01f)
            return;

        // 移動状態へ
        ChangeState(State.Move, "Run01FWD");
    }

    // 移動
    private void Move()
    {
        // 入力なしの場合は待機状態へ
        if (TargetDirection().sqrMagnitude <= 0.01f)
        {
            ChangeState(State.Idle, "Idle03");
            // 移動量を初期化
            velocity = Vector3.zero;
            return;
        }

        // 移動量を設定
        Vector3 moveVec = TargetDirection().normalized * input.magnitude * moveSpeed;
        velocity.x = moveVec.x;
        velocity.z = moveVec.z;

        // 旋回制御
        RotationControl();
    }

    // ジャンプ開始
    private void JumpStart()
    {
        // モーションが終了したらジャンプ中状態へ
        if (stateTimer > StateTimeLength())
        {
            ChangeState(State.JumpAir, "JumpAir");
            return;
        }

        // 上昇する
        velocity.y = jumpSpeed;
    }

    // ジャンプ中
    private void JumpAir()
    {
        // 着地したらジャンプ終了状態へ
        if (check.isCollided())
        {
            ChangeState(State.JumpEnd, "JumpEnd");
            // 移動量を初期化
            velocity = Vector3.zero;
            return;
        }

        // 移動量を設定
        Vector3 moveVec = TargetDirection().normalized * input.magnitude * moveSpeed;
        velocity.x = moveVec.x;
        velocity.z = moveVec.z;

        // 旋回制御
        RotationControl();
    }

    // ジャンプ終了
    private void JumpEnd()
    {
        // モーションが終了したら待機状態へ
        if (stateTimer > StateTimeLength())
        {
            ChangeState(State.Idle, "Idle03");
            // 移動量を初期化
            velocity = Vector3.zero;
            return;
        }
    }

    // プッシュ
    private void Push()
    {
        // モーションが終了したら待機状態へ
        if (stateTimer > StateTimeLength())
        {
            ChangeState(State.Idle, "Idle03");
            // 移動量を初期化
            velocity = Vector3.zero;
            return;
        }
    }

    // スライディング
    private void Slide()
    {
        // 着地した場合
        if (check.isCollided())
        {
            velocity = Vector3.zero;
        }

        // モーションが終了したら待機状態へ
        if (stateTimer > StateTimeLength())
        {
            ChangeState(State.Idle, "Idle03");
            // 移動量を初期化
            velocity = Vector3.zero;
            return;
        }

        // 時間経過によって移動量を変更する
        Vector3 moveVec = (stateTimer >= StateTimeLength() / 2)
            ? Vector3.zero : transform.forward * slideSpeed;

        // 移動量を更新
        velocity.x = moveVec.x;
        velocity.z = moveVec.z;
    }

    // ダメージ
    private void Damage()
    {
        // モーションが終了したら待機状態へ
        if (stateTimer > StateTimeLength())
        {
            ChangeState(State.Idle, "Idle03");
            // 移動量を初期化
            velocity = Vector3.zero;
            return;
        }
    }

    // Pushオブジェクトを生成
    private void PushGenerate()
    {
        // Pushフラグをオン
        isPush = true;
        StartCoroutine(_pushCollide(1f));
    }

    IEnumerator _pushCollide(float pauseTime)
    {
        // 自身の中心位置
        Vector3 center = transform.position + capsuleCollider.center;
        // 生成位置
        Vector3 position = center + transform.forward * pushObjectOffset;
        // RPCでPushの判定を生成
        myPV.RPC("PushObject", PhotonTargets.AllViaServer, position, transform.rotation);
        // pauseTimeだけ待つ
        yield return new WaitForSeconds(pauseTime);
        // Pushisフラグをオフ
        isPush = false;
    }

    [PunRPC] // Pushの判定を生成
    void PushObject(Vector3 instpos, Quaternion instrot, PhotonMessageInfo info)
    {
        GameObject push = Instantiate(pushObject, instpos, instrot) as GameObject;
        // 自分の情報を乗せる
        push.GetComponent<PushManagerScript>().player = info.sender;
    }

    // 被弾処理
    private void OnTriggerEnter(Collider other)
    {
        // 自キャラ以外なら処理しない
        if (!myPV.isMine)
            return;

        // 衝突処理
        OnCollide(other);

        if (other.CompareTag("FlyingFloor"))
        {
            // ノックバックする
            velocity = other.transform.forward * 6.0f;
            return;
        }

        if (other.CompareTag("Push"))
        {
            // Push判定の生成者
            PhotonPlayer pushPlayer = other.GetComponent<PushManagerScript>().player;
            // 自分が生成したもの、または衝突したものがPush判定以外の場合
            if (pushPlayer.IsLocal)
                return;
            // ダメージ状態へ遷移
            animator.SetTrigger("Damage");
            // ダメージフラグをオン
            isDamage = true;
            // ノックバックする
            velocity = other.transform.forward * knockBack;
        }
    }

    // 衝突処理
    private void OnCollide(Collider other)
    {
        // 自分の座標
        Vector3 position = transform.position;
        // 相手の座標
        Vector3 target = other.transform.position;
        // y座標を除く
        position.y = 0.0f;
        target.y = 0.0f;
        // 相手との距離
        float distance = Vector3.Distance(position, target);
        // 相手の半径
        float targetRadius = (other.CompareTag("Player"))
            ? other.GetComponent<CapsuleCollider>().radius
            : other.GetComponent<SphereCollider>().radius;
        // 衝突判定球の半径同士を加えた長さを求める
        float length = capsuleCollider.radius + targetRadius;
        // 衝突判定球の重なっている長さを求める
        float overlap = length - distance;
        // 重なっている部分の半分の距離だけ離れる
        Vector3 leaveDistance = (position - target).normalized * overlap * 0.5f;
        transform.Translate(leaveDistance, Space.World);
    }
}
