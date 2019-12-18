using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIKeyboardVR : MonoBehaviour
{
    public enum ButtonName { Comma, Num0, Num1, Num2, Num3, Num4, Num5, Num6, Num7, Num8, Num9, Enter, Backspace};

    private TMP_InputField targetinputField;
    //public UWBCanvasToggler canvasToggler;
    private CanvasGroup m_canvasGroup;

    void Start()
    {
        //canvasToggler = GetComponent<UWBCanvasToggler>();
        m_canvasGroup = GetComponent<CanvasGroup>();

        m_canvasGroup.alpha = 0f;
        m_canvasGroup.interactable = false;
        m_canvasGroup.blocksRaycasts = false;
    }

    public void ActivateKeyboard(TMP_InputField iField)
    {
        m_canvasGroup.DOFade(1f, 0.2f).OnComplete(() =>
        {
            m_canvasGroup.interactable = true;
            m_canvasGroup.blocksRaycasts = true;
            targetinputField = iField;
        });
    }

    public void DisableKeyboard()
    {
        m_canvasGroup.DOFade(0f, 0.2f).OnComplete(() =>
        {
            m_canvasGroup.interactable = false;
            m_canvasGroup.blocksRaycasts = false;
            targetinputField = null;
        });
    }

    public void TypeToInputField(ButtonName b)
    {
        string editInputFieldText = targetinputField.text;
        string textToAppend = "";
        switch (b)
        {
            case ButtonName.Num0:
                textToAppend = "0";
                break;
            case ButtonName.Num1:
                textToAppend = "1";
                break;
            case ButtonName.Num2:
                textToAppend = "2";
                break;
            case ButtonName.Num3:
                textToAppend = "3";
                break;
            case ButtonName.Num4:
                textToAppend = "4";
                break;
            case ButtonName.Num5:
                textToAppend = "5";
                break;
            case ButtonName.Num6:
                textToAppend = "6";
                break;
            case ButtonName.Num7:
                textToAppend = "7";
                break;
            case ButtonName.Num8:
                textToAppend = "8";
                break;
            case ButtonName.Num9:
                textToAppend = "9";
                break;
            case ButtonName.Comma:
                textToAppend = ".";
                break;
        }
        editInputFieldText += textToAppend;
        targetinputField.text = editInputFieldText;
    }

    public void TypeToInputField(string b)
    {
        string editInputFieldText = targetinputField.text;
        string textToAppend = b;
        
        editInputFieldText += textToAppend;
        targetinputField.text = editInputFieldText;
    }

    public void RemoveCharacter()
    {
        string editInputFieldText = targetinputField.text;
        if(editInputFieldText.Length > 0)
            editInputFieldText = editInputFieldText.Substring(0, editInputFieldText.Length - 1);
        targetinputField.text = editInputFieldText;
    }

    public void AcceptInput()
    {
        targetinputField.onEndEdit.Invoke(targetinputField.text);
        //canvasToggler.ToggleCanvas(false);
    }
}
