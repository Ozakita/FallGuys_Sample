using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public struct CharacterData
    {
        // �v���C���[�l�[��
        public static string PlayerName
        {
            get => PlayerPrefs.GetString("PlayerName", "No Name");
            set => PlayerPrefs.SetString("PlayerName", value);
        }

        // �`�[���ԍ�
        // NOTE:�`�[���킪���݂���ꍇ�Ɏg�p����z��
        public static int teamNum;

        // �ȉ������ڂ̃i���o�[
        public static int[] meshPartsNum = new int[7];
    }
}
