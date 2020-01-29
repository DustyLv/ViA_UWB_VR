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
        InitOverrideFields();
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
            toggle_invertX.isOn = m_activeUWBObject.m_InvertPositionState[0];
            toggle_invertY.isOn = m_activeUWBObject.m_InvertPositionState[1];
            toggle_invertZ.isOn = m_activeUWBObject.m_InvertPositionState[2];
        }
    }

    public void ToggleInvertX(bool val)
    {
        m_activeUWBObject.m_InvertPositionState[0] = val;
        UWBObjectManager.i.SendUpdatedActiveObjectsSettingsToServer();
    }

    public void ToggleInvertY(bool val)
    {
        m_activeUWBObject.m_InvertPositionState[1] = val;
        UWBObjectManager.i.SendUpdatedActiveObjectsSettingsToServer();
    }

    public void ToggleInvertZ(bool val)
    {
        m_activeUWBObject.m_InvertPositionState[2] = val;
        UWBObjectManager.i.SendUpdatedActiveObjectsSettingsToServer();

    }
    #endregion

    #region // Position Override //
    void InitOverrideFields()
    {
        if (m_activeUWBObject != null)
        {
            inputFieldToggle_X.isOn = m_activeUWBObject.m_OverridePositionState[0];
            inputFieldToggle_Y.isOn = m_activeUWBObject.m_OverridePositionState[1];
            inputFieldToggle_Z.isOn = m_activeUWBObject.m_OverridePositionState[2];

            inputField_X.interactable = inputFieldToggle_X.isOn;
            inputField_Y.interactable = inputFieldToggle_Y.isOn;
            inputField_Z.interactable = inputFieldToggle_Z.isOn;

            //inputField_X.text = m_activeUWBObject.m_OverridePosition.x.ToString("0.000");
            //inputField_Y.text = m_activeUWBObject.m_OverridePosition.y.ToString("0.000");
            //inputField_Z.text = m_activeUWBObject.m_OverridePosition.z.ToString("0.000");
        }
    }

    public void ToggleInputField_X(bool val)
    {
        inputField_X.interactable = val;
        m_activeUWBObject.m_OverridePositionState[0] = inputFieldToggle_X.isOn;

    }
    public void ToggleInputField_Y(bool val)
    {
        inputField_Y.interactable = val;
        m_activeUWBObject.m_OverridePositionState[1] = inputFieldToggle_Y.isOn;
    }
    public void ToggleInputField_Z(bool val)
    {
        inputField_Z.interactable = val;
        m_activeUWBObject.m_OverridePositionState[2] = inputFieldToggle_Z.isOn;
    }

    public void SetOverrideOnObject_X()
    {
        float x = 0f;
        float.TryParse(inputField_X.text, out x);
        m_activeUWBObject.m_OverridePosition.x = x;
        m_activeUWBObject.m_OverridePositionState[0] = inputFieldToggle_X.isOn;
        UWBObjectManager.i.SendUpdatedActiveObjectsSettingsToServer();
    }

    public void SetOverrideOnObject_Y()
    {
        float y = 0f;
        float.TryParse(inputField_Y.text, out y);
        m_activeUWBObject.m_OverridePosition.y = y;
        m_activeUWBObject.m_OverridePositionState[1] = inputFieldToggle_Y.isOn;
        UWBObjectManager.i.SendUpdatedActiveObjectsSettingsToServer();
    }

    public void SetOverrideOnObject_Z()
    {
        float z = 0f;
        float.TryParse(inputField_Z.text, out z);
        m_activeUWBObject.m_OverridePosition.z = z;
        m_activeUWBObject.m_OverridePositionState[2] = inputFieldToggle_Z.isOn;
        UWBObjectManager.i.SendUpdatedActiveObjectsSettingsToServer();

    }
    #endregion

    #region // Position Offset //
    public void SetOffsetValueX(string val)
    {
        float.TryParse(val, out float i);
        m_activeUWBObject.m_OffsetPosition.x = i;
        UWBObjectManager.i.SendUpdatedActiveObjectsSettingsToServer();
    }
    public void SetOffsetValueY(string val)
    {
        float.TryParse(val, out float i);
        m_activeUWBObject.m_OffsetPosition.y = i;
        UWBObjectManager.i.SendUpdatedActiveObjectsSettingsToServer();
    }
    public void SetOffsetValueZ(string val)
    {
        float.TryParse(val, out float i);
        m_activeUWBObject.m_OffsetPosition.z = i;
        UWBObjectManager.i.SendUpdatedActiveObjectsSettingsToServer();
    }
    #endregion
}
