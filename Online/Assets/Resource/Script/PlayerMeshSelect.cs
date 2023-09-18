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

    public bool isBodyParts;
    public bool isGloves;
    public bool isHeadParts;
    public bool isTails;

    void Start()
    {
        // パーツのスクリプトをセット
        SetPartsScript();
        // パーツを探す
        FindParts();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) bodies.OwnUpdate();
        if (Input.GetKeyDown(KeyCode.Alpha2)) eyes.OwnUpdate();
        if (Input.GetKeyDown(KeyCode.Alpha3)) mouthAndNoses.OwnUpdate();

        if (Input.GetKeyDown(KeyCode.Alpha4)) bodyParts.OwnUpdate();
        if (Input.GetKeyDown(KeyCode.Alpha5)) gloves.OwnUpdate();
        if (Input.GetKeyDown(KeyCode.Alpha6)) headParts.OwnUpdate();
        if (Input.GetKeyDown(KeyCode.Alpha7)) tails.OwnUpdate();

        bodyParts.SetEnable(isBodyParts);
        gloves.SetEnable(isGloves);
        headParts.SetEnable(isHeadParts);
        tails.SetEnable(isTails);
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
