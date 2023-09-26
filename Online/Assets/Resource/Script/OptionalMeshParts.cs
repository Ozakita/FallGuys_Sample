using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class OptionalMeshParts : MonoBehaviour
{
    // パーツ
    public GameObject parts;
    // 使用するインデックス
    public int useIndex = -1;

    void Start() {}

    void Update() {}

    // 自身の開始
    public void OwnStart(int index)
    {
        // 使用パーツを設定
        useIndex = index;
        // パーツを変更
        ChangeParts(IsEnable());
    }

    // 自身の更新
    public void OwnUpdate()
    {
        // 次のパーツ
        useIndex++;
        // 次のパーツが範囲外なら初期化
        if (useIndex >= PartsCount())
            useIndex = -1;

        // パーツを変更
        ChangeParts(IsEnable());
    }

    // パーツを変更
    private void ChangeParts(bool isEnable)
    {
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

    // 有効にするか？
    private bool IsEnable()
    {
        if (useIndex < 0)
            return false;
        if (useIndex >= PartsCount())
            return false;
        return true;
    }
}
