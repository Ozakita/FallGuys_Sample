using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuScript : MonoBehaviour
{
    // �{�f�B�[�J���[��\������e�L�X�g
    [SerializeField]
    private TextMeshProUGUI bodyColor;
    // ���O�̕ύX
    public void SetName(string value)
    {
        PlayerData.CharacterData.PlayerName = value;
    }

    // �{�f�B�[�J���[�̕ύX
    // TODO:�l�̑�������E�l�̐ݒ�A���J�X�^�}�C�Y�ӏ��ւ̑Ή�
    public void SetBodyColor()
    {
        PlayerData.CharacterData.meshPartsNum[0]++;
        bodyColor.text = PlayerData.CharacterData.meshPartsNum[0].ToString();
    }

    // ��Scene�ֈړ�����
    public void PushNextButton()
    {
        SceneManager.LoadScene("OnlineTest");
    }
}

