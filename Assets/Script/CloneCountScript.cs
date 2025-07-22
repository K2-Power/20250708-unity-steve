using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CloneCountScript : MonoBehaviour
{
    public TextMeshProUGUI CountText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CountText.text = Player.instance.CloneCount.ToString();
    }
}
