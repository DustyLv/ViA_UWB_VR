using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasSpecificFunctions : MonoBehaviour
{
    public TextMeshProUGUI m_SliderOutputText;
    public Slider m_SliderScale;

    void Start()
    {
        InitSliderOutput();
    }

    private void InitSliderOutput()
    {
        if(!m_SliderScale || !m_SliderOutputText)
        {
            Debug.LogWarning("There is no Slider or TMProUGUI assigned.");
            return;
        }
        m_SliderOutputText.text = m_SliderScale.value.ToString();
    }

    public void UpdateSliderOutput(float _val)
    {
        if (!m_SliderOutputText)
        {
            Debug.LogWarning("There is no TMProUGUI assigned.");
            return;
        }

        m_SliderOutputText.text = _val.ToString("0.000");
    }
}
