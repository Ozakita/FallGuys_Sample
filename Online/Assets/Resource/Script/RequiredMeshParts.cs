using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class RequiredMeshParts : MonoBehaviour
{
    // パーツ
    public GameObject parts;
    // 使用するインデックス
    public int useIndex = 0;

    void Start(){}

    void Update(){}

    // 自身の開始
    public void OwnStart(int index)
    {
        // 使用パーツを設定
        useIndex = index;
        // パーツを変更
        ChangeParts();
    }

    // 自身の更新
    // OPTIMIZE:入力なしを示すvalueのデフォルト値の管理方法
    //          ここに記載？変数を用意すべき？
    public void OwnUpdate(int value = -999)
    {
        // 値の入力があればその値にする
        if (value != -999)
        {
            useIndex = value;
        }
        else
        {
            // 次のパーツ、カウントアップ
            useIndex++;
        }
        // 次のパーツが範囲外なら初期化
        if (useIndex >= PartsCount()) 
            useIndex = 0;

        // パーツを変更
        ChangeParts();
    }

    // パーツを変更
    private void ChangeParts()
    {
        Assert.IsTrue(IsRange(), "パーツの配列範囲外を指定しています。");

        // パーツ変更処理
        for (int i = 0; i < PartsCount(); ++i)
        {
            // 個々のパーツを取得
            GameObject child = parts.transform.GetChild(i).gameObject;
            // 有効か無効か
            bool isActive = (i == useIndex) ? true : false;
            // アクティブ設定
            child.SetActive(isActive);
        }
    }

    // パーツの個数を取得
    private int PartsCount()
    {
        return parts.transform.childCount;
    }

    // 範囲内かどうか？
    private bool IsRange()
    {
        if (useIndex < 0)
            return false;
        if (useIndex >= PartsCount())
            return false;
        return true;
    }
}
