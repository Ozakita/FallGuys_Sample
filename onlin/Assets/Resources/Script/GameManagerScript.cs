using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : Photon.PunBehaviour
{
    //�N�������O�C������x�ɐ�������v���C���[Prefab
    public GameObject playerPrefab;

    void Start()
    {
        if (!PhotonNetwork.connected)   //Phootn�ɐڑ�����Ă��Ȃ����
        {
            SceneManager.LoadScene("Login"); //���O�C����ʂɖ߂�
            return;
        }
        //Photon�ɐڑ����Ă���Ύ��v���C���[�𐶐�
        GameObject Player = PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(36.0f, 0f, 28.0f), Quaternion.identity, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
