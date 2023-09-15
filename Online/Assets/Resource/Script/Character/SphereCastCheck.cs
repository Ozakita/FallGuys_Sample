using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCastCheck : MonoBehaviour
{
    // Rayの長さ
    [SerializeField] private float rayLength = 1f;
    // Rayをどれくらい身体にめり込ませるか
    [SerializeField] private float rayOffset;
    // Rayの判定に用いるLayer
    [SerializeField] private LayerMask layerMask = default;
    // Sphereの半径
    [SerializeField] private float radius;
    // 衝突フラグ
    private bool isCollide = false;

    private void FixedUpdate()
    {
        // 衝突判定
        isCollide = CheckGrounded();
    }

    private bool CheckGrounded()
    {
        Ray ray = new Ray(transform.position + Vector3.up * rayOffset, -transform.up);
        return Physics.SphereCast(ray, radius, rayLength, layerMask);
    }

    private void OnDrawGizmos()
    {
        // 衝突判定時は緑、衝突していないときは赤にする
        Gizmos.color = isCollide ? Color.green : Color.red;
        Gizmos.DrawRay(transform.position + Vector3.up * rayOffset, -transform.up * rayLength);
        Gizmos.DrawWireSphere((transform.position + Vector3.up * rayOffset) - transform.up * rayLength, radius);
    }

    public bool isCollided()
    {
        return isCollide;
    }
}
