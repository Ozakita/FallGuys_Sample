using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushManagerScript : MonoBehaviour
{
    // �����҂�PhotonPlayer
    public PhotonPlayer player;
    // ���肪������܂ł̎���(�b)
    public float destroyTime = 2f;

    void Start()
    {
        // ���Ԃ��o�߂�����I�u�W�F�N�g��j�󂷂�
        Destroy(this.gameObject, destroyTime);
    }
}
