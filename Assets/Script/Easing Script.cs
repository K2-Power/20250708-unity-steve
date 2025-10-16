using UnityEngine;

/// <summary>
/// イージング関数のコレクション
/// 各関数は 0-1 の範囲の値を受け取り、イージングを適用した値を返します
/// </summary>
public static class Easings
{
    private const float PI = Mathf.PI;
    private const float C1 = 1.70158f;
    private const float C2 = C1 * 1.525f;
    private const float C3 = C1 + 1f;
    private const float C4 = (2f * PI) / 3f;
    private const float C5 = (2f * PI) / 4.5f;

    // Sine
    public static float InSine(float x)
    {
        return 1f - Mathf.Cos((x * PI) / 2f);
    }

    public static float OutSine(float x)
    {
        return Mathf.Sin((x * PI) / 2f);
    }

    public static float InOutSine(float x)
    {
        return -(Mathf.Cos(PI * x) - 1f) / 2f;
    }

    // Quad
    public static float InQuad(float x)
    {
        return x * x;
    }

    public static float OutQuad(float x)
    {
        return 1f - (1f - x) * (1f - x);
    }

    public static float InOutQuad(float x)
    {
        return x < 0.5f ? 2f * x * x : 1f - Mathf.Pow(-2f * x + 2f, 2f) / 2f;
    }

    // Cubic
    public static float InCubic(float x)
    {
        return x * x * x;
    }

    public static float OutCubic(float x)
    {
        return 1f - Mathf.Pow(1f - x, 3f);
    }

    public static float InOutCubic(float x)
    {
        return x < 0.5f ? 4f * x * x * x : 1f - Mathf.Pow(-2f * x + 2f, 3f) / 2f;
    }

    // Quart
    public static float InQuart(float x)
    {
        return x * x * x * x;
    }

    public static float OutQuart(float x)
    {
        return 1f - Mathf.Pow(1f - x, 4f);
    }

    public static float InOutQuart(float x)
    {
        return x < 0.5f ? 8f * x * x * x * x : 1f - Mathf.Pow(-2f * x + 2f, 4f) / 2f;
    }

    // Quint
    public static float InQuint(float x)
    {
        return x * x * x * x * x;
    }

    public static float OutQuint(float x)
    {
        return 1f - Mathf.Pow(1f - x, 5f);
    }

    public static float InOutQuint(float x)
    {
        return x < 0.5f ? 16f * x * x * x * x * x : 1f - Mathf.Pow(-2f * x + 2f, 5f) / 2f;
    }

    // Expo
    public static float InExpo(float x)
    {
        return x == 0f ? 0f : Mathf.Pow(2f, 10f * x - 10f);
    }

    public static float OutExpo(float x)
    {
        return x == 1f ? 1f : 1f - Mathf.Pow(2f, -10f * x);
    }

    public static float InOutExpo(float x)
    {
        return x == 0f ? 0f : x == 1f ? 1f : x < 0.5f ?
            Mathf.Pow(2f, 20f * x - 10f) / 2f :
            (2f - Mathf.Pow(2f, -20f * x + 10f)) / 2f;
    }

    // Circ
    public static float InCirc(float x)
    {
        return 1f - Mathf.Sqrt(1f - Mathf.Pow(x, 2f));
    }

    public static float OutCirc(float x)
    {
        return Mathf.Sqrt(1f - Mathf.Pow(x - 1f, 2f));
    }

    public static float InOutCirc(float x)
    {
        return x < 0.5f ?
            (1f - Mathf.Sqrt(1f - Mathf.Pow(2f * x, 2f))) / 2f :
            (Mathf.Sqrt(1f - Mathf.Pow(-2f * x + 2f, 2f)) + 1f) / 2f;
    }

    // Back
    public static float InBack(float x)
    {
        return C3 * x * x * x - C1 * x * x;
    }

    public static float OutBack(float x)
    {
        return 1f + C3 * Mathf.Pow(x - 1f, 3f) + C1 * Mathf.Pow(x - 1f, 2f);
    }

    public static float InOutBack(float x)
    {
        return x < 0.5f ?
            (Mathf.Pow(2f * x, 2f) * ((C2 + 1f) * 2f * x - C2)) / 2f :
            (Mathf.Pow(2f * x - 2f, 2f) * ((C2 + 1f) * (x * 2f - 2f) + C2) + 2f) / 2f;
    }

    // Elastic
    public static float InElastic(float x)
    {
        return x == 0f ? 0f : x == 1f ? 1f :
            -Mathf.Pow(2f, 10f * x - 10f) * Mathf.Sin((x * 10f - 10.75f) * C4);
    }

    public static float OutElastic(float x)
    {
        return x == 0f ? 0f : x == 1f ? 1f :
            Mathf.Pow(2f, -10f * x) * Mathf.Sin((x * 10f - 0.75f) * C4) + 1f;
    }

    public static float InOutElastic(float x)
    {
        return x == 0f ? 0f : x == 1f ? 1f : x < 0.5f ?
            -(Mathf.Pow(2f, 20f * x - 10f) * Mathf.Sin((20f * x - 11.125f) * C5)) / 2f :
            (Mathf.Pow(2f, -20f * x + 10f) * Mathf.Sin((20f * x - 11.125f) * C5)) / 2f + 1f;
    }

    // Bounce
    public static float InBounce(float x)
    {
        return 1f - OutBounce(1f - x);
    }

    public static float OutBounce(float x)
    {
        const float n1 = 7.5625f;
        const float d1 = 2.75f;

        if (x < 1f / d1)
        {
            return n1 * x * x;
        }
        else if (x < 2f / d1)
        {
            return n1 * (x -= 1.5f / d1) * x + 0.75f;
        }
        else if (x < 2.5f / d1)
        {
            return n1 * (x -= 2.25f / d1) * x + 0.9375f;
        }
        else
        {
            return n1 * (x -= 2.625f / d1) * x + 0.984375f;
        }
    }

    public static float InOutBounce(float x)
    {
        return x < 0.5f ?
            (1f - OutBounce(1f - 2f * x)) / 2f :
            (1f + OutBounce(2f * x - 1f)) / 2f;
    }

    // Linear
    public static float Linear(float x)
    {
        return x;
    }
}

public class EasingScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
