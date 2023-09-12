using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    // インスタンス（未完成なため、使わない）
    //public static CharacterManager Instance { get; private set; }

    // プレイヤー
    public PlayerCharacter playerCharacter;

    // キャラクターリスト
    private List<CharacterBase> characterList = new List<CharacterBase>();

    void Start()
    {
        //Instance = this;

        CharaStart();
    }

    void Update()
    {
        CharaUpdate();
    }

    // 開始
    public void CharaStart()
    {
        // プレイヤーを生成する
        GeneratePlayer();

        // キャラクターの開始処理
        foreach (CharacterBase c in characterList)
        {
            c.CharaStart();
        }
    }

    // 更新
    public void CharaUpdate()
    {
        // キャラクターの更新処理
        foreach (CharacterBase c in characterList)
        {
            c.CharaUpdate();
        }
    }

    // プレイヤーを生成する
    public PlayerCharacter GeneratePlayer()
    {
        // 生成する
        PlayerCharacter player = Instantiate(playerCharacter, new Vector3(0f, 0f, 0f), Quaternion.identity);
        // リストに追加
        characterList.Add(player);

        return player;
    }

    // キャラクター数を取得
    public int CharacterCount()
    {
        return characterList.Count;
    }
}
