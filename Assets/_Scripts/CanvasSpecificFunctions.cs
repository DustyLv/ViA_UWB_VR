using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasSpecificFunctions : MonoBehaviour
{
    public TextMeshProUGUI m_ScaleSliderOutputText;
    public Slider m_SliderScale;

    public TextMeshProUGUI m_RotationSliderOutputText;
    public Slider m_SliderRotation;

    void Start()
    {
        InitSliderOutput();
    }

    private void InitSliderOutput()
    {
        if(!m_SliderScale || !m_ScaleSliderOutputText || !m_SliderRotation || !m_RotationSliderOutputText)
        {
            Debug.LogWarning("There is no Slider or TMProUGUI assigned.");
            return;
        }
        m_ScaleSliderOutputText.text = m_SliderScale.value.ToString();
        m_RotationSliderOutputText.text = m_SliderRotation.value.ToString();
    }

    public void UpdateScaleSliderOutput(float _val)
    {
        if (!m_ScaleSliderOutputText)
        {
            Debug.LogWarning("There is no TMProUGUI assigned.");
            return;
        }

        m_ScaleSliderOutputText.text = _val.ToString("0.000");
    }

    public void UpdateRotationSliderOutput(float _val)
    {
        if (!m_RotationSliderOutputText)
        {
            Debug.LogWarning("There is no TMProUGUI assigned.");
            return;
        }

        m_RotationSliderOutputText.text = _val.ToString("0.000");
    }
}
