using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushManagerScript : MonoBehaviour
{
    // 生成者のPhotonPlayer
    public PhotonPlayer player;
    // 判定が消えるまでの時間(秒)
    public float destroyTime = 2f;

    void Start()
    {
        // 時間が経過したらオブジェクトを破壊する
        Destroy(this.gameObject, destroyTime);
    }
}
