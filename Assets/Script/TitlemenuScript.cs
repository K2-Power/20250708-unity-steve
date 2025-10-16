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
        // �g�p��
        // �ʒu�ړ��̗�
        // StartCoroutine(MovePosition(transform.position, transform.position + Vector3.right * 5f));

        // �X�P�[���ω��̗�
        // StartCoroutine(ScaleObject(Vector3.one, Vector3.one * 2f));

        // ���l�A�j���[�V�����̗�i�X�R�A�\���Ȃǁj
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
                UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
        #else
            Application.Quit();//�Q�[���v���C�I��
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
    // 1. �ʒu�̈ړ�
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
    // 2. ��]
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
    // 3. �X�P�[���i�g��E�k���j
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
    // 4. �F�̕ω�
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
    // 5. �����x�̕ω��i�t�F�[�h�C���E�t�F�[�h�A�E�g�j
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
    // 6. UI�v�f�̈ړ��iCanvas�j
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
    // 7. �J�����̃Y�[���iFOV�ύX�j
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
    // 8. ���l�̕ω��i�X�R�A�\���Ȃǁj
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
    // 9. ���C�g�̋��x�ω�
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
    // 10. �I�[�f�B�I�̃{�����[���ω�
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
    // 11. �p�[�e�B�N���V�X�e���̃T�C�Y�ω�
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
    // 12. �}�e���A���̃v���p�e�B�ω�
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
    // 13. �Ȑ��I�Ȉړ��i�x�W�F�Ȑ����j
    // ========================================
    public IEnumerator MoveCurved(Vector3 start, Vector3 end, Vector3 controlPoint)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Easings.InOutQuad(elapsed / duration);

            // �񎟃x�W�F�Ȑ�
            Vector3 p0 = Vector3.Lerp(start, controlPoint, t);
            Vector3 p1 = Vector3.Lerp(controlPoint, end, t);
            targetObject.position = Vector3.Lerp(p0, p1, t);

            yield return null;
        }
    }

    // ========================================
    // 14. �W�����v����
    // ========================================
    public IEnumerator Jump(Vector3 startPos, Vector3 endPos, float jumpHeight)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            // �����ړ�
            float horizontalT = Easings.Linear(t);
            Vector3 currentPos = Vector3.Lerp(startPos, endPos, horizontalT);

            // �����ړ��i�������j
            float verticalT = Easings.OutQuad(t);
            float height = jumpHeight * Mathf.Sin(Mathf.PI * verticalT);
            currentPos.y += height;

            targetObject.position = currentPos;
            yield return null;
        }
    }

    // ========================================
    // 15. �V�F�C�N����
    // ========================================
    public IEnumerator Shake(float intensity)
    {
        Vector3 originalPos = targetObject.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            // ���x�����X�Ɍ���
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
    // 16. �p���X���ʁi�g��k���̌J��Ԃ��j
    // ========================================
    public IEnumerator Pulse(float minScale, float maxScale)
    {
        Vector3 originalScale = targetObject.localScale;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            // 0��1��0�ƕω�
            float pulse = Mathf.Sin(t * Mathf.PI);
            float easedPulse = Easings.InOutQuad(pulse);

            float scale = Mathf.Lerp(minScale, maxScale, easedPulse);
            targetObject.localScale = originalScale * scale;

            yield return null;
        }

        targetObject.localScale = originalScale;
    }
}
