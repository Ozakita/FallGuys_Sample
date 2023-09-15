using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{
    public Transform target;    //追跡するオブジェクトのtransform
    public Vector3 offset;      //追跡対象の中心位置調整用オフセット
    private Vector3 lookAt;     //targetとoffsetによる注視する座標

    // ターゲットとカメラ間の距離
    [SerializeField] private float distance = 8.0f;
    [SerializeField] private float distance_min = 1.0f;
    [SerializeField] private float distance_max = 10.0f;

    // カメラを回転させる角度
    private Vector2 current = new Vector2(0, 0);
    //カメラ回転用係数(値が大きいほど回転速度が上がる)
    [SerializeField] private float moveX = 1.0f;     // カメラX方向回転係数
    [SerializeField] private float moveY = 0.5f;     // カメラY方向回転係数
    private const float YAngle_MIN = -50.0f;   //カメラのY方向の最小角度
    private const float YAngle_MAX = 0.0f;    //カメラのY方向の最大角度

    // Rayの判定に用いるLayer
    [SerializeField] private LayerMask layerMask = default;
    // Sphereの半径
    [SerializeField] private float radius;
    // 衝突を有効にするか？
    [SerializeField] private bool isCollideEnable = false;
    // 衝突フラグ
    private bool isCollide = false;
    // Rayで当たったもの
    RaycastHit hit;

    void Update()
    {
        // カメラ位置をリセットする（プレイヤーの前方向を向く）
        //if (Input.GetButtonDown("CameraReset"))
        //{
        //    transform.forward = target.forward;
        //    Debug.Log("Reset");
        //}

        //マウス右クリックを押しているときだけマウスの移動量に応じてカメラが回転
        if (Input.GetMouseButton(1))
        {
            current.x += Input.GetAxis("Mouse X") * 4.0f;
            current.y += Input.GetAxis("Mouse Y") * 2.0f;
        }
        //else
        //{
        //    current.x += Input.GetAxis("CameraYaw") * moveX;
        //    current.y += Input.GetAxis("CameraPitch") * moveY;
        //}
        // カメラの回転の制限
        current.y = Mathf.Clamp(current.y, YAngle_MIN, YAngle_MAX);
        // ターゲットとカメラの距離の制限
        distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel"), distance_min, distance_max);
    }
    void LateUpdate()
    {
        // ターゲットがいない場合
        if (target == null)
            return;

        // 注視座標はtarget位置+offsetの座標
        lookAt = target.position + offset;

        //カメラ旋回処理
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(-current.y, current.x, 0);

        // カメラの位置を変更
        transform.position = lookAt + rotation * dir;
        // カメラをLookAtの方向に向けさせる
        transform.LookAt(lookAt);

        // 衝突が有効か？
        if (!isCollideEnable)
            return;

        // カメラの当たり判定処理
        if (CheckCollide())
        {
            transform.position = hit.point;
            isCollide = true;
        }
        else
        {
            isCollide = false;
        }
    }

    private bool CheckCollide()
    {
        // ターゲットの方向のRay
        Ray ray = new Ray(transform.position, transform.forward);
        // ターゲットとカメラの距離
        float distanceTarget = Vector3.Distance(transform.position, lookAt);
        return Physics.SphereCast(ray, radius, out hit, distanceTarget, layerMask);
    }

    // Debug用にRayを可視化する
    private void OnDrawGizmos()
    {
        // 衝突が有効か？
        if (!isCollideEnable)
            return;

        // 接地判定時は緑、空中にいるときは赤にする
        Gizmos.color = isCollide ? Color.green : Color.red;
        float distanceTarget = Vector3.Distance(transform.position, lookAt);
        Gizmos.DrawRay(transform.position, transform.forward * distanceTarget);
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
