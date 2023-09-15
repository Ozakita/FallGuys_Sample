using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    // �C���X�^���X�i�������Ȃ��߁A�g��Ȃ��j
    //public static CharacterManager Instance { get; private set; }

    // �v���C���[
    public PlayerCharacter playerCharacter;

    // �L�����N�^�[���X�g
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

    // �J�n
    public void CharaStart()
    {
        // �v���C���[�𐶐�����
        GeneratePlayer();

        // �L�����N�^�[�̊J�n����
        foreach (CharacterBase c in characterList)
        {
            c.CharaStart();
        }
    }

    // �X�V
    public void CharaUpdate()
    {
        // �L�����N�^�[�̍X�V����
        foreach (CharacterBase c in characterList)
        {
            c.CharaUpdate();
        }
    }

    // �v���C���[�𐶐�����
    public PlayerCharacter GeneratePlayer()
    {
        // ��������
        PlayerCharacter player = Instantiate(playerCharacter, new Vector3(0f, 0f, 0f), Quaternion.identity);
        // ���X�g�ɒǉ�
        characterList.Add(player);

        return player;
    }

    // �L�����N�^�[�����擾
    public int CharacterCount()
    {
        return characterList.Count;
    }
}
