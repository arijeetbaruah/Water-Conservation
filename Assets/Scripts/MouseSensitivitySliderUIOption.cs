using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MouseSensitivitySliderUIOption : MonoBehaviour
{
    public TextMeshProUGUI value;
    public Slider slider;

    FirstPersonAIO player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<FirstPersonAIO>();

        slider.value = player.mouseSensitivity;
        value.SetText(player.mouseSensitivity.ToString());
    }

    public void HandleChange()
    {
        player.mouseSensitivity = slider.value;
        value.SetText(slider.value.ToString());
    }
}
