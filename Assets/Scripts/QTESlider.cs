using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QTESlider : MonoBehaviour
{
    [SerializeField]
    private float maxValue = 0.0f;
    [SerializeField]
    private float minValue = 0.0f;
    public float value = 0.0f;

    public float timeSpent = 0;

    public Slider LSlider;
    public Slider RSlider;

    public float MinValue { get => minValue; set => minValue = value; }
    public float MaxValue { get => maxValue; set => maxValue = value; }

    public KeyCode keyPress;
    public TextMeshProUGUI txtMesh;

    // Start is called before the first frame update
    void Start()
    {
        txtMesh.SetText(keyPress.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        timeSpent+=Time.deltaTime;
        SetValue(timeSpent);
    }

    public void SetKey(KeyCode kp)
    {
        keyPress = kp;
        txtMesh.SetText(keyPress.ToString());
    }

    public void SetMaxValue(float maxV)
    {
        LSlider.maxValue = maxV;
        RSlider.maxValue = maxV;
    }

    public void SetMinValue(float maxV)
    {
        LSlider.minValue = maxV;
        RSlider.minValue = maxV;
    }
    public void SetValue(float maxV)
    {
        LSlider.value = maxV;
        RSlider.value = maxV;
    }


    public void OnValidate()
    {
        SetMaxValue(maxValue);

        SetMinValue(minValue);

        SetValue(value);

        if (maxValue < minValue)
        {
            maxValue = minValue;
        }

        if (maxValue < value)
        {
            value = maxValue;
        }

        if (minValue > value)
        {
            value = minValue;
        }
    }
}
