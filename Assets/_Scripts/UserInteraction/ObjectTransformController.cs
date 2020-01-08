using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ObjectTransformController : MonoBehaviour
{
    public SteamVR_Input_Sources m_TargetSource;
    public SteamVR_Action_Boolean m_ClickAction;

    private Transform m_RaycastSource;

    private GameObject m_MoveTarget;

    //private bool m_HasGrabbed = false;

    public LayerMask m_LayerMask;

    //GizmoClickDetector hitReciver = null;

    void Start()
    {
        m_RaycastSource = GlobalVariables.i.RIGHT_ARM;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMoveTarget();
    }

    private void FixedUpdate()
    {
        if (m_MoveTarget != null)
        {
            RaycastHit hit;

            bool btn = m_ClickAction.GetState(m_TargetSource);

            if (btn)
            {
                
                if (Physics.Raycast(m_RaycastSource.position, m_RaycastSource.forward, out hit, Mathf.Infinity, m_LayerMask))
                {
                    //hit.normal
                    Vector3 point = hit.point;
                    //point.y = 0f;
                    m_MoveTarget.transform.position = point;
                    UWBObjectManager.i.activeSelectedUWBObject.m_OverridePosition = point;

                    UWBObjectManager.i.activeSelectedUWBObject.m_OverridePositionState[0] = true;
                    UWBObjectManager.i.activeSelectedUWBObject.m_OverridePositionState[1] = true;
                    UWBObjectManager.i.activeSelectedUWBObject.m_OverridePositionState[2] = true;

                }
            }
        }
    }

    public void Scale(float _scale)
    {
        if (UWBObjectManager.i.activeSelectedUWBObject == null)
        {
            Debug.LogWarning("There is no active selected UWB object.");
            return;
        }

        Transform activeObject = UWBObjectManager.i.activeSelectedUWBObject.gameObject.transform;
        Vector3 newScale = new Vector3(_scale, _scale, _scale);
        activeObject.localScale = newScale;

    }

    public void Rotate(float _rotation)
    {
        if (UWBObjectManager.i.activeSelectedUWBObject == null)
        {
            Debug.LogWarning("There is no active selected UWB object.");
            return;
        }
        Transform activeObject = UWBObjectManager.i.activeSelectedUWBObject.gameObject.transform;
        Vector3 newRotation = new Vector3(0f, _rotation, 0f);
        activeObject.rotation = Quaternion.Euler(newRotation);
    }

    void UpdateMoveTarget()
    {
        if (UWBObjectManager.i.activeSelectedUWBObject == null)
        {
            return;
        }
        if (m_MoveTarget != UWBObjectManager.i.activeSelectedUWBObject.gameObject)
            m_MoveTarget = UWBObjectManager.i.activeSelectedUWBObject.gameObject;
    }
}
