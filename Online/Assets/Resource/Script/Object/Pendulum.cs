using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    [SerializeField, Header("�U��q�I�u�W�F�N�g")]
    private GameObject pendulumPrefab;
    [SerializeField, Header("�I�t�Z�b�g")]
    public Vector3 offset;
    [SerializeField, Header("�����p�x")]
    public Vector3 initialAngle;

    [SerializeField, Header("��]��������")]
    public Vector3 axis;
    [SerializeField, Header("��]�p�x")]
    public float angle;
    [SerializeField, Header("�X�s�[�h")]
    public float speed;
    [SerializeField, Header("�f�B���C�^�C�}�[")]
    public float delayTimer;

    void Start()
    {
        Instantiate(pendulumPrefab, transform);
        pendulumPrefab.transform.position = offset;
        pendulumPrefab.transform.rotation = Quaternion.Euler(initialAngle);
        transform.Rotate(axis);
    }

    // Update is called once per frame
    void Update()
    {
        delayTimer -= Time.deltaTime;
        if (delayTimer <= 0)
        {
            transform.Rotate(axis * speed * Time.deltaTime);
        }
    }
}
