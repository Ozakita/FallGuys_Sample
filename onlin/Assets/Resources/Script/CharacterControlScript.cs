using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControlScript : MonoBehaviour
{
    // オンライン化に必要なコンポーネントを設定
    public PhotonView myPV;
    public PhotonTransformView myPTV;

    // 移動処理に必要なコンポーネントを設定
    private Animator animator;                 // モーションのコントロール
    private CharacterController controller;    // キャラクター移動の管理
    private SphereCastCheck sphereCast;        // 着地しているかどうかの判定
    private Camera mainCam;

    // パラメータ用変数(inspectorビューで設定)
    public float speed;         // 移動速度
    public float jumpSpeed;     // ジャンプ力
    public float slideSpeed;    // スライディング力
    public float rotateSpeed;   // 回転速度
    public float gravity;       // 重力

    // 移動処理に必要なベクトル
    public Vector3 velocity;    // 移動量
    Vector3 targetDirection;    // 進行方向のベクトル
    Vector2 input;              // 移動の入力

    // Push用のGameObject
    public GameObject pushPrefab;
    private bool isPush = false;

    // 初期化
    void Start()
    {
        // 自キャラではない場合   
        if (!myPV.isMine)
            return;

        // Animatorを取得
        animator = GetComponent<Animator>();
        // CharacterControllerを取得
        controller = GetComponent<CharacterController>();
        // SphereCastCheckを取得
        sphereCast = GetComponent<SphereCastCheck>();
        // 移動量を初期化
        velocity = Vector3.zero;
        // 移動の入力を初期化
        input = Vector2.zero;

        // MainCameraのtargetにこのゲームオブジェクトを設定
        mainCam = Camera.main;
        mainCam.GetComponent<CameraScript>().target = this.gameObject.transform;
    }

    // 更新
    void Update()
    {
        // 自キャラではない場合   
        if (!myPV.isMine)
            return;

        // 移動処理
        MoveControl();
        // 重力処理
        GravityControl();
        // 旋回処理
        RotationControl();
        // 最終的な移動処理
        controller.Move(velocity * Time.deltaTime);
    }

    // 移動処理
    void MoveControl()
    {
        // 移動方向を計算
        input.y = Input.GetAxisRaw("Vertical");         // InputManagerの↑↓の入力       
        input.x = Input.GetAxisRaw("Horizontal");       // InputManagerの←→の入力 

        // カメラの正面方向ベクトルからY成分を除き、正規化してキャラが走る方向を取得
        Vector3 forward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 right = Camera.main.transform.right; //カメラの右方向を取得

        // カメラの方向を考慮したキャラの進行方向を計算
        targetDirection = input.x * right + input.y * forward;

        // スムーズな同期のためにPhotonTransformViewに速度値を渡す
        Vector3 velocity = controller.velocity;
        myPTV.SetSynchronizedValues(velocity, 0);
    }

    // 旋回処理
    void RotationControl()
    {
        Vector3 rotateDirection = velocity;
        rotateDirection.y = 0;

        //それなりに移動方向が変化する場合のみ移動方向を変える
        if (rotateDirection.sqrMagnitude > 0.01)
        {
            //緩やかに移動方向を変える
            float step = rotateSpeed * Time.deltaTime;
            Vector3 newDir = Vector3.Slerp(transform.forward, rotateDirection, step);
            transform.rotation = Quaternion.LookRotation(newDir);
        }
    }

    // 重力処理
    void GravityControl()
    {
        // 地面に着地している場合
        if (IsGrounded())
            return;
        // 重力を有効にする
        velocity.y -= gravity * Time.deltaTime;
    }

    // 空中移動処理
    public void AirMoveControl()
    {
        // 移動量
        Vector3 v;
        // 空中での移動制御
        v = Vector3.Scale(targetDirection, new Vector3(1, 0, 1)).normalized;
        v *= speed;
        // 移動量を更新
        velocity.x = v.x;
        velocity.z = v.z;
    }

    // Push処理
    public void PushControl()
    {
        // Pushフラグをオン
        isPush = true;
        StartCoroutine(_pushCollide(1f));
    }

    IEnumerator _pushCollide(float pauseTime)
    {
        // 自身の中心位置
        Vector3 center = transform.position + controller.center;
        // 生成位置
        Vector3 position = center + transform.forward;
        // RPCでPushの判定を生成
        myPV.RPC("PushGenerate", PhotonTargets.AllViaServer, position, transform.rotation);
        // pauseTimeだけ待つ
        yield return new WaitForSeconds(pauseTime);
        // Pushisフラグをオフ
        isPush = false;
    }

    [PunRPC] // Pushの判定を生成
    void PushGenerate(Vector3 instpos, Quaternion instrot, PhotonMessageInfo info)
    {
        GameObject push = Instantiate(pushPrefab, instpos, instrot) as GameObject;
        // 自分の情報を乗せる
        push.GetComponent<PushManagerScript>().player = info.sender;
    }

    // 被弾処理
    private void OnTriggerEnter(Collider other)
    {
        // 自キャラ以外なら処理しない
        if (!myPV.isMine)
            return;

        OnCollide(other);

        // Push判定の生成者
        PhotonPlayer player = other.GetComponent<PushManagerScript>().player;

        // 自分が生成したもの、または衝突したものがPush判定以外の場合
        if (player.IsLocal || !other.CompareTag("Push"))
            return;
        // ダメージ状態へ遷移
        animator.SetTrigger("Damage");
        // ノックバックする
        velocity = -other.transform.forward * 0.5f;
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
            ? other.GetComponent<CharacterController>().radius
            : other.GetComponent<SphereCollider>().radius;
        // 衝突判定球の半径同士を加えた長さを求める
        float length = controller.radius + targetRadius;
        // 衝突判定球の重なっている長さを求める
        float overlap = length - distance;
        // 重なっている部分の半分の距離だけ離れる
        Vector3 leaveDistance = (position - target).normalized * overlap * 0.5f;
        transform.Translate(leaveDistance, Space.World);
    }

    // 地面に着地しているかの取得
    public bool IsGrounded()
    {
        return sphereCast.isCollided();
    }

    // 進行方向のベクトルの取得
    public Vector3 TargetDirection()
    {
        return targetDirection;
    }

    // 移動の入力の取得
    public Vector2 MoveInput()
    {
        return input;
    }

    // 前方向ベクトルを取得
    public Vector3 Forward()
    {
        return this.gameObject.transform.forward;
    }

    // Pushフラグ
    public bool IsPush()
    {
        return isPush;
    }
}
