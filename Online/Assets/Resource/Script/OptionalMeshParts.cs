using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class OptionalMeshParts : MonoBehaviour
{
    // 使用するか？
    private bool isEnable = false;
    // パーツ
    public GameObject parts;
    // 使用するインデックス
    public int useIndex = 0;

    void Start() {}

    void Update() {}

    // 自身の開始
    public void OwnStart(int index, bool enable)
    {
        // 使用パーツを設定
        useIndex = index;
        // 使用するか設定
        SetEnable(enable);
    }

    // 自身の更新
    public void OwnUpdate()
    {
        // 次のパーツ
        useIndex++;
        // 次のパーツが範囲外なら初期化
        if (useIndex >= PartsCount())
            useIndex = 0;

        // パーツを変更
        ChangeParts();
    }

    // 使用するか設定する
    public void SetEnable(bool enable)
    {
        isEnable = enable;
        // 使用しない場合はスキップする
        if (!isEnable)
            return;
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
            bool isActive = (!isEnable) ? false : (i != useIndex) ? false : true;
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
