using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DoorFactory : MonoBehaviour
{
    // インスタンス
    public static DoorFactory Instance { get; private set; }

    [SerializeField, Header("通れるドア")]
    private GameObject doorPrefab;
    [SerializeField, Header("通れないドア")]
    private GameObject fakeDoorPrefab;

    [Serializable]
    private class DoorTable
    {
        [SerializeField, Header("配置する位置")]
        public Vector3 position;

        [SerializeField, Header("配置するドアの間隔")]
        public float offset;

        [SerializeField, Header("配置する全ドアの個数")]
        public int allCount;

        [SerializeField, Header("通れるドアの個数")]
        public int doorCount;
    }

    [SerializeField, Header("ドアの配置・設定")]
    private DoorTable[] doorTableArray;

    // 開始
    void Start()
    {
        Instance = this;
    }

    // 更新
    void Update()
    {
        
    }

    // ドアをセットする
    private void SetDoor()
    {
        for (int i = 0; i < doorTableArray.Length; ++i)
        {
            // 生成位置
            Vector3 pos = doorTableArray[i].position;

            for (int j = 0; j < doorTableArray[i].allCount; ++j)
            {
                // 間隔をあける
                pos.z += j * doorTableArray[i].offset;

                // 配置するドア番号を取得
                int[] doorNum = SetDoorNumber(doorTableArray[i].doorCount, doorTableArray[i].allCount);

                


                // 生成する
                Instantiate(doorPrefab, pos, Quaternion.identity);
            }
        }
    }

    // 配置するドア番号を取得
    private int[] SetDoorNumber(int count, int allCount)
    {
        // 配置する番号
        int[] result = new int[count];

        // 配置できる番号のリスト
        List<int> num = new List<int>();
        // 番号のリストを生成
        for (int i = 0; i < allCount; ++i)
        {
            num.Add(i);
        }

        // 配置番号を決定する
        for (int i = 0; i < count;)
        {
            // ランダムで配列から番号を取得
            int random = UnityEngine.Random.Range(0, num.Count);
            result[i] = random;
            // 選んだものを削除
            num.Remove(random);
        }
        return result;
    }
}
