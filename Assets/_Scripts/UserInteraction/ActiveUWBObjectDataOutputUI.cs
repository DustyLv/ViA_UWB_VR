using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActiveUWBObjectDataOutputUI : MonoBehaviour
{

    public TextMeshProUGUI activeObjectTransformDataText;

    private UWBObjectManager uwbMan;

    private string activeTransformString = "";


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


    private UWBObject m_activeUWBObject;

    void Start()
    {
        uwbMan = UWBObjectManager.i;
    }

    void Update()
    {
        m_activeUWBObject = uwbMan.activeSelectedUWBObject;
        if (m_activeUWBObject != null)
        {
            Vector3 activePos = uwbMan.activeSelectedUWBObject.transform.position;
            Vector3 activeRot = uwbMan.activeSelectedUWBObject.transform.rotation.eulerAngles;
            activeTransformString = string.Format("Position: \n X: {0} \n Y: {1} \n Z: {2} \n\n Rotation: \n X: {3} \n Y: {4} \n Z: {5}", activePos.x, activePos.y, activePos.z, activeRot.x, activeRot.y, activeRot.z);
        }
        else
        {
            activeTransformString = "Position: \n - \n - \n - \n\n Rotation: \n - \n - \n -";
        }
        activeObjectTransformDataText.text = activeTransformString;
    }

    public void InitNewSelectedObject()
    {
        InitInvertToggles();
        InitOverrideFields();
    }


    #region // Position Inversion //
    void InitInvertToggles()
    {
        if (m_activeUWBObject != null)
        {
            toggle_invertX.isOn = m_activeUWBObject.InvertPosX;
            toggle_invertY.isOn = m_activeUWBObject.InvertPosY;
            toggle_invertZ.isOn = m_activeUWBObject.InvertPosZ;
        }
    }

    public void ToggleInvertX(bool val)
    {
        m_activeUWBObject.InvertPosX = val;
    }

    public void ToggleInvertY(bool val)
    {
        m_activeUWBObject.InvertPosY = val;
    }

    public void ToggleInvertZ(bool val)
    {
        m_activeUWBObject.InvertPosZ = val;

    }
    #endregion

    #region // Position Override //
    void InitOverrideFields()
    {
        if (m_activeUWBObject != null)
        {
            inputFieldToggle_X.isOn = m_activeUWBObject.overridePosX;
            inputFieldToggle_Y.isOn = m_activeUWBObject.overridePosY;
            inputFieldToggle_Z.isOn = m_activeUWBObject.overridePosZ;

            inputField_X.interactable = inputFieldToggle_X.isOn;
            inputField_Y.interactable = inputFieldToggle_Y.isOn;
            inputField_Z.interactable = inputFieldToggle_Z.isOn;

            inputField_X.text = m_activeUWBObject.OverridePosXValue.ToString();
            inputField_Y.text = m_activeUWBObject.OverridePosYValue.ToString();
            inputField_Z.text = m_activeUWBObject.OverridePosZValue.ToString();
        }
    }

    public void ToggleInputField_X(bool val)
    {
        inputField_X.interactable = val;
        m_activeUWBObject.overridePosX = inputFieldToggle_X.isOn;
    }
    public void ToggleInputField_Y(bool val)
    {
        inputField_Y.interactable = val;
        m_activeUWBObject.overridePosY = inputFieldToggle_Y.isOn;
    }
    public void ToggleInputField_Z(bool val)
    {
        inputField_Z.interactable = val;
        m_activeUWBObject.overridePosZ = inputFieldToggle_Z.isOn;
    }

    public void SetOverrideOnObject_X()
    {
        float x = 0f;
        float.TryParse(inputField_X.text, out x);
        m_activeUWBObject.OverridePosXValue = x;
        m_activeUWBObject.overridePosX = inputFieldToggle_X.isOn;
    }

    public void SetOverrideOnObject_Y()
    {
        float y = 0f;
        float.TryParse(inputField_Y.text, out y);
        m_activeUWBObject.OverridePosYValue = y;
        m_activeUWBObject.overridePosY = inputFieldToggle_Y.isOn;
    }

    public void SetOverrideOnObject_Z()
    {
        float z = 0f;
        float.TryParse(inputField_Z.text, out z);
        m_activeUWBObject.OverridePosZValue = z;
        m_activeUWBObject.overridePosZ = inputFieldToggle_Z.isOn;

    }
    #endregion

    #region // Position Offset //
    public void SetOffsetValueX(string val)
    {
        float.TryParse(val, out float i);
        m_activeUWBObject.OffsetPosX = i;
    }
    public void SetOffsetValueY(string val)
    {
        float.TryParse(val, out float i);
        m_activeUWBObject.OffsetPosY = i;
    }
    public void SetOffsetValueZ(string val)
    {
        float.TryParse(val, out float i);
        m_activeUWBObject.OffsetPosZ = i;
    }
    #endregion
}
