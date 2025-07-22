using UnityEngine;
using UnityEngine.SceneManagement; // シーンの管理に必要

public class Reset : MonoBehaviour
{
    // Update関数は毎フレーム呼ばれる
    void Update()
    {
        // 「R」キーが押されたらリセット
        if (Input.GetKeyDown("joystick button 3"))
        {
            ResetGame(); // リセット関数を呼び出す
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
