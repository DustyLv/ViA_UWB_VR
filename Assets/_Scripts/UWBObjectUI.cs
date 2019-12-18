using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using Valve.VR;
//using Valve.VR.InteractionSystem;

public class UWBObjectUI : MonoBehaviour
{
    [Header("Position Invert Objects")]
    public Toggle toggle_invertX;
    public Toggle toggle_invertY;
    public Toggle toggle_invertZ;

    [Header("Position Override Objects")]
    public TMP_InputField inputField_X;
    public Toggle inputFieldToggle_X;
    [Space(5)]
    public TMP_InputField inputField_Y;
    public Toggle inputFieldToggle_Y;
    [Space(5)]
    public TMP_InputField inputField_Z;
    public Toggle inputFieldToggle_Z;

    [Header("Other Objects")]
    public UWBObject uwbObject;
    public UIKeyboardVR keyboard;
    //public Player vrPlayer;

    void Start()
    {
        keyboard = GameObject.FindObjectOfType<UIKeyboardVR>();
        //vrPlayer = GameObject.FindObjectOfType<Player>();
        InitInvertToggles();
        InitOverrideFields();

    }

    public void ActivateKeyboard(TMP_InputField field)
    {
        //Vector3 fieldPos = vrPlayer.rightHand.transform.position;

        //keyboard.ActivateKeyboard(field, fieldPos);
    }

    #region // Position Inversion //
    void InitInvertToggles()
    {
        toggle_invertX.isOn = uwbObject.InvertPosX;
        toggle_invertY.isOn = uwbObject.InvertPosY;
        toggle_invertZ.isOn = uwbObject.InvertPosZ;
    }

    public void ToggleInvertX(bool val)
    {
        uwbObject.InvertPosX = val;
    }

    public void ToggleInvertY(bool val)
    {
        uwbObject.InvertPosY = val;
    }

    public void ToggleInvertZ(bool val)
    {
        uwbObject.InvertPosZ = val;

    }
    #endregion

    #region // Position Override //
    void InitOverrideFields()
    {
        inputField_X.interactable = inputFieldToggle_X.isOn;
        inputField_Y.interactable = inputFieldToggle_Y.isOn;
        inputField_Z.interactable = inputFieldToggle_Z.isOn;
    }

    public void ToggleInputField_X(bool val)
    {
        inputField_X.interactable = val;
        uwbObject.overridePosX = inputFieldToggle_X.isOn;
    }
    public void ToggleInputField_Y(bool val)
    {
        inputField_Y.interactable = val;
        uwbObject.overridePosY = inputFieldToggle_Y.isOn;
    }
    public void ToggleInputField_Z(bool val)
    {
        inputField_Z.interactable = val;
        uwbObject.overridePosZ = inputFieldToggle_Z.isOn;
    }

    public void SetOverrideOnObject_X()
    {
        float x = 0f;
        float.TryParse(inputField_X.text, out x);
        uwbObject.OverridePosXValue = x;
        uwbObject.overridePosX = inputFieldToggle_X.isOn;
    }

    public void SetOverrideOnObject_Y()
    {
        float y = 0f;
        float.TryParse(inputField_Y.text, out y);
        uwbObject.OverridePosYValue = y;
        uwbObject.overridePosY = inputFieldToggle_Y.isOn;
    }

    public void SetOverrideOnObject_Z()
    {
        float z = 0f;
        float.TryParse(inputField_Z.text, out z);
        uwbObject.OverridePosZValue = z;
        uwbObject.overridePosZ = inputFieldToggle_Z.isOn;

    }
    #endregion

    #region // Position Offset //
    public void SetOffsetValueX(string val)
    {
        float.TryParse(val, out float i);
        uwbObject.OffsetPosX = i;
    }
    public void SetOffsetValueY(string val)
    {
        float.TryParse(val, out float i);
        uwbObject.OffsetPosY = i;
    }
    public void SetOffsetValueZ(string val)
    {
        float.TryParse(val, out float i);
        uwbObject.OffsetPosZ = i;
    }
    #endregion
}
