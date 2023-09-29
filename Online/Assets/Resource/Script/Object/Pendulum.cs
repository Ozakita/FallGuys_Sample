using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    [SerializeField, Header("振り子オブジェクト")]
    private GameObject pendulumPrefab;
    [SerializeField, Header("オフセット")]
    public Vector3 offset;
    [SerializeField, Header("初期角度")]
    public Vector3 initialAngle;

    [SerializeField, Header("回転したい軸")]
    public Vector3 axis;
    [SerializeField, Header("回転角度")]
    public float angle;
    [SerializeField, Header("スピード")]
    public float speed;
    [SerializeField, Header("ディレイタイマー")]
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
