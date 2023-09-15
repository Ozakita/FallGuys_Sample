using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{
    public Transform target;    //�ǐՂ���I�u�W�F�N�g��transform
    public Vector3 offset;      //�ǐՑΏۂ̒��S�ʒu�����p�I�t�Z�b�g
    private Vector3 lookAt;     //target��offset�ɂ�钍��������W

    // �^�[�Q�b�g�ƃJ�����Ԃ̋���
    [SerializeField] private float distance = 8.0f;
    [SerializeField] private float distance_min = 1.0f;
    [SerializeField] private float distance_max = 10.0f;

    // �J��������]������p�x
    private Vector2 current = new Vector2(0, 0);
    //�J������]�p�W��(�l���傫���قǉ�]���x���オ��)
    [SerializeField] private float moveX = 1.0f;     // �J����X������]�W��
    [SerializeField] private float moveY = 0.5f;     // �J����Y������]�W��
    private const float YAngle_MIN = -50.0f;   //�J������Y�����̍ŏ��p�x
    private const float YAngle_MAX = 0.0f;    //�J������Y�����̍ő�p�x

    // Ray�̔���ɗp����Layer
    [SerializeField] private LayerMask layerMask = default;
    // Sphere�̔��a
    [SerializeField] private float radius;
    // �Փ˂�L���ɂ��邩�H
    [SerializeField] private bool isCollideEnable = false;
    // �Փ˃t���O
    private bool isCollide = false;
    // Ray�œ�����������
    RaycastHit hit;

    void Update()
    {
        // �J�����ʒu�����Z�b�g����i�v���C���[�̑O�����������j
        //if (Input.GetButtonDown("CameraReset"))
        //{
        //    transform.forward = target.forward;
        //    Debug.Log("Reset");
        //}

        //�}�E�X�E�N���b�N�������Ă���Ƃ������}�E�X�̈ړ��ʂɉ����ăJ��������]
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
        // �J�����̉�]�̐���
        current.y = Mathf.Clamp(current.y, YAngle_MIN, YAngle_MAX);
        // �^�[�Q�b�g�ƃJ�����̋����̐���
        distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel"), distance_min, distance_max);
    }
    void LateUpdate()
    {
        // �^�[�Q�b�g�����Ȃ��ꍇ
        if (target == null)
            return;

        // �������W��target�ʒu+offset�̍��W
        lookAt = target.position + offset;

        //�J�������񏈗�
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(-current.y, current.x, 0);

        // �J�����̈ʒu��ύX
        transform.position = lookAt + rotation * dir;
        // �J������LookAt�̕����Ɍ���������
        transform.LookAt(lookAt);

        // �Փ˂��L�����H
        if (!isCollideEnable)
            return;

        // �J�����̓����蔻�菈��
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
        // �^�[�Q�b�g�̕�����Ray
        Ray ray = new Ray(transform.position, transform.forward);
        // �^�[�Q�b�g�ƃJ�����̋���
        float distanceTarget = Vector3.Distance(transform.position, lookAt);
        return Physics.SphereCast(ray, radius, out hit, distanceTarget, layerMask);
    }

    // Debug�p��Ray����������
    private void OnDrawGizmos()
    {
        // �Փ˂��L�����H
        if (!isCollideEnable)
            return;

        // �ڒn���莞�͗΁A�󒆂ɂ���Ƃ��͐Ԃɂ���
        Gizmos.color = isCollide ? Color.green : Color.red;
        float distanceTarget = Vector3.Distance(transform.position, lookAt);
        Gizmos.DrawRay(transform.position, transform.forward * distanceTarget);
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
