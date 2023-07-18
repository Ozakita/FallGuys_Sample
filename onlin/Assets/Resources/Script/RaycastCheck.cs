using UnityEngine;

public class RaycastCheck : MonoBehaviour
{
    // Ray�̒���
    [SerializeField] private float rayLength = 1f;

    // Ray���ǂꂭ�炢�g�̂ɂ߂荞�܂��邩
    [SerializeField] private float rayOffset;

    // Ray�̔���ɗp����Layer
    [SerializeField] private LayerMask layerMask = default;

    private CharacterController controller;
    private bool isGround;

    private void Start()
    {
        // CharacterController���擾
        controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        // �ڒn����
        isGround = CheckGrounded();
    }

    private bool CheckGrounded()
    {
        // �������̏����ʒu�Ǝp��
        // �኱�g�̂ɂ߂荞�܂����ʒu���甭�˂��Ȃ��Ɛ���������ł��Ȃ���������
        var ray = new Ray(origin: transform.position + Vector3.up * rayOffset, direction: Vector3.down);

        // Raycast��hit���邩�ǂ����Ŕ���
        // ���C���̎w���Y�ꂸ��
        return Physics.Raycast(ray, rayLength, layerMask);
    }

    // Debug�p��Ray����������
    private void OnDrawGizmos()
    {
        // �ڒn���莞�͗΁A�󒆂ɂ���Ƃ��͐Ԃɂ���
        Gizmos.color = isGround ? Color.green : Color.red;
        Gizmos.DrawRay(transform.position + Vector3.up * rayOffset, Vector3.down * rayLength);
    }

    public bool isGrounded()
    {
        return isGround;
    }
}

