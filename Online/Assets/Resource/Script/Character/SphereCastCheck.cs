using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCastCheck : MonoBehaviour
{
    // Ray�̒���
    [SerializeField] private float rayLength = 1f;
    // Ray���ǂꂭ�炢�g�̂ɂ߂荞�܂��邩
    [SerializeField] private float rayOffset;
    // Ray�̔���ɗp����Layer
    [SerializeField] private LayerMask layerMask = default;
    // Sphere�̔��a
    [SerializeField] private float radius;
    // �Փ˃t���O
    private bool isCollide = false;

    private void FixedUpdate()
    {
        // �Փ˔���
        isCollide = CheckGrounded();
    }

    private bool CheckGrounded()
    {
        Ray ray = new Ray(transform.position + Vector3.up * rayOffset, -transform.up);
        return Physics.SphereCast(ray, radius, rayLength, layerMask);
    }

    private void OnDrawGizmos()
    {
        // �Փ˔��莞�͗΁A�Փ˂��Ă��Ȃ��Ƃ��͐Ԃɂ���
        Gizmos.color = isCollide ? Color.green : Color.red;
        Gizmos.DrawRay(transform.position + Vector3.up * rayOffset, -transform.up * rayLength);
        Gizmos.DrawWireSphere((transform.position + Vector3.up * rayOffset) - transform.up * rayLength, radius);
    }

    public bool isCollided()
    {
        return isCollide;
    }
}
