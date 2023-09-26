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
        // NetworkRunner�𐶐�����
        networkRunner = Instantiate(networkRunnerPrefab);
        // StartGameArgs�ɓn�����ݒ�ŁA�Z�b�V�����ɎQ������
        var result = await networkRunner.StartGame(new StartGameArgs {
            GameMode = GameMode.Shared,
            SceneManager = networkRunner.GetComponent<NetworkSceneManagerDefault>()
        });

        if (result.Ok)
        {
            Debug.Log("�����I");
            // �v���C���[�𐶐�
            networkRunner.Spawn(playerPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity, networkRunner.LocalPlayer);
        }
        else
        {
            Debug.Log("���s�I");
        }
    }
}