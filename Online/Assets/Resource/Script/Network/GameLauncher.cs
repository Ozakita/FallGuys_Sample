using Fusion;
using UnityEngine;

public class GameLauncher : MonoBehaviour
{
    [SerializeField]
    private NetworkRunner networkRunnerPrefab;
    [SerializeField]
    private NetworkPrefabRef playerPrefab;

    private NetworkRunner networkRunner;

    private async void Start()
    {
        // NetworkRunnerを生成する
        networkRunner = Instantiate(networkRunnerPrefab);
        // StartGameArgsに渡した設定で、セッションに参加する
        var result = await networkRunner.StartGame(new StartGameArgs {
            GameMode = GameMode.Shared,
            SceneManager = networkRunner.GetComponent<NetworkSceneManagerDefault>()
        });

        if (result.Ok)
        {
            Debug.Log("成功！");
            // プレイヤーを生成
            networkRunner.Spawn(playerPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity, networkRunner.LocalPlayer);
        }
        else
        {
            Debug.Log("失敗！");
        }
    }
}