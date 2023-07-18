using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{
    private const float YAngle_MIN = -89.0f;   //�J������Y�����̍ŏ��p�x
    private const float YAngle_MAX = 89.0f;     //�J������Y�����̍ő�p�x

    public Transform target;    //�ǐՂ���I�u�W�F�N�g��transform
    public Vector3 offset;      //�ǐՑΏۂ̒��S�ʒu�����p�I�t�Z�b�g
    private Vector3 lookAt;     //target��offset�ɂ�钍��������W

    private float distance = 10.0f;    //�L�����N�^�[�ƃJ�����Ԃ̊p�x
    private float distance_min = 1.0f;  //�L�����N�^�[�Ƃ̍ŏ�����
    private float distance_max = 20.0f; //�L�����N�^�[�Ƃ̍ő勗��
    private float currentX = 0.0f;  //�J������X�����ɉ�]������p�x
    private float currentY = 0.0f;  //�J������Y�����ɉ�]������p�x

    //�J������]�p�W��(�l���傫���قǉ�]���x���オ��)
    private float moveX = 4.0f;     //�}�E�X�h���b�O�ɂ��J����X������]�W��
    private float moveY = 2.0f;     //�}�E�X�h���b�O�ɂ��J����Y������]�W��
    private float moveX_QE = 2.0f;  //QE�L�[�ɂ��J����X������]�W��

    void Start()
    {

    }

    void Update()
    {
        //Q��E�L�[�ŃJ������]
        if (Input.GetKey(KeyCode.Q))
        {
            currentX += -moveX_QE;
        }
        if (Input.GetKey(KeyCode.E))
        {
            currentX += moveX_QE;
        }

        //�}�E�X�E�N���b�N�������Ă���Ƃ������}�E�X�̈ړ��ʂɉ����ăJ��������]
        if (Input.GetMouseButton(1))
        {
            currentX += Input.GetAxis("Mouse X") * moveX;
            currentY += Input.GetAxis("Mouse Y") * moveY;
            currentY = Mathf.Clamp(currentY, YAngle_MIN, YAngle_MAX);

        }
        distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel"), distance_min, distance_max);
    }
    void LateUpdate()
    {
        if (target != null)  //target���w�肳���܂ł̃G���[���
        {
            lookAt = target.position + offset;  //�������W��target�ʒu+offset�̍��W

            //�J�������񏈗�
            Vector3 dir = new Vector3(0, 0, -distance);
            Quaternion rotation = Quaternion.Euler(-currentY, currentX, 0);

            transform.position = lookAt + rotation * dir;   //�J�����̈ʒu��ύX
            transform.LookAt(lookAt);   //�J������LookAt�̕����Ɍ���������
        }

    }

}
