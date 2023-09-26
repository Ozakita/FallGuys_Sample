using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControl : MonoBehaviour
{
    // インスタンス
    //public static InputControl Instance { get; private set; }

    // 開始
    void Start()
    {
        //Instance = this;
    }

    // 更新
    void Update() {}

    // ジャンプ
    public bool IsJump()
    {
        return Input.GetKeyDown(KeyCode.Space) 
            || Input.GetKeyDown(KeyCode.JoystickButton0);
    }

    // スライディング
    public bool IsSlide()
    {
        return Input.GetKeyDown(KeyCode.LeftShift)
            || Input.GetKeyDown(KeyCode.JoystickButton2);
    }

    // プッシュ
    public bool IsPush()
    {
        return Input.GetKeyDown(KeyCode.F)
            || Input.GetKeyDown(KeyCode.JoystickButton5);
    }
}
