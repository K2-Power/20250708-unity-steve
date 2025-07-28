using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement; // シーンの管理に必要

public class Reset : MonoBehaviour
{
    public GameObject ResetText;

    void Start()
    {
        ResetText.SetActive(false);
    }
    // Update関数は毎フレーム呼ばれる
    void Update()
    {
        if (Player.instance.CloneCount <= 0)
        {
            ResetText.SetActive(true);
        }
        // 「R」キーが押されたらリセット
        if (Input.GetKeyDown("joystick button 3"))
        {
            ResetGame(); // リセット関数を呼び出す
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            ResetGame();
        }
    }

    // ゲームのリセット処理（シーンの再読み込み）
    void ResetGame()
    {
        // 現在のアクティブなシーンを取得
        Scene currentScene = SceneManager.GetActiveScene();

        // そのシーンを再読み込みしてリセット
        SceneManager.LoadScene(currentScene.name);
    }
}
