using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ObjectTransformGizmo : MonoBehaviour
{
    public SteamVR_Input_Sources m_TargetSource;
    public SteamVR_Action_Boolean m_ClickAction;
    public Transform m_RaycastSource;

    public GameObject m_AxisXObject;
    public GameObject m_AxisYObject;
    public GameObject m_AxisZObject;
    public GameObject m_MoveObject;

    public GameObject m_GizmoTarget;

    public GizmoClickDetector[] m_GizmoClickDetectors;

    private Vector3 m_LastPosition = Vector3.zero;

    private bool m_HasGrabbed = false;

    public LayerMask m_LayerMask;

    GizmoClickDetector hitReciver = null;

    void Start()
    {
        m_GizmoClickDetectors = new GizmoClickDetector[1];
        //m_GizmoClickDetectors[0] = m_AxisXObject.GetComponent<GizmoClickDetector>();
        //m_GizmoClickDetectors[1] = m_AxisYObject.GetComponent<GizmoClickDetector>();
        //m_GizmoClickDetectors[2] = m_AxisZObject.GetComponent<GizmoClickDetector>();
        m_GizmoClickDetectors[0] = m_MoveObject.GetComponent<GizmoClickDetector>();

        UpdateGizmoTarget();

        UpdateGizmoPosition();
    }

    void Update()
    {
        //if (m_ClickAction.GetStateDown(m_TargetSource) && m_GizmoClickDetectors[0].m_IsPressed)
        //{
        //    Vector3 direction = m_GizmoTarget.transform.position - m_LastPosition;
        //    m_LastPosition = m_GizmoTarget.transform.position;
        //    m_GizmoTarget.transform.Translate(direction);
        //}

        //if (m_ClickAction.GetState(m_TargetSource))
        //{
        //    m_HasGrabbed = true;
        //}
        //else if (m_ClickAction.GetStateUp(m_TargetSource))
        //{
        //    m_HasGrabbed = false;
        //}

        UpdateGizmoTarget();

        UpdateGizmoPosition();

        //for (int i = 0; i < 3; i++)
        //{
        //    if (m_ClickAction.GetStateDown(m_TargetSource) && m_GizmoClickDetectors[i].m_IsPressed)
        //    {
        //        Vector3 offset = Vector3.zero;

        //        switch (i)
        //        {
        //            case 0:
        //                float deltaX = Input.GetAxis("Mouse X") * (Time.deltaTime);
        //                offset = Vector3.left * deltaX;
        //                offset = new Vector3(offset.x, 0.0f, 0.0f);
        //                m_GizmoTarget.transform.Translate(offset);
        //                break;
        //            case 1:
        //                float deltaY = Input.GetAxis("Mouse X") * (Time.deltaTime);
        //                offset = Vector3.up * deltaY;
        //                offset = new Vector3(0.0f, offset.y, 0.0f);
        //                m_GizmoTarget.transform.Translate(offset);
        //                break;
        //            case 2:
        //                float deltaZ = Input.GetAxis("Mouse X") * (Time.deltaTime);
        //                offset = Vector3.forward * deltaZ;
        //                offset = new Vector3(0.0f, 0.0f, offset.z);
        //                m_GizmoTarget.transform.Translate(offset);
        //                break;
        //        }
        //    }
        //}
    }

    private void FixedUpdate()
    {
        RaycastHit hit;

        bool btn = m_ClickAction.GetState(m_TargetSource);
        if (btn)
        {
            
            if (Physics.Raycast(m_RaycastSource.position, m_RaycastSource.forward, out hit, Mathf.Infinity, m_LayerMask))
            {
                print("I hit: " + hit.collider.gameObject.name);
                if (hit.collider != null && hitReciver == null)
                {
                    print("getting new receiver");
                    // Find the hit reciver (if existant) and call the method
                    hitReciver = hit.collider.gameObject.GetComponent<GizmoClickDetector>();
                    hitReciver.m_IsPressed = true;
                    hitReciver.m_Collider.enabled = false;

                }

                if (hitReciver != null && hitReciver.m_IsPressed)
                {
                    Vector3 point = hit.point;
                    point.y = 0f;
                    hitReciver.transform.position = point;
                    m_GizmoTarget.transform.position = point;
                    //m_GizmoTarget.transform.position.y = 0f;
                }
            }
        }
        else if(btn == false)
        {
            if(hitReciver != null)
            {
                hitReciver.m_IsPressed = false;
                hitReciver.m_Collider.enabled = true;
                hitReciver = null;
            }
        }
    }

    void UpdateGizmoTarget()
    {
        if (UWBObjectManager.i.activeSelectedUWBObject == null)
        {
            return;
        }
        m_GizmoTarget = UWBObjectManager.i.activeSelectedUWBObject.gameObject;
    }

    void UpdateGizmoPosition()
    {
        if (!m_GizmoTarget)
        {
            return;
        }
        transform.position = m_GizmoTarget.transform.position;
    }
}
