using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsNumSetter : MonoBehaviour
{

    private int settingPartsNum;

    public void SetPartsNum(int value)
    {
        settingPartsNum = value;
    }

    public void NumCountUp()
    {
        ++PlayerData.CharacterData.meshPartsNum[settingPartsNum];
    }
    public void NumCountDown()
    {
        --PlayerData.CharacterData.meshPartsNum[settingPartsNum];
    }
}
