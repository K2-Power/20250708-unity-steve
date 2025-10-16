using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TitlemenuScript : MonoBehaviour
{
    [SerializeField] private Transform targetObject;
    [SerializeField] private float duration = 1f;
    public bool menuopen = false;
    public UnityEngine.UI.Button button;
    public UnityEngine.UI.Button button1;
    public GameObject menubutton;
    public GameObject menubutton1;
    private RectTransform RectTransform_button;
    private RectTransform RectTransform_button1;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        RectTransform_button = button.GetComponent<RectTransform>();
        RectTransform_button1 = button1.GetComponent<RectTransform>();
        // 使用例
        // 位置移動の例
        // StartCoroutine(MovePosition(transform.position, transform.position + Vector3.right * 5f));

        // スケール変化の例
        // StartCoroutine(ScaleObject(Vector3.one, Vector3.one * 2f));

        // 数値アニメーションの例（スコア表示など）
        // StartCoroutine(AnimateValue(0f, 100f, value => Debug.Log($"Score: {value:F0}")));
        //Wait3Seconds();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void QuitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
        #else
            Application.Quit();//ゲームプレイ終了
        #endif
    }
    public void StringArgFunction(string s)
    {
        
        SceneManager.LoadScene(s);
    }
    private void Wait3Seconds()
    { 
        Vector2 targetpos = Vector2.zero;
        Vector2 buttonpos;
        buttonpos.x = 1320.0f;
        buttonpos.y = 0.0f;
        StartCoroutine(MoveUIElement(RectTransform_button, buttonpos, targetpos));
    }

    // ========================================
    // 1. 位置の移動
    // ========================================
    public IEnumerator MovePosition(Vector3 start, Vector3 end)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Easings.OutCubic(elapsed / duration);
            targetObject.position = Vector3.Lerp(start, end, t);
            yield return null;
        }
    }

    // ========================================
    // 2. 回転
    // ========================================
    public IEnumerator RotateObject(Quaternion start, Quaternion end)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Easings.InOutQuad(elapsed / duration);
            targetObject.rotation = Quaternion.Lerp(start, end, t);
            yield return null;
        }
    }

    // ========================================
    // 3. スケール（拡大・縮小）
    // ========================================
    public IEnumerator ScaleObject(Vector3 start, Vector3 end)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Easings.OutBack(elapsed / duration);
            targetObject.localScale = Vector3.Lerp(start, end, t);
            yield return null;
        }
    }

    // ========================================
    // 4. 色の変化
    // ========================================
    public IEnumerator ChangeColor(Color start, Color end)
    {
        Renderer renderer = targetObject.GetComponent<Renderer>();
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Easings.InOutSine(elapsed / duration);
            renderer.material.color = Color.Lerp(start, end, t);
            yield return null;
        }
    }

    // ========================================
    // 5. 透明度の変化（フェードイン・フェードアウト）
    // ========================================
    public IEnumerator FadeAlpha(float start, float end)
    {
        Renderer renderer = targetObject.GetComponent<Renderer>();
        Color color = renderer.material.color;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Easings.InOutQuad(elapsed / duration);
            color.a = Mathf.Lerp(start, end, t);
            renderer.material.color = color;
            yield return null;
        }
    }

    // ========================================
    // 6. UI要素の移動（Canvas）
    // ========================================
    public IEnumerator MoveUIElement(RectTransform rectTransform, Vector2 start, Vector2 end)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Easings.OutBounce(elapsed / duration);
            rectTransform.anchoredPosition = Vector2.Lerp(start, end, t);
            yield return null;
        }
    }

    // ========================================
    // 7. カメラのズーム（FOV変更）
    // ========================================
    public IEnumerator ZoomCamera(Camera cam, float startFOV, float endFOV)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Easings.InOutCubic(elapsed / duration);
            cam.fieldOfView = Mathf.Lerp(startFOV, endFOV, t);
            yield return null;
        }
    }

    // ========================================
    // 8. 数値の変化（スコア表示など）
    // ========================================
    public IEnumerator AnimateValue(float start, float end, System.Action<float> onUpdate)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Easings.OutQuart(elapsed / duration);
            float value = Mathf.Lerp(start, end, t);
            onUpdate?.Invoke(value);
            yield return null;
        }
    }

    // ========================================
    // 9. ライトの強度変化
    // ========================================
    public IEnumerator ChangeLightIntensity(Light light, float start, float end)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Easings.InOutSine(elapsed / duration);
            light.intensity = Mathf.Lerp(start, end, t);
            yield return null;
        }
    }

    // ========================================
    // 10. オーディオのボリューム変化
    // ========================================
    public IEnumerator ChangeAudioVolume(AudioSource audioSource, float start, float end)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Easings.InOutQuad(elapsed / duration);
            audioSource.volume = Mathf.Lerp(start, end, t);
            yield return null;
        }
    }

    // ========================================
    // 11. パーティクルシステムのサイズ変化
    // ========================================
    public IEnumerator ChangeParticleSize(ParticleSystem ps, float start, float end)
    {
        var main = ps.main;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Easings.OutBounce(elapsed / duration);
            main.startSize = Mathf.Lerp(start, end, t);
            yield return null;
        }
    }

    // ========================================
    // 12. マテリアルのプロパティ変化
    // ========================================
    public IEnumerator ChangeMaterialProperty(Material material, string propertyName, float start, float end)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Easings.InOutCubic(elapsed / duration);
            material.SetFloat(propertyName, Mathf.Lerp(start, end, t));
            yield return null;
        }
    }

    // ========================================
    // 13. 曲線的な移動（ベジェ曲線風）
    // ========================================
    public IEnumerator MoveCurved(Vector3 start, Vector3 end, Vector3 controlPoint)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Easings.InOutQuad(elapsed / duration);

            // 二次ベジェ曲線
            Vector3 p0 = Vector3.Lerp(start, controlPoint, t);
            Vector3 p1 = Vector3.Lerp(controlPoint, end, t);
            targetObject.position = Vector3.Lerp(p0, p1, t);

            yield return null;
        }
    }

    // ========================================
    // 14. ジャンプ動作
    // ========================================
    public IEnumerator Jump(Vector3 startPos, Vector3 endPos, float jumpHeight)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            // 水平移動
            float horizontalT = Easings.Linear(t);
            Vector3 currentPos = Vector3.Lerp(startPos, endPos, horizontalT);

            // 垂直移動（放物線）
            float verticalT = Easings.OutQuad(t);
            float height = jumpHeight * Mathf.Sin(Mathf.PI * verticalT);
            currentPos.y += height;

            targetObject.position = currentPos;
            yield return null;
        }
    }

    // ========================================
    // 15. シェイク効果
    // ========================================
    public IEnumerator Shake(float intensity)
    {
        Vector3 originalPos = targetObject.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            // 強度を徐々に減衰
            float strength = intensity * Easings.OutExpo(1f - t);

            Vector3 offset = new Vector3(
                Random.Range(-1f, 1f) * strength,
                Random.Range(-1f, 1f) * strength,
                0f
            );

            targetObject.position = originalPos + offset;
            yield return null;
        }

        targetObject.position = originalPos;
    }

    // ========================================
    // 16. パルス効果（拡大縮小の繰り返し）
    // ========================================
    public IEnumerator Pulse(float minScale, float maxScale)
    {
        Vector3 originalScale = targetObject.localScale;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            // 0→1→0と変化
            float pulse = Mathf.Sin(t * Mathf.PI);
            float easedPulse = Easings.InOutQuad(pulse);

            float scale = Mathf.Lerp(minScale, maxScale, easedPulse);
            targetObject.localScale = originalScale * scale;

            yield return null;
        }

        targetObject.localScale = originalScale;
    }
}
