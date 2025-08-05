using UnityEngine;
using UnityEngine.UI;

public class CloneCountScript : MonoBehaviour
{
    private Image image;
    public Sprite[] numbers;
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        image.sprite = numbers[Player.instance.CloneCount];
    }
}
