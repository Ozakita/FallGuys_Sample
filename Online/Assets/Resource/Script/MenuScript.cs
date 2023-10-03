using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuScript : MonoBehaviour
{
    // ボディーカラーを表示するテキスト
    [SerializeField]
    private TextMeshProUGUI bodyColor;
    // 名前の変更
    public void SetName(string value)
    {
        PlayerData.CharacterData.PlayerName = value;
    }

    // ボディーカラーの変更
    // TODO:値の増減や限界値の設定、他カスタマイズ箇所への対応
    public void SetBodyColor()
    {
        PlayerData.CharacterData.meshPartsNum[0]++;
        bodyColor.text = PlayerData.CharacterData.meshPartsNum[0].ToString();
    }

    // 次Sceneへ移動する
    public void PushNextButton()
    {
        SceneManager.LoadScene("OnlineTest");
    }
}

