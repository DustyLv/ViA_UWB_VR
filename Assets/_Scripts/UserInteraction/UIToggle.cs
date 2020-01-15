using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class UIToggle : MonoBehaviour
{
    public SteamVR_Input_Sources m_TargetSource;
    public SteamVR_Action_Boolean m_ClickAction;

    public GameObject[] m_UIObjects;


    void Update()
    {
        bool btn = m_ClickAction.GetStateDown(m_TargetSource);

        if (btn)
        {
            ToggleUIObjects();
        }
    }

    public void ToggleUIObjects()
    {
        foreach (GameObject ob in m_UIObjects)
        {
            ob.SetActive(!ob.activeInHierarchy);
        }
    }
}
