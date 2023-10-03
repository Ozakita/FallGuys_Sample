using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public struct CharacterData
    {
        // プレイヤーネーム
        public static string PlayerName
        {
            get => PlayerPrefs.GetString("PlayerName", "No Name");
            set => PlayerPrefs.SetString("PlayerName", value);
        }

        // チーム番号
        // NOTE:チーム戦が存在する場合に使用する想定
        public static int teamNum;

        // 以下見た目のナンバー
        public static int[] meshPartsNum = new int[7];
    }
}
