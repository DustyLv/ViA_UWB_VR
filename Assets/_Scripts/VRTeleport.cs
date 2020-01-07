using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.XR;

public class VRTeleport : MonoBehaviour
{
    public LayerMask layerMask = -1;
    public Transform rightHand;
    public Transform playerObject;

    public SteamVR_Input_Sources m_TargetSource;
    public SteamVR_Action_Boolean m_TeleportAction;

    private bool validTeleportAreaFound = false;
    private RaycastHit m_hit;

    void Start()
    {
    }

    void FixedUpdate()
    {
        if (m_TeleportAction.GetStateUp(m_TargetSource))
        {
            Teleport();
        }
    }

    public void Teleport()
    {
        validTeleportAreaFound = Physics.Raycast(rightHand.position, rightHand.TransformDirection(Vector3.forward), out m_hit, Mathf.Infinity, layerMask);
        if (validTeleportAreaFound)
        {
            Vector3 newPos = m_hit.point;
            playerObject.position = newPos;
        }
    }
}
