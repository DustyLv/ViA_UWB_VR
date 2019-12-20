using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ToggleVR : MonoBehaviour
{
    public bool m_VREnabled = true;

    void Awake()
    {
        Toggle();
    }

    void Update()
    {
        
    }

    void Toggle()
    {
        if (m_VREnabled)
        {
            if (!XRSettings.enabled)
            {
                XRSettings.enabled = true;
            }
        }
        else
        {
            if (XRSettings.enabled)
            {
                XRSettings.enabled = false;
            }
        }
    }
}
