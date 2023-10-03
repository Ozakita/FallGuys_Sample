using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMeshSelect : MonoBehaviour
{
    // 必須のメッシュパーツ
    private RequiredMeshParts bodies;
    private RequiredMeshParts eyes;
    private RequiredMeshParts mouthAndNoses;

    // 任意のメッシュパーツ
    private OptionalMeshParts bodyParts;
    private OptionalMeshParts gloves;
    private OptionalMeshParts headParts;
    private OptionalMeshParts tails;

    void Start() {}
    void Update() {}

    // 開始処理
    public void OwnStart()
    {
        // パーツのスクリプトをセット
        SetPartsScript();
        // パーツを探す
        FindParts();

        // メニューでのアバター設定を反映
        bodies.OwnUpdate(PlayerData.CharacterData.meshPartsNum[0]);
    }

    // 更新処理
    public void OwnUpdate()
    {
        //bodies.OwnUpdate();
    }

    // パーツのスクリプトをセット
    private void SetPartsScript()
    {
        bodies = new RequiredMeshParts();
        eyes = new RequiredMeshParts();
        mouthAndNoses = new RequiredMeshParts();

        bodyParts = new OptionalMeshParts();
        gloves = new OptionalMeshParts();
        headParts = new OptionalMeshParts();
        tails = new OptionalMeshParts();
    }

    // パーツを探す
    private void FindParts()
    {
        bodies.parts = transform.Find("Bodies").gameObject;
        eyes.parts = transform.Find("Eyes").gameObject;
        mouthAndNoses.parts = transform.Find("MouthandNoses").gameObject;

        bodyParts.parts = transform.Find("Bodyparts").gameObject;
        gloves.parts = transform.Find("Gloves").gameObject;
        headParts.parts = transform.Find("Headparts").gameObject;
        tails.parts = transform.Find("Tails").gameObject;
    }
}
